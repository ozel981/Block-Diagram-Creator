using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockDiagramCreator
{
    [Serializable]
    abstract class Block
    {
        protected bool selected = false;
        protected Point layout;
        protected string inscription;        
        public int X { get { return layout.X; } }
        public int Y { get { return layout.Y; } }
        public Point Layout { get { return layout; } }
        public Block(int x, int y, string inscription)
        {
            layout = new Point(x, y);
            this.inscription = inscription;
        }
        public string GetText()
        {
            return inscription;
        }
        public void SetText(string text)
        {
            inscription = text;
        }
        public void UnSelect()
        {
            selected = false;
        }
        public bool IsSelected()
        {
            return selected;
        }
        public abstract void Select();
        public abstract void Draw(Graphics canvas);
        public abstract bool Click(int x, int y);
        public abstract void SetLayout(int x, int y);       
        public abstract void Remove();
        public abstract LinkPoint CheckPoint(int x, int y, bool isInput);
    }
    [Serializable]
    class OperatingBlock : Block
    {
        private readonly (int width, int height) size = (120,40);
        private LinkPoint input;
        private LinkPoint output;
        private Rectangle rec;
        public OperatingBlock(int x, int y, string inscription) : base(x, y, inscription)
        {
            input = new LinkPoint(x, y - (size.height / 2), true, this);
            output = new LinkPoint(x, y + (size.height / 2), false, this);
            rec = new Rectangle(new Point(x - (size.width/2), y - (size.height / 2)), new Size(size.width, size.height));
        }
        // sets as selected
        public override void Select()
        {
            selected = true;
            //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            input.SetDrawingLinkPoint();
            output.SetDrawingLinkPoint();
        }
        // draws block with all components
        public override void Draw(Graphics canvas)
        {
            Pen pen = new Pen(Color.Black, 3);
            Font font = new Font(new FontFamily("Arial"), 12, FontStyle.Regular, GraphicsUnit.Pixel);
            Brush brush = new SolidBrush(Color.Black);
            pen.DashStyle = selected ? System.Drawing.Drawing2D.DashStyle.Dash : System.Drawing.Drawing2D.DashStyle.Solid;
            canvas.DrawRectangle(pen, rec);
            canvas.FillRectangle(new SolidBrush(Color.White), rec);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            canvas.DrawString(inscription, font, brush, rec, stringFormat);

            input.Draw(canvas);
            output.Draw(canvas);
        }
        // checks if block was clicked
        public override bool Click(int x, int y)
        {
            if (rec.Left <= x && rec.Right >= x && rec.Top <= y && rec.Bottom >= y) return true;
            return false;
        }
        // changes layout block with all components 
        public override void SetLayout(int x, int y)
        {
            rec = new Rectangle(new Point(x - (size.width / 2), y - (size.height / 2)), new Size(size.width, size.height));
            input.SetLayout(x, y - (size.height / 2));
            output.SetLayout(x, y + (size.height / 2));
            layout.X = x;
            layout.Y = y;
        }
        // removes block with all components 
        public override void Remove()
        {
            input.Remove();
            output.Remove();
            input = null;
            output = null;
        }
        // checks wchitch link point was clicked
        public override LinkPoint CheckPoint(int x, int y, bool isInput)
        {
            if (input.CheckPoint(x, y, isInput)) return input;
            else if (output.CheckPoint(x, y, isInput)) return output;
            return null;
        }
    }
    [Serializable]
    class DecidingBlock : Block
    {
        private readonly (int width, int height) size = (120, 100);
        private Rectangle stringRect;
        private LinkPoint input;
        private LinkPoint trueOutput;
        private LinkPoint falseOutput;
        private Point[] points = new Point[4];
        public DecidingBlock(int x, int y, string inscription) : base(x, y, inscription)
        {
            input = new LinkPoint(x, y - (size.height / 2), true, this);
            trueOutput = new LinkPoint(x + (size.width / 2), y, false, this);
            falseOutput = new LinkPoint(x - (size.width / 2), y, false, this);
            points[0] = new Point(x - (size.width / 2), y);
            points[1] = new Point(x, y - (size.height / 2));
            points[2] = new Point(x + (size.width / 2), y);
            points[3] = new Point(x, y + (size.height / 2));
            stringRect = new Rectangle(new Point(x - (size.width / 4), y - (size.height / 4)), new Size((size.width / 2), (size.height / 2)));
        }
        // sets as selected
        public override void Select()
        {
            selected = true;
            input.SetDrawingLinkPoint();
            trueOutput.SetDrawingLinkPoint();
            falseOutput.SetDrawingLinkPoint();
        }
        // draws block with all components
        public override void Draw(Graphics canvas)
        {
            Pen pen = new Pen(Color.Black, 3);
            Font font = new Font(new FontFamily("Arial"), 12, FontStyle.Regular, GraphicsUnit.Pixel);
            Brush brush = new SolidBrush(Color.Black);
            pen.DashStyle = selected ? System.Drawing.Drawing2D.DashStyle.Dash : System.Drawing.Drawing2D.DashStyle.Solid;
            canvas.DrawPolygon(pen, points);
            canvas.FillPolygon(new SolidBrush(Color.White), points);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            canvas.DrawString(inscription, font, brush, stringRect, stringFormat);
            canvas.DrawString("T", font, brush,layout.X + (size.width / 2) + 3, -12 + Layout.Y);
            canvas.DrawString("F", font, brush, Layout.X - (size.width / 2) - 12, -12 + Layout.Y);


            input.Draw(canvas);
            trueOutput.Draw(canvas);
            falseOutput.Draw(canvas);
        }
        // checks if block was clicked
        public override bool Click(int x, int y)
        {
            double xx = Math.Abs(points[1].X - x) * 7 / 13;
            if (xx + points[0].Y - 35 <= y && -xx + points[0].Y + 35 >= y)
            {
                return true;
            }
            return false;
        }
        // changes layout block with all components 
        public override void SetLayout(int x, int y)
        {
            stringRect = new Rectangle(new Point(x - (size.width / 4), y - (size.height / 4)), new Size((size.width / 2), (size.height / 2)));
            points[0] = new Point(x - (size.width / 2), y);
            points[1] = new Point(x, y - (size.height / 2));
            points[2] = new Point(x + (size.width / 2), y);
            points[3] = new Point(x, y + (size.height / 2));
            input.SetLayout(x, y - (size.height / 2));
            trueOutput.SetLayout(x+ (size.width / 2), y);
            falseOutput.SetLayout(x- (size.width / 2), y);
            layout.X = x;
            layout.Y = y;
        }
        // removes block with all components 
        public override void Remove()
        {
            input.Remove();
            trueOutput.Remove();
            falseOutput.Remove();
            input = null;
            trueOutput = null;
            falseOutput = null;
        }
        // checks wchitch link point was clicked
        public override LinkPoint CheckPoint(int x, int y, bool isInput)
        {
            if (input.CheckPoint(x, y, isInput)) return input;
            else if (trueOutput.CheckPoint(x, y, isInput)) return trueOutput;
            else if (falseOutput.CheckPoint(x, y, isInput)) return falseOutput;
            return null;
        }
    }
    [Serializable]
    class StartBlock : Block
    {
        private readonly (int width, int height) size = (100, 40);
        private LinkPoint linkpoint;
        private Rectangle rec;
        public StartBlock(int x, int y, string inscription) : base(x, y, inscription)
        {
            linkpoint = new LinkPoint(x, y + (size.height / 2), false, this);
            rec = new Rectangle(new Point(x - (size.width/2), y - (size.height/2)), new Size(size.width, size.height));
        }
        // sets as selected
        public override void Select()
        {
            selected = true;
            //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            linkpoint.SetDrawingLinkPoint();
        }
        // draws block with all components
        public override void Draw(Graphics canvas)
        {
            Pen pen = new Pen(Color.Black, 3);
            Font font = new Font(new FontFamily("Arial"), 12, FontStyle.Regular, GraphicsUnit.Pixel);
            Brush brush = new SolidBrush(Color.Black);
            pen.DashStyle = selected ? System.Drawing.Drawing2D.DashStyle.Dash : System.Drawing.Drawing2D.DashStyle.Solid;
            pen.Color = Color.Green;
            canvas.FillEllipse(new SolidBrush(Color.White), rec);
            canvas.DrawEllipse(pen, rec);
            pen.Color = Color.Black;

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            canvas.DrawString(inscription, font, brush, layout.X, layout.Y, stringFormat);

            linkpoint.Draw(canvas);

        }
        // checks if block was clicked
        public override bool Click(int x, int y)
        {
            int a = 50;
            int b = 20;
            if ((double)((layout.X - x) * (layout.X - x)) / (double)(a * a) + (double)((layout.Y - y) * (layout.Y - y)) / (double)(b * b) <= 1) return true;
            return false;
        }
        // changes layout block with all components 
        public override void SetLayout(int x, int y)
        {
            rec = new Rectangle(new Point(x - (size.width / 2), y - (size.height / 2)), new Size(size.width, size.height));
            linkpoint.SetLayout(x, y + (size.height / 2));
            layout.X = x;
            layout.Y = y;
        }
        // removes block with all components 
        public override void Remove()
        {
            linkpoint.Remove();
            linkpoint = null;
        }
        // checks wchitch link point was clicked
        public override LinkPoint CheckPoint(int x, int y, bool isInput)
        {
            if (linkpoint.CheckPoint(x, y, isInput)) return linkpoint;
            else return null;
        }

    }
    [Serializable]
    class EndBlock : Block
    {
        private readonly (int width, int height) size = (100, 40);
        private LinkPoint linkpoint;
        private Rectangle rec;
        public EndBlock(int x, int y, string inscription) : base(x, y, inscription)
        {
            linkpoint = new LinkPoint(x, y - 20, true, this);
            rec = new Rectangle(new Point(x - (size.width / 2), y - (size.height / 2)), new Size(size.width, size.height));
        }
        // sets as selected
        public override void Select()
        {
            selected = true;
            linkpoint.SetDrawingLinkPoint();
        }
        // draws block with all components
        public override void Draw(Graphics canvas)
        {
            Pen pen = new Pen(Color.Black, 3);
            Font font = new Font(new FontFamily("Arial"), 12, FontStyle.Regular, GraphicsUnit.Pixel);
            Brush brush = new SolidBrush(Color.Black);
            pen.DashStyle = selected ? System.Drawing.Drawing2D.DashStyle.Dash : System.Drawing.Drawing2D.DashStyle.Solid;
            pen.Color = Color.Red;
            canvas.FillEllipse(new SolidBrush(Color.White), rec);
            canvas.DrawEllipse(pen, rec);
            pen.Color = Color.Black;

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            canvas.DrawString(inscription, font, brush, layout.X, layout.Y, stringFormat);
            
            linkpoint.Draw(canvas);
        }
        public override bool Click(int x, int y)
        {
            int a = 50;
            int b = 20;
            if ((double)((layout.X - x) * (layout.X - x)) / (double)(a * a) + (double)((layout.Y - y) * (layout.Y - y)) / (double)(b * b) <= 1) return true;
            return false;
        }
        // changes layout block with all components 
        public override void SetLayout(int x, int y)
        {
            rec = new Rectangle(new Point(x - (size.width / 2), y - (size.height / 2)), new Size(size.width, size.height));
            linkpoint.SetLayout(x, y - (size.height / 2));
            layout.X = x;
            layout.Y = y;
        }
        // removes block with all components 
        public override void Remove()
        {
            linkpoint.Remove();
            linkpoint = null;
        }
        // checks wchitch link point was clicked
        public override LinkPoint CheckPoint(int x, int y, bool isInput)
        {
            if (linkpoint.CheckPoint(x, y, isInput)) return linkpoint;
            else return null;
        }


    }
    [Serializable]
    class LinkPoint
    {
        private readonly (int width, int height) size = (8, 8);
        protected bool isDrawingLinkPoint = false;
        protected Link link;       
        protected Rectangle square;        
        protected bool used = false;
        protected bool input = true;
        public Point Layout { get; private set; }
        public Block Block { get; }
        public LinkPoint(Point layout, bool isInput, Block block)
        {
            square = new Rectangle(new Point(layout.X - (size.width/2), layout.Y - (size.height / 2)), new Size(size.width, size.height));
            Layout = layout;
            Block = block;
            isDrawingLinkPoint = input = isInput;
        }
        public LinkPoint(int x, int y, bool isInput, Block block)
        {
            square = new Rectangle(new Point(x - (size.width / 2), y - (size.height / 2)), new Size(size.width, size.height));
            Layout = new Point(x, y);
            Block = block;
            isDrawingLinkPoint = input = isInput;
        }
        public void Draw(Graphics canvas)
        {
            if (!used)
            {
                canvas.DrawEllipse(Pens.Black, square);
                canvas.FillEllipse(new SolidBrush(input ? Color.White : Color.Black), square);
            }
            else
            {
                if (isDrawingLinkPoint) link.Draw(canvas);
            }
        }
        // changes layout
        public void SetLayout(int x, int y)
        {
            Layout = new Point(x, y);
            square = new Rectangle(new Point(x - (size.width / 2), y - (size.height / 2)), new Size(size.width, size.height));
        }
        // removes link point and link 
        public void Remove()
        {
            if (link != null)
            {
                link.Remove();
                link = null;
            }
        }
        // checks if point was clicked
        public bool CheckPoint(int x, int y, bool isInput)
        {
            if (square.Left <= x && square.Right >= x && square.Top <= y && square.Bottom >= y && input == isInput && !used)
            {
                return true;
            }

            return false;
        }
        // sets link
        public void SetLink(Link link)
        {
            if(link != null)
            {
                this.link = link;
                used = true;
            }
            else
            {
                isDrawingLinkPoint = true;
                used = false;
            }
        }
        // link point draws link
        public void SetDrawingLinkPoint()
        {
            isDrawingLinkPoint = true;
            if(link != null) link.ChangeDrawingLinkPoint(this);

        }
        // link point does not draw link
        public void DeleteDrawingLinkPoint()
        {
            isDrawingLinkPoint = false;
        }
    }
    [Serializable]
    class Link
    {
        protected LinkPoint from;
        protected LinkPoint to;
        public Link(LinkPoint from, LinkPoint to)
        {
            this.from = from;
            this.to = to;
        }
        // draws link 
        public void Draw(Graphics canvas)
        {
            Pen pen = new Pen(Color.Black, 1);
            // create arrow cap
            System.Drawing.Drawing2D.GraphicsPath graphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
            Point[] points = new Point[3];
            points[0] = new Point(0, 0);
            points[1] = new Point(-4, -7);
            points[2] = new Point(4, -7);
            graphicsPath.AddPolygon(points);
            pen.CustomEndCap = new System.Drawing.Drawing2D.CustomLineCap(graphicsPath, null);
            canvas.DrawLine(pen, from.Layout.X, from.Layout.Y, to.Layout.X, to.Layout.Y);
        }
        // unlinks linkpoints and remove link
        public void Remove()
        {
            from.SetLink(null);
            to.SetLink(null);
        }
        // sets link point which draw link during drawing
        public void ChangeDrawingLinkPoint(LinkPoint linkPoint)
        {
            if(linkPoint == from)
            {
                to.DeleteDrawingLinkPoint();
            }
            else
            {
                from.DeleteDrawingLinkPoint();
            }
        }
    }
}
