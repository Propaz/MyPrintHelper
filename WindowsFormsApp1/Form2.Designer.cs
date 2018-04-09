namespace PrinterParser
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.PrinterAdditionalProperty = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PrinterAdditionalProperty
            // 
            this.PrinterAdditionalProperty.FormattingEnabled = true;
            this.PrinterAdditionalProperty.Location = new System.Drawing.Point(12, 12);
            this.PrinterAdditionalProperty.Name = "PrinterAdditionalProperty";
            this.PrinterAdditionalProperty.Size = new System.Drawing.Size(514, 394);
            this.PrinterAdditionalProperty.TabIndex = 0;
            this.PrinterAdditionalProperty.SelectedIndexChanged += new System.EventHandler(this.PrinterAdvaicedProperties_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "Renew";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 455);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PrinterAdditionalProperty);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox PrinterAdditionalProperty;
        private System.Windows.Forms.Button button1;
    }
}