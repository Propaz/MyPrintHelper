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
            this.renewButton1 = new System.Windows.Forms.Button();
            this.ListOfAdditionalPrinterProperties = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // renewButton1
            // 
            this.renewButton1.Location = new System.Drawing.Point(12, 412);
            this.renewButton1.Name = "renewButton1";
            this.renewButton1.Size = new System.Drawing.Size(103, 38);
            this.renewButton1.TabIndex = 1;
            this.renewButton1.Text = "Renew";
            this.renewButton1.UseVisualStyleBackColor = true;
            this.renewButton1.Click += new System.EventHandler(this.GetAdditionalPrinterPropertiesClick);
            // 
            // ListOfAdditionalPrinterProperties
            // 
            this.ListOfAdditionalPrinterProperties.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListOfAdditionalPrinterProperties.Location = new System.Drawing.Point(13, 13);
            this.ListOfAdditionalPrinterProperties.Name = "ListOfAdditionalPrinterProperties";
            this.ListOfAdditionalPrinterProperties.Size = new System.Drawing.Size(513, 393);
            this.ListOfAdditionalPrinterProperties.TabIndex = 2;
            this.ListOfAdditionalPrinterProperties.UseCompatibleStateImageBehavior = false;
            this.ListOfAdditionalPrinterProperties.SelectedIndexChanged += new System.EventHandler(this.ListOfAdditionalPropertiesOfPrinterChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 455);
            this.Controls.Add(this.ListOfAdditionalPrinterProperties);
            this.Controls.Add(this.renewButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button renewButton1;
        private System.Windows.Forms.ListView ListOfAdditionalPrinterProperties;
    }
}