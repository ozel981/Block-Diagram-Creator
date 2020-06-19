namespace BlockDiagramCreator
{
    partial class newSchemeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(newSchemeForm));
            this.widthLabel = new System.Windows.Forms.Label();
            this.heightLabel = new System.Windows.Forms.Label();
            this.widthBox = new System.Windows.Forms.NumericUpDown();
            this.heightBox = new System.Windows.Forms.NumericUpDown();
            this.newSchemeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.widthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightBox)).BeginInit();
            this.SuspendLayout();
            // 
            // widthLabel
            // 
            resources.ApplyResources(this.widthLabel, "widthLabel");
            this.widthLabel.Name = "widthLabel";
            // 
            // heightLabel
            // 
            resources.ApplyResources(this.heightLabel, "heightLabel");
            this.heightLabel.Name = "heightLabel";
            // 
            // widthBox
            // 
            resources.ApplyResources(this.widthBox, "widthBox");
            this.widthBox.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.widthBox.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.widthBox.Name = "widthBox";
            this.widthBox.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // heightBox
            // 
            resources.ApplyResources(this.heightBox, "heightBox");
            this.heightBox.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.heightBox.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.heightBox.Name = "heightBox";
            this.heightBox.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // newSchemeButton
            // 
            resources.ApplyResources(this.newSchemeButton, "newSchemeButton");
            this.newSchemeButton.Name = "newSchemeButton";
            this.newSchemeButton.UseVisualStyleBackColor = true;
            this.newSchemeButton.Click += new System.EventHandler(this.newSchemeButton_Click);
            // 
            // newSchemeForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.newSchemeButton);
            this.Controls.Add(this.heightBox);
            this.Controls.Add(this.widthBox);
            this.Controls.Add(this.heightLabel);
            this.Controls.Add(this.widthLabel);
            this.Name = "newSchemeForm";
            ((System.ComponentModel.ISupportInitialize)(this.widthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Label heightLabel;
        private System.Windows.Forms.NumericUpDown widthBox;
        private System.Windows.Forms.NumericUpDown heightBox;
        private System.Windows.Forms.Button newSchemeButton;
    }
}