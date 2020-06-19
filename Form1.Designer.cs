namespace BlockDiagramCreator
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.menuPanel = new System.Windows.Forms.TableLayoutPanel();
            this.FileBox = new System.Windows.Forms.GroupBox();
            this.fileButtonPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SaveScheme = new System.Windows.Forms.Button();
            this.NewSchemeButton = new System.Windows.Forms.Button();
            this.LoadScheme = new System.Windows.Forms.Button();
            this.EditingBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.startBoxButton = new System.Windows.Forms.Button();
            this.DecidingBoxButton = new System.Windows.Forms.Button();
            this.linkButton = new System.Windows.Forms.Button();
            this.endBoxButton = new System.Windows.Forms.Button();
            this.OperationBoxButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textSelectedBlock = new System.Windows.Forms.TextBox();
            this.textTextSelectedBlock = new System.Windows.Forms.Label();
            this.LanguageBox = new System.Windows.Forms.GroupBox();
            this.languageButtonPanel = new System.Windows.Forms.TableLayoutPanel();
            this.lanButtonEN = new System.Windows.Forms.Button();
            this.lanButtonPL = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.menuPanel.SuspendLayout();
            this.FileBox.SuspendLayout();
            this.fileButtonPanel.SuspendLayout();
            this.EditingBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.LanguageBox.SuspendLayout();
            this.languageButtonPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            resources.ApplyResources(this.menuPanel, "menuPanel");
            this.menuPanel.Controls.Add(this.FileBox, 0, 0);
            this.menuPanel.Controls.Add(this.EditingBox, 0, 1);
            this.menuPanel.Controls.Add(this.LanguageBox, 0, 2);
            this.menuPanel.Name = "menuPanel";
            // 
            // FileBox
            // 
            resources.ApplyResources(this.FileBox, "FileBox");
            this.FileBox.Controls.Add(this.fileButtonPanel);
            this.FileBox.Name = "FileBox";
            this.FileBox.TabStop = false;
            // 
            // fileButtonPanel
            // 
            resources.ApplyResources(this.fileButtonPanel, "fileButtonPanel");
            this.fileButtonPanel.CausesValidation = false;
            this.fileButtonPanel.Controls.Add(this.SaveScheme, 0, 1);
            this.fileButtonPanel.Controls.Add(this.NewSchemeButton, 0, 2);
            this.fileButtonPanel.Controls.Add(this.LoadScheme, 0, 0);
            this.fileButtonPanel.Name = "fileButtonPanel";
            // 
            // SaveScheme
            // 
            resources.ApplyResources(this.SaveScheme, "SaveScheme");
            this.SaveScheme.Name = "SaveScheme";
            this.SaveScheme.UseVisualStyleBackColor = true;
            this.SaveScheme.Click += new System.EventHandler(this.SaveScheme_Click);
            // 
            // NewSchemeButton
            // 
            resources.ApplyResources(this.NewSchemeButton, "NewSchemeButton");
            this.NewSchemeButton.Name = "NewSchemeButton";
            this.NewSchemeButton.UseVisualStyleBackColor = true;
            this.NewSchemeButton.Click += new System.EventHandler(this.NewShem_Click);
            // 
            // LoadScheme
            // 
            resources.ApplyResources(this.LoadScheme, "LoadScheme");
            this.LoadScheme.Name = "LoadScheme";
            this.LoadScheme.UseVisualStyleBackColor = true;
            this.LoadScheme.Click += new System.EventHandler(this.LoadScheme_Click);
            // 
            // EditingBox
            // 
            resources.ApplyResources(this.EditingBox, "EditingBox");
            this.EditingBox.Controls.Add(this.tableLayoutPanel1);
            this.EditingBox.Name = "EditingBox";
            this.EditingBox.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.startBoxButton);
            this.flowLayoutPanel1.Controls.Add(this.DecidingBoxButton);
            this.flowLayoutPanel1.Controls.Add(this.linkButton);
            this.flowLayoutPanel1.Controls.Add(this.endBoxButton);
            this.flowLayoutPanel1.Controls.Add(this.OperationBoxButton);
            this.flowLayoutPanel1.Controls.Add(this.deleteButton);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // startBoxButton
            // 
            resources.ApplyResources(this.startBoxButton, "startBoxButton");
            this.startBoxButton.BackColor = System.Drawing.SystemColors.Control;
            this.startBoxButton.Name = "startBoxButton";
            this.startBoxButton.UseVisualStyleBackColor = false;
            this.startBoxButton.Click += new System.EventHandler(this.BoxButton_Click);
            // 
            // DecidingBoxButton
            // 
            resources.ApplyResources(this.DecidingBoxButton, "DecidingBoxButton");
            this.DecidingBoxButton.Name = "DecidingBoxButton";
            this.DecidingBoxButton.UseVisualStyleBackColor = false;
            this.DecidingBoxButton.Click += new System.EventHandler(this.BoxButton_Click);
            // 
            // linkButton
            // 
            resources.ApplyResources(this.linkButton, "linkButton");
            this.linkButton.BackColor = System.Drawing.SystemColors.Control;
            this.linkButton.Name = "linkButton";
            this.linkButton.UseVisualStyleBackColor = false;
            this.linkButton.Click += new System.EventHandler(this.BoxButton_Click);
            // 
            // endBoxButton
            // 
            resources.ApplyResources(this.endBoxButton, "endBoxButton");
            this.endBoxButton.Name = "endBoxButton";
            this.endBoxButton.UseVisualStyleBackColor = false;
            this.endBoxButton.Click += new System.EventHandler(this.BoxButton_Click);
            // 
            // OperationBoxButton
            // 
            resources.ApplyResources(this.OperationBoxButton, "OperationBoxButton");
            this.OperationBoxButton.Name = "OperationBoxButton";
            this.OperationBoxButton.UseVisualStyleBackColor = false;
            this.OperationBoxButton.Click += new System.EventHandler(this.BoxButton_Click);
            // 
            // deleteButton
            // 
            resources.ApplyResources(this.deleteButton, "deleteButton");
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.BoxButton_Click);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.textSelectedBlock, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textTextSelectedBlock, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // textSelectedBlock
            // 
            resources.ApplyResources(this.textSelectedBlock, "textSelectedBlock");
            this.textSelectedBlock.Name = "textSelectedBlock";
            this.textSelectedBlock.TextChanged += new System.EventHandler(this.textSelectedBlock_TextChanged);
            // 
            // textTextSelectedBlock
            // 
            resources.ApplyResources(this.textTextSelectedBlock, "textTextSelectedBlock");
            this.textTextSelectedBlock.Name = "textTextSelectedBlock";
            // 
            // LanguageBox
            // 
            resources.ApplyResources(this.LanguageBox, "LanguageBox");
            this.LanguageBox.Controls.Add(this.languageButtonPanel);
            this.LanguageBox.Name = "LanguageBox";
            this.LanguageBox.TabStop = false;
            // 
            // languageButtonPanel
            // 
            resources.ApplyResources(this.languageButtonPanel, "languageButtonPanel");
            this.languageButtonPanel.Controls.Add(this.lanButtonEN, 0, 1);
            this.languageButtonPanel.Controls.Add(this.lanButtonPL, 0, 0);
            this.languageButtonPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.languageButtonPanel.Name = "languageButtonPanel";
            // 
            // lanButtonEN
            // 
            resources.ApplyResources(this.lanButtonEN, "lanButtonEN");
            this.lanButtonEN.Name = "lanButtonEN";
            this.lanButtonEN.UseVisualStyleBackColor = true;
            this.lanButtonEN.Click += new System.EventHandler(this.lanButtonEN_Click);
            // 
            // lanButtonPL
            // 
            resources.ApplyResources(this.lanButtonPL, "lanButtonPL");
            this.lanButtonPL.Name = "lanButtonPL";
            this.lanButtonPL.UseVisualStyleBackColor = true;
            this.lanButtonPL.Click += new System.EventHandler(this.lanButtonPL_Click);
            // 
            // mainPanel
            // 
            resources.ApplyResources(this.mainPanel, "mainPanel");
            this.mainPanel.Controls.Add(this.menuPanel, 1, 0);
            this.mainPanel.Controls.Add(this.pictureBoxPanel, 0, 0);
            this.mainPanel.Name = "mainPanel";
            // 
            // pictureBoxPanel
            // 
            resources.ApplyResources(this.pictureBoxPanel, "pictureBoxPanel");
            this.pictureBoxPanel.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxPanel.Name = "pictureBoxPanel";
            // 
            // mainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "mainForm";
            this.menuPanel.ResumeLayout(false);
            this.FileBox.ResumeLayout(false);
            this.fileButtonPanel.ResumeLayout(false);
            this.EditingBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.LanguageBox.ResumeLayout(false);
            this.languageButtonPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FileBox;
        private System.Windows.Forms.TableLayoutPanel fileButtonPanel;
        private System.Windows.Forms.Button SaveScheme;
        private System.Windows.Forms.Button NewSchemeButton;
        private System.Windows.Forms.Button LoadScheme;
        private System.Windows.Forms.GroupBox EditingBox;
        private System.Windows.Forms.TableLayoutPanel mainPanel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.FlowLayoutPanel pictureBoxPanel;
        private System.Windows.Forms.Button OperationBoxButton;
        private System.Windows.Forms.GroupBox LanguageBox;
        private System.Windows.Forms.TableLayoutPanel languageButtonPanel;
        private System.Windows.Forms.Button lanButtonEN;
        private System.Windows.Forms.Button lanButtonPL;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button startBoxButton;
        private System.Windows.Forms.Button linkButton;
        private System.Windows.Forms.Button endBoxButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textSelectedBlock;
        private System.Windows.Forms.Label textTextSelectedBlock;
        private System.Windows.Forms.Button DecidingBoxButton;
        private System.Windows.Forms.TableLayoutPanel menuPanel;
    }
}

