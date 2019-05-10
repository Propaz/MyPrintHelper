namespace PrinterHelper
{
    partial class Frpogui
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
            this.SendFRPOcommand = new System.Windows.Forms.Button();
            this.Label_SelectedPrinterName = new System.Windows.Forms.Label();
            this.ComboBoxOfCommands = new System.Windows.Forms.ComboBox();
            this.TextBoxCustomFRPOCommand = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SendScriptToPrinter = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SendFRPOcommand
            // 
            this.SendFRPOcommand.Location = new System.Drawing.Point(228, 126);
            this.SendFRPOcommand.Name = "SendFRPOcommand";
            this.SendFRPOcommand.Size = new System.Drawing.Size(156, 45);
            this.SendFRPOcommand.TabIndex = 0;
            this.SendFRPOcommand.Text = "Send FRPO Command";
            this.SendFRPOcommand.UseVisualStyleBackColor = true;
            this.SendFRPOcommand.Click += new System.EventHandler(this.SendFRPO_Command_Click);
            // 
            // Label_SelectedPrinterName
            // 
            this.Label_SelectedPrinterName.AutoSize = true;
            this.Label_SelectedPrinterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label_SelectedPrinterName.Location = new System.Drawing.Point(12, 9);
            this.Label_SelectedPrinterName.Name = "Label_SelectedPrinterName";
            this.Label_SelectedPrinterName.Size = new System.Drawing.Size(100, 16);
            this.Label_SelectedPrinterName.TabIndex = 1;
            this.Label_SelectedPrinterName.Text = "SelectedPrinter";
            // 
            // ComboBoxOfCommands
            // 
            this.ComboBoxOfCommands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOfCommands.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ComboBoxOfCommands.FormattingEnabled = true;
            this.ComboBoxOfCommands.Items.AddRange(new object[] {
            "Event Log",
            "Status Page",
            "Draw Arc",
            "Fill Pattern",
            "Margin Test"});
            this.ComboBoxOfCommands.Location = new System.Drawing.Point(12, 126);
            this.ComboBoxOfCommands.Name = "ComboBoxOfCommands";
            this.ComboBoxOfCommands.Size = new System.Drawing.Size(210, 21);
            this.ComboBoxOfCommands.TabIndex = 2;
            // 
            // TextBoxCustomFRPOCommand
            // 
            this.TextBoxCustomFRPOCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxCustomFRPOCommand.Location = new System.Drawing.Point(6, 29);
            this.TextBoxCustomFRPOCommand.Name = "TextBoxCustomFRPOCommand";
            this.TextBoxCustomFRPOCommand.Size = new System.Drawing.Size(360, 26);
            this.TextBoxCustomFRPOCommand.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextBoxCustomFRPOCommand);
            this.groupBox1.Location = new System.Drawing.Point(12, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 71);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Custom FRPO Command";
            // 
            // SendScriptToPrinter
            // 
            this.SendScriptToPrinter.Location = new System.Drawing.Point(228, 178);
            this.SendScriptToPrinter.Name = "SendScriptToPrinter";
            this.SendScriptToPrinter.Size = new System.Drawing.Size(156, 42);
            this.SendScriptToPrinter.TabIndex = 5;
            this.SendScriptToPrinter.Text = "Send script File";
            this.SendScriptToPrinter.UseVisualStyleBackColor = true;
            this.SendScriptToPrinter.Click += new System.EventHandler(this.SendScriptToPrinter_Click);
            // 
            // Frpogui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 243);
            this.Controls.Add(this.SendScriptToPrinter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ComboBoxOfCommands);
            this.Controls.Add(this.Label_SelectedPrinterName);
            this.Controls.Add(this.SendFRPOcommand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Frpogui";
            this.Text = "frpogui";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SendFRPOcommand;
        private System.Windows.Forms.Label Label_SelectedPrinterName;
        private System.Windows.Forms.ComboBox ComboBoxOfCommands;
        private System.Windows.Forms.TextBox TextBoxCustomFRPOCommand;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button SendScriptToPrinter;
    }
}