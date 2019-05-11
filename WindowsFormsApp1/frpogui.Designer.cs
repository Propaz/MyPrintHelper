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
            this.SendCustomFRPOcommand = new System.Windows.Forms.Button();
            this.Label_SelectedPrinterName = new System.Windows.Forms.Label();
            this.ComboBoxOfCommands = new System.Windows.Forms.ComboBox();
            this.TextBoxCustomFRPOCommand = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextBoxClearButton = new System.Windows.Forms.Button();
            this.SendScriptToPrinter = new System.Windows.Forms.Button();
            this.SendFRPOCommandFromList = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SendCustomFRPOcommand
            // 
            this.SendCustomFRPOcommand.Location = new System.Drawing.Point(244, 51);
            this.SendCustomFRPOcommand.Name = "SendCustomFRPOcommand";
            this.SendCustomFRPOcommand.Size = new System.Drawing.Size(144, 40);
            this.SendCustomFRPOcommand.TabIndex = 0;
            this.SendCustomFRPOcommand.Text = "Send FRPO Command";
            this.SendCustomFRPOcommand.UseVisualStyleBackColor = true;
            this.SendCustomFRPOcommand.Click += new System.EventHandler(this.SendCustomFRPOcommand_Click);
            // 
            // Label_SelectedPrinterName
            // 
            this.Label_SelectedPrinterName.AutoSize = true;
            this.Label_SelectedPrinterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Label_SelectedPrinterName.Location = new System.Drawing.Point(12, 9);
            this.Label_SelectedPrinterName.Name = "Label_SelectedPrinterName";
            this.Label_SelectedPrinterName.Size = new System.Drawing.Size(115, 16);
            this.Label_SelectedPrinterName.TabIndex = 1;
            this.Label_SelectedPrinterName.Text = "SelectedPrinter";
            // 
            // ComboBoxOfCommands
            // 
            this.ComboBoxOfCommands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxOfCommands.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ComboBoxOfCommands.FormattingEnabled = true;
            this.ComboBoxOfCommands.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.ComboBoxOfCommands.Items.AddRange(new object[] {
            "Check Disk",
            "Dashed Pattern",
            "Draw Arc",
            "Draw Circle",
            "Draw Cube",
            "Draw Filled-in Block",
            "Event Log",
            "Fill Closed Path",
            "Fill Pattern",
            "Font List",
            "FRPO INIT",
            "Margin Test",
            "Simple Color Palette",
            "Status Page"});
            this.ComboBoxOfCommands.Location = new System.Drawing.Point(12, 152);
            this.ComboBoxOfCommands.Name = "ComboBoxOfCommands";
            this.ComboBoxOfCommands.Size = new System.Drawing.Size(225, 21);
            this.ComboBoxOfCommands.Sorted = true;
            this.ComboBoxOfCommands.TabIndex = 2;
            // 
            // TextBoxCustomFRPOCommand
            // 
            this.TextBoxCustomFRPOCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextBoxCustomFRPOCommand.Location = new System.Drawing.Point(6, 19);
            this.TextBoxCustomFRPOCommand.Name = "TextBoxCustomFRPOCommand";
            this.TextBoxCustomFRPOCommand.Size = new System.Drawing.Size(347, 26);
            this.TextBoxCustomFRPOCommand.TabIndex = 3;
            this.TextBoxCustomFRPOCommand.TextChanged += new System.EventHandler(this.TextBoxCustomFRPOCommand_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextBoxClearButton);
            this.groupBox1.Controls.Add(this.SendScriptToPrinter);
            this.groupBox1.Controls.Add(this.TextBoxCustomFRPOCommand);
            this.groupBox1.Controls.Add(this.SendCustomFRPOcommand);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 105);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Custom FRPO Command";
            // 
            // TextBoxClearButton
            // 
            this.TextBoxClearButton.Location = new System.Drawing.Point(359, 19);
            this.TextBoxClearButton.Name = "TextBoxClearButton";
            this.TextBoxClearButton.Size = new System.Drawing.Size(29, 26);
            this.TextBoxClearButton.TabIndex = 4;
            this.TextBoxClearButton.Text = "X";
            this.TextBoxClearButton.UseVisualStyleBackColor = true;
            this.TextBoxClearButton.Click += new System.EventHandler(this.TextBoxClearButton_Click);
            // 
            // SendScriptToPrinter
            // 
            this.SendScriptToPrinter.Location = new System.Drawing.Point(6, 51);
            this.SendScriptToPrinter.Name = "SendScriptToPrinter";
            this.SendScriptToPrinter.Size = new System.Drawing.Size(144, 40);
            this.SendScriptToPrinter.TabIndex = 5;
            this.SendScriptToPrinter.Text = "Send Script File";
            this.SendScriptToPrinter.UseVisualStyleBackColor = true;
            this.SendScriptToPrinter.Click += new System.EventHandler(this.SendScriptToPrinter_Click);
            // 
            // SendFRPOCommandFromList
            // 
            this.SendFRPOCommandFromList.Location = new System.Drawing.Point(256, 152);
            this.SendFRPOCommandFromList.Name = "SendFRPOCommandFromList";
            this.SendFRPOCommandFromList.Size = new System.Drawing.Size(144, 40);
            this.SendFRPOCommandFromList.TabIndex = 6;
            this.SendFRPOCommandFromList.Text = "Send FRPO Command";
            this.SendFRPOCommandFromList.UseVisualStyleBackColor = true;
            this.SendFRPOCommandFromList.Click += new System.EventHandler(this.SendFRPOCommandFromList_Click);
            // 
            // Frpogui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 208);
            this.Controls.Add(this.SendFRPOCommandFromList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Label_SelectedPrinterName);
            this.Controls.Add(this.ComboBoxOfCommands);
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

        private System.Windows.Forms.Button SendCustomFRPOcommand;
        private System.Windows.Forms.Label Label_SelectedPrinterName;
        private System.Windows.Forms.ComboBox ComboBoxOfCommands;
        private System.Windows.Forms.TextBox TextBoxCustomFRPOCommand;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button SendScriptToPrinter;
        private System.Windows.Forms.Button SendFRPOCommandFromList;
        private System.Windows.Forms.Button TextBoxClearButton;
    }
}