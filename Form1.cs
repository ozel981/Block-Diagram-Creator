using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Globalization;
using System.Threading;

namespace BlockDiagramCreator
{
    public partial class mainForm : Form
    {
        Size size;
        Assembly assembly = Assembly.Load("BlockDiagramCreator");
        ResourceManager resourceManager;
        int xDis = 0, yDis = 0;
        bool scrollDown = false;
        Button button = null;
        Block selected = null;
        Image pictureBoxImage = null;
        bool linking = false;
        Graphics canvas;
        List<Block> blockList = new List<Block>();
        LinkPoint selectedLinkPoint = null;
        public mainForm()
        {
            InitializeComponent();
            size = this.Size;
            NewScheme();
            resourceManager = new ResourceManager("BlockDiagramCreator.languagepl-PL", assembly);
        }
        // create and return new picture box
        private PictureBox NewSchemeCanvas(int width = 1000, int height = 1000)
        {
            PictureBox newPictureBox = new PictureBox();
            newPictureBox.Name = "pictureBox";
            newPictureBox.Size = new Size(width, height);
            newPictureBox.BackColor = Color.White;
            newPictureBox.MouseDown += pictureBox_MouseDown;
            newPictureBox.MouseUp += pictureBox_MouseUp;
            newPictureBox.MouseMove += pictureBox_MouseMove;
            newPictureBox.Image = new Bitmap(width, height);
            return newPictureBox;
        }
        // clear picture box
        private void ClearPictureBoxPanel()
        {
            List<PictureBox> removedBlock = new List<PictureBox>();
            foreach (PictureBox pictBox in pictureBoxPanel.Controls)
            {
                removedBlock.Add(pictBox);
            }
            foreach (PictureBox pictBox in removedBlock)
            {
                pictureBoxPanel.Controls.Remove(pictBox);
            }
        }
        // create and set new scheme
        public void NewScheme(int width = 1000, int height = 1000)
        {
            selected = null;
            textSelectedBlock.Enabled = false;
            textSelectedBlock.Text = "";
            ClearPictureBoxPanel();
            pictureBox = NewSchemeCanvas(width, height);
            canvas = Graphics.FromImage(pictureBox.Image);
            blockList.Clear();
            pictureBoxPanel.Controls.Add(pictureBox);
        }
        // refresh scheme with drawing image (refresh after image draw can by disable) 
        private void RefreshScheme(Image image, bool refresh = true)
        {
            if (canvas != null && pictureBox != null)
            {
                canvas.Clear(Color.White);
                canvas.DrawImage(image, new Point(0, 0));
                if (refresh) pictureBox.Refresh();
            }
        }
        // refresh scheme with redrawing all blocks
        private void RefreshScheme()
        {
            Image refresedImage = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics graphics = Graphics.FromImage(refresedImage);
            foreach (Block block in blockList)
            {
                block.Draw(graphics);
            }
            if (canvas != null && pictureBox != null)
            {
                canvas.Clear(Color.White);
                canvas.DrawImage(refresedImage, new Point(0, 0));
                pictureBox.Refresh();
            }
        }
        // create diagram image witchout selected block
        private Image CreateImageWithoutSelectedBlock(Block selected)
        {
            Image newImage = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics graphics = Graphics.FromImage(newImage);
            foreach (Block block in blockList)
            {
                if (block != selected) block.Draw(graphics);
            }
            return newImage;
        }
        // set variable selected if any block is selected
        private bool SelectBlock(int X, int Y)
        {
            Block selectedBlock = null;
            selectedBlock = FindNearestBlock((Block block) => block.Click(X, Y), blockList, X, Y);
            if (selected != null && selectedBlock != null) selected.UnSelect();
            if (selectedBlock != null)
            {
                selected = selectedBlock;
                selected.Select();
                // set text textSelectedBlock text box to selected block inscription
                textSelectedBlock.Enabled = (selected.GetType().Name == "DecidingBlock" || selected.GetType().Name == "OperatingBlock");
                textSelectedBlock.Text = selected.GetText();
                return true;
            }
            else
            {
                if (selected != null)
                {
                    // disable textSelectedBlock text box
                    textSelectedBlock.Enabled = false;
                    textSelectedBlock.Text = "";
                    selected.UnSelect();
                }
                selected = null;
                return false;
            }
        }
        // find and return block nearest from mouse (X,Y) point
        private Block FindNearestBlock(Func<Block, bool> func, List<Block> list, int X, int Y)
        {
            int dist = int.MaxValue;
            Block foundBlock = null;
            foreach (Block block in list)
            {
                if (func(block) && Math.Sqrt((X - block.X) * (X - block.X) + (Y - block.Y) * (Y - block.Y)) < dist)
                {
                    foundBlock = block;
                    dist = (int)Math.Sqrt((X - block.X) * (X - block.X) + (Y - block.Y) * (Y - block.Y));
                }
            }
            return foundBlock;
        }
        // edit menu button click
        private void BoxButton_Click(object sender, EventArgs e)
        {
            if (button != null)
            {
                button.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
           ((Button)sender).BackColor = Color.FromKnownColor(KnownColor.ActiveBorder);
            button = ((Button)sender);
        }
        // picture box mouse down event 
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            // disable buttons during block moving
            if (scrollDown) return;
            // if block has to start moving
            if (e.Button == MouseButtons.Middle && selected != null && !linking)
            {
                scrollDown = true;
                pictureBoxImage = CreateImageWithoutSelectedBlock(selected);
                xDis = selected.X - e.X;
                yDis = selected.Y - e.Y;
            }
            // (un)select block
            if (e.Button == MouseButtons.Right)
            {
                SelectBlock(e.X, e.Y);
                RefreshScheme();
                pictureBoxImage = (Image)pictureBox.Image.Clone();
            }
            // left click mouse interaction
            if (e.Button == MouseButtons.Left)
            {
                // create operation block
                if (button == OperationBoxButton)
                {
                    canvas = Graphics.FromImage(pictureBox.Image);
                    OperatingBlock bloc = new OperatingBlock(e.X, e.Y, resourceManager.GetString("OperationBlock"));
                    bloc.Draw(canvas);
                    blockList.Add(bloc);
                }
                // create decide block
                else if (button == DecidingBoxButton)
                {
                    canvas = Graphics.FromImage(pictureBox.Image);
                    DecidingBlock bloc = new DecidingBlock(e.X, e.Y, resourceManager.GetString("DecideBlock"));
                    bloc.Draw(canvas);
                    blockList.Add(bloc);
                }
                // create start block
                else if (button == startBoxButton)
                {
                    if (FindNearestBlock((Block b) =>
                    {
                        if (b.GetType().Name == "StartBlock") return true;
                        else return false;
                    }, blockList, e.X, e.Y) == null)
                    {
                        // create
                        canvas = Graphics.FromImage(pictureBox.Image);
                        StartBlock bloc = new StartBlock(e.X, e.Y, resourceManager.GetString("StartBlock"));
                        bloc.Draw(canvas);
                        blockList.Add(bloc);
                    }
                    else
                    {
                        // if already exist then show message
                        MessageBox.Show(resourceManager.GetString("DoubleStartBlockWarning"));
                    }
                }
                //create end block
                else if (button == endBoxButton)
                {
                    canvas = Graphics.FromImage(pictureBox.Image);
                    EndBlock bloc = new EndBlock(e.X, e.Y, resourceManager.GetString("StopBlock"));
                    bloc.Draw(canvas);
                    blockList.Add(bloc);
                }
                // start link creating
                else if (button == linkButton)
                {
                    LinkPoint x = null;
                    foreach (Block block in blockList)
                    {
                        if ((x = block.CheckPoint(e.X, e.Y, false)) != null)
                        {
                            selectedLinkPoint = x;
                        }
                    }
                    if (selectedLinkPoint != null)
                    {
                        linking = true;
                        pictureBox.Refresh();
                        pictureBoxImage = (Image)pictureBox.Image.Clone();
                    }
                }
                // delete block
                else if (button == deleteButton)
                {
                    Block removed = FindNearestBlock((Block block) => block.Click(e.X, e.Y), blockList, e.X, e.Y);
                    if (removed != null)
                    {
                        textSelectedBlock.Enabled = false;
                        textSelectedBlock.Text = "";
                        if (selected == removed) selected = null;
                        removed.Remove();
                        blockList.Remove(removed);
                    }
                    RefreshScheme();
                }
            }
            pictureBox.Refresh();
        }
        //picture box mouse move event
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            // during block moving
            if (scrollDown && selected != null)
            {
                selected.SetLayout(e.X + xDis, e.Y + yDis);
                RefreshScheme(pictureBoxImage, false);
                selected.Draw(canvas);
                pictureBox.Refresh();
            }
            // during link creating 
            else if (linking)
            {
                RefreshScheme(pictureBoxImage, false);
                canvas.DrawLine(Pens.Black, selectedLinkPoint.Layout.X, selectedLinkPoint.Layout.Y, e.X, e.Y);
                pictureBox.Refresh();
            }
        }
        //picture box mouse up event
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            // stop block moving
            if (e.Button == MouseButtons.Middle && selected != null)
            {
                scrollDown = false;
                selected.SetLayout(selected.X > pictureBox.Image.Width ? pictureBox.Image.Width : selected.X, selected.Y > pictureBox.Image.Height ? pictureBox.Image.Height : selected.Y);
                selected.SetLayout(selected.X < 0 ? 0 : selected.X, selected.Y < 0 ? 0 : selected.Y);
            }
            // check and create link
            else if (e.Button == MouseButtons.Left && selectedLinkPoint != null && button == linkButton)
            {
                LinkPoint x;
                foreach (Block block in blockList)
                {
                    if ((x = block.CheckPoint(e.X, e.Y, true)) != null)
                    {
                        if (x.Block != selectedLinkPoint.Block)
                        {
                            Link link = new Link(selectedLinkPoint, x);
                            selectedLinkPoint.SetLink(link);
                            x.SetLink(link);
                        }
                    }
                }
                selectedLinkPoint = null;
                linking = false;
            }
            // reselect if was selected
            if (selected != null) selected.Select();
            RefreshScheme();
        }
        //create new scheme
        private void NewShem_Click(object sender, EventArgs e)
        {
            
            newSchemeForm newScheme = new newSchemeForm(this);
            newScheme.StartPosition = FormStartPosition.CenterParent;
            newScheme.ShowDialog(this);
        }
        // text changed Text Box event
        private void textSelectedBlock_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Enabled)
            {
                selected.SetText(((TextBox)sender).Text);
                selected.Draw(canvas);
                pictureBox.Refresh();
            }
        }
        // on Polish button click event
        private void lanButtonPL_Click(object sender, EventArgs e)
        {
            //resource manager changes
            resourceManager = new ResourceManager("BlockDiagramCreator.languagepl-PL", assembly);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL"); //Launch the Form with the default/selected language           
            //save previous setup
            bool en = textSelectedBlock.Enabled;
            string str = textSelectedBlock.Text;
            Size s = this.Size;
            string buttonName = button != null ? button.Name : "";
            FormWindowState wasMaximized = this.WindowState;
            // reinitialization all components
            this.Controls.Clear();
            InitializeComponent();
            //restoration previous setup
            pictureBoxPanel.Controls.Add(pictureBox);          
            textSelectedBlock.Enabled = en;
            textSelectedBlock.Text = str;
            if(wasMaximized == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Minimized;
                this.WindowState = FormWindowState.Maximized;
            }
            else this.Size = s;
            switch (buttonName)
            {
                case "startBoxButton": { button = startBoxButton; } break;
                case "endBoxButton": { button = endBoxButton; } break;
                case "DecidingBoxButton": { button = DecidingBoxButton; } break;
                case "OperationBoxButton": { button = OperationBoxButton; } break;
                case "linkButton": { button = linkButton; } break;
                case "deleteButton": { button = deleteButton; } break;
                default: button = null; break;
            }
            if (button != null) button.BackColor = Color.FromKnownColor(KnownColor.ActiveBorder);
        }
        // on English button click event
        private void lanButtonEN_Click(object sender, EventArgs e)
        {
            //resource manager changes
            resourceManager = new ResourceManager("BlockDiagramCreator.languageen-GB", assembly);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB"); //Launch the Form with the default/selected language         
            //save previous setup
            bool en = textSelectedBlock.Enabled;
            string str = textSelectedBlock.Text;
            Size s = this.Size;
            string buttonName = button != null ? button.Name : "";
            FormWindowState wasMaximized = this.WindowState;
            // reinitialization all components
            this.Controls.Clear();
            InitializeComponent();
            //restoration previous setup
            pictureBoxPanel.Controls.Add(pictureBox);
            textSelectedBlock.Enabled = en;
            textSelectedBlock.Text = str;
            if (wasMaximized == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Minimized;
                this.WindowState = FormWindowState.Maximized;
            }
            else this.Size = s;
            switch (buttonName)
            {
                case "startBoxButton": { button = startBoxButton; } break;
                case "endBoxButton": { button = endBoxButton; } break;
                case "DecidingBoxButton": { button = DecidingBoxButton; } break;
                case "OperationBoxButton": { button = OperationBoxButton; } break;
                case "linkButton": { button = linkButton; } break;
                case "deleteButton": { button = deleteButton; } break;
                default: button = null; break;
            }
            if (button != null) button.BackColor = Color.FromKnownColor(KnownColor.ActiveBorder);
        }
        // save scheme event
        private void SaveScheme_Click(object sender, EventArgs e)
        {
            Stream newStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "diag files (*.diag)|*.diag";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((newStream = saveFileDialog.OpenFile()) != null)
                {
                    //create binary formater and do serialization
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    try
                    {
                        binaryFormatter.Serialize(newStream, blockList);
                        binaryFormatter.Serialize(newStream, pictureBox.Width);
                        binaryFormatter.Serialize(newStream, pictureBox.Height);
                        //message after property serialization 
                        MessageBox.Show(resourceManager.GetString("SaveSucces"));
                    }
                    catch(Exception)
                    {
                        //message after fail serialization 
                        MessageBox.Show(resourceManager.GetString("SaveFail"));
                        return;
                    }
                    newStream.Close();
                }
            }
        }
        // load scheme event
        private void LoadScheme_Click(object sender, EventArgs e)
        {


            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "diag files (*.diag)|*.diag";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    //Read the contents of the file into a stream
                    int width = 0; 
                    int height = 0;
                    var fileStream = openFileDialog.OpenFile();
                    //create binary formater and deserialize
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        try 
                        { 
                            blockList = (List<Block>)binaryFormatter.Deserialize(fileStream);
                            width = (int)binaryFormatter.Deserialize(fileStream);
                            height = (int)binaryFormatter.Deserialize(fileStream);
                        }
                        catch (Exception)
                        {
                            //message after fail deserialization
                            MessageBox.Show(resourceManager.GetString("LoadFail"));
                            return;
                        }
                        //reset aplication setup
                        textSelectedBlock.Enabled = false;
                        textSelectedBlock.Text = "";
                        xDis = 0; yDis = 0;
                        scrollDown = false;
                        selected = null;
                        pictureBoxImage = null;
                        linking = false;
                        selectedLinkPoint = null;
                        foreach (Block block in blockList)
                        {
                            if (block.IsSelected())
                            {
                                selected = block;
                                textSelectedBlock.Text = selected.GetText();
                                textSelectedBlock.Enabled = textSelectedBlock.Enabled = (selected.GetType().Name == "DecidingBlock" || selected.GetType().Name == "OperatingBlock");

                            }
                        }
                        ClearPictureBoxPanel();
                        pictureBox = NewSchemeCanvas(width, height);
                        canvas = Graphics.FromImage(pictureBox.Image);
                        pictureBoxPanel.Controls.Add(pictureBox);
                        RefreshScheme();
                        fileContent = reader.ReadToEnd();
                        //message after property deserialization
                        MessageBox.Show(resourceManager.GetString("LoadSucces"));
                    }
                }
            }

        }
    }
}
