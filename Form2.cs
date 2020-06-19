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

namespace BlockDiagramCreator
{
    public partial class newSchemeForm : Form
    {
        mainForm parent;
        public newSchemeForm(mainForm parent)
        {
            InitializeComponent();
            this.parent = parent;
        }
        // create new scheme
        private void newSchemeButton_Click(object sender, EventArgs e)
        {
            parent.NewScheme((int)widthBox.Value, (int)heightBox.Value);
            Close();
        }
    }
}
