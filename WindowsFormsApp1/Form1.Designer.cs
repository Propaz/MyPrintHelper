namespace PrinterParser
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.FindPriners = new System.Windows.Forms.Button();
            this.ListOfPrintersListBox = new System.Windows.Forms.ListBox();
            this.contextMenuOfCommands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SendTestPage = new System.Windows.Forms.ToolStripMenuItem();
            this.QueueOfPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.sendFileToPrinterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertiesOfPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintBWGrid = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.BWGirdUpDownNumeric = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RinbowUpDownNumeric = new System.Windows.Forms.NumericUpDown();
            this.PrintTheRainbowBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ListOfColorsForPrint = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownTheSingleColor = new System.Windows.Forms.NumericUpDown();
            this.PrintTheColor = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.AddNewPrinter = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.StopPrintSpool = new System.Windows.Forms.Button();
            this.RestartPrintSpool = new System.Windows.Forms.Button();
            this.StartPrintSpool = new System.Windows.Forms.Button();
            this.contextMenuOfCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BWGirdUpDownNumeric)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RinbowUpDownNumeric)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTheSingleColor)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // FindPriners
            // 
            this.FindPriners.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.FindPriners.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FindPriners.Location = new System.Drawing.Point(6, 386);
            this.FindPriners.Name = "FindPriners";
            this.FindPriners.Size = new System.Drawing.Size(395, 62);
            this.FindPriners.TabIndex = 0;
            this.FindPriners.Text = "Find Printers";
            this.FindPriners.UseVisualStyleBackColor = true;
            this.FindPriners.Click += new System.EventHandler(this.FindThePrinterBtnClick);
            // 
            // ListOfPrintersListBox
            // 
            this.ListOfPrintersListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListOfPrintersListBox.ContextMenuStrip = this.contextMenuOfCommands;
            this.ListOfPrintersListBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ListOfPrintersListBox.ForeColor = System.Drawing.Color.Black;
            this.ListOfPrintersListBox.FormattingEnabled = true;
            this.ListOfPrintersListBox.ItemHeight = 16;
            this.ListOfPrintersListBox.Location = new System.Drawing.Point(6, 6);
            this.ListOfPrintersListBox.Name = "ListOfPrintersListBox";
            this.ListOfPrintersListBox.Size = new System.Drawing.Size(395, 370);
            this.ListOfPrintersListBox.TabIndex = 1;
            this.ListOfPrintersListBox.SelectedIndexChanged += new System.EventHandler(this.ListOfPrintersChanged);
            // 
            // contextMenuOfCommands
            // 
            this.contextMenuOfCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SendTestPage,
            this.QueueOfPrinter,
            this.sendFileToPrinterToolStripMenuItem,
            this.PropertiesOfPrinter,
            this.DeletePrinter});
            this.contextMenuOfCommands.Name = "contextMenuStrip1";
            this.contextMenuOfCommands.Size = new System.Drawing.Size(169, 114);
            this.contextMenuOfCommands.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // SendTestPage
            // 
            this.SendTestPage.BackColor = System.Drawing.SystemColors.Menu;
            this.SendTestPage.Name = "SendTestPage";
            this.SendTestPage.Size = new System.Drawing.Size(168, 22);
            this.SendTestPage.Text = "Windows Test Page";
            this.SendTestPage.Click += new System.EventHandler(this.SendTestPage_Click);
            // 
            // QueueOfPrinter
            // 
            this.QueueOfPrinter.Name = "QueueOfPrinter";
            this.QueueOfPrinter.Size = new System.Drawing.Size(168, 22);
            this.QueueOfPrinter.Text = "Queue of Printer";
            this.QueueOfPrinter.Click += new System.EventHandler(this.QueueOfPrinter_Click);
            // 
            // sendFileToPrinterToolStripMenuItem
            // 
            this.sendFileToPrinterToolStripMenuItem.BackColor = System.Drawing.SystemColors.Menu;
            this.sendFileToPrinterToolStripMenuItem.Name = "sendFileToPrinterToolStripMenuItem";
            this.sendFileToPrinterToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.sendFileToPrinterToolStripMenuItem.Text = "Send File To Printer";
            this.sendFileToPrinterToolStripMenuItem.Click += new System.EventHandler(this.SendFileToPrinterToolStripMenuItem_Click);
            // 
            // PropertiesOfPrinter
            // 
            this.PropertiesOfPrinter.BackColor = System.Drawing.SystemColors.Window;
            this.PropertiesOfPrinter.Name = "PropertiesOfPrinter";
            this.PropertiesOfPrinter.Size = new System.Drawing.Size(168, 22);
            this.PropertiesOfPrinter.Text = "Properties";
            this.PropertiesOfPrinter.Click += new System.EventHandler(this.PropertiesToolStripMenuItem_Click);
            // 
            // DeletePrinter
            // 
            this.DeletePrinter.BackColor = System.Drawing.Color.Tomato;
            this.DeletePrinter.Name = "DeletePrinter";
            this.DeletePrinter.Size = new System.Drawing.Size(168, 22);
            this.DeletePrinter.Text = "Delete the Printer";
            this.DeletePrinter.Click += new System.EventHandler(this.DeleteThePrinterClick);
            // 
            // PrintBWGrid
            // 
            this.PrintBWGrid.Location = new System.Drawing.Point(6, 18);
            this.PrintBWGrid.Name = "PrintBWGrid";
            this.PrintBWGrid.Size = new System.Drawing.Size(100, 34);
            this.PrintBWGrid.TabIndex = 7;
            this.PrintBWGrid.Text = "Print";
            this.PrintBWGrid.UseVisualStyleBackColor = true;
            this.PrintBWGrid.Click += new System.EventHandler(this.PrintTheGridBtnClick);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintTheGridDocument);
            // 
            // BWGirdUpDownNumeric
            // 
            this.BWGirdUpDownNumeric.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BWGirdUpDownNumeric.Location = new System.Drawing.Point(112, 31);
            this.BWGirdUpDownNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BWGirdUpDownNumeric.Name = "BWGirdUpDownNumeric";
            this.BWGirdUpDownNumeric.Size = new System.Drawing.Size(35, 21);
            this.BWGirdUpDownNumeric.TabIndex = 8;
            this.BWGirdUpDownNumeric.Tag = "Copies";
            this.BWGirdUpDownNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BWGirdUpDownNumeric.ValueChanged += new System.EventHandler(this.NumericUpDownCopiesOfGridChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Copies";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PrintBWGrid);
            this.groupBox1.Controls.Add(this.BWGirdUpDownNumeric);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(407, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 62);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Print the Grid Test";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.RinbowUpDownNumeric);
            this.groupBox2.Controls.Add(this.PrintTheRainbowBtn);
            this.groupBox2.Location = new System.Drawing.Point(407, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(156, 62);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Print the Rainbow Test";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Copies";
            // 
            // RinbowUpDownNumeric
            // 
            this.RinbowUpDownNumeric.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RinbowUpDownNumeric.Location = new System.Drawing.Point(113, 32);
            this.RinbowUpDownNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RinbowUpDownNumeric.Name = "RinbowUpDownNumeric";
            this.RinbowUpDownNumeric.Size = new System.Drawing.Size(35, 21);
            this.RinbowUpDownNumeric.TabIndex = 1;
            this.RinbowUpDownNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RinbowUpDownNumeric.ValueChanged += new System.EventHandler(this.NumericUpDown2TheRainbowCopiesChanged);
            // 
            // PrintTheRainbowBtn
            // 
            this.PrintTheRainbowBtn.Location = new System.Drawing.Point(7, 19);
            this.PrintTheRainbowBtn.Name = "PrintTheRainbowBtn";
            this.PrintTheRainbowBtn.Size = new System.Drawing.Size(100, 34);
            this.PrintTheRainbowBtn.TabIndex = 0;
            this.PrintTheRainbowBtn.Text = "Print";
            this.PrintTheRainbowBtn.UseVisualStyleBackColor = true;
            this.PrintTheRainbowBtn.Click += new System.EventHandler(this.PrintTheRainbowClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 11);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(577, 483);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.FindPriners);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.ListOfPrintersListBox);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(569, 457);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ListOfColorsForPrint);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.numericUpDownTheSingleColor);
            this.groupBox4.Controls.Add(this.PrintTheColor);
            this.groupBox4.Location = new System.Drawing.Point(407, 143);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(156, 91);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CMYK or RGB";
            // 
            // ListOfColorsForPrint
            // 
            this.ListOfColorsForPrint.FormattingEnabled = true;
            this.ListOfColorsForPrint.Items.AddRange(new object[] {
            "Black",
            "Cyan",
            "Magenta",
            "Yellow",
            "Red",
            "Green",
            "Blue"});
            this.ListOfColorsForPrint.Location = new System.Drawing.Point(7, 60);
            this.ListOfColorsForPrint.Name = "ListOfColorsForPrint";
            this.ListOfColorsForPrint.Size = new System.Drawing.Size(139, 21);
            this.ListOfColorsForPrint.TabIndex = 15;
            this.ListOfColorsForPrint.SelectedIndexChanged += new System.EventHandler(this.ListOfColorsForPrint_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Copies";
            // 
            // numericUpDownTheSingleColor
            // 
            this.numericUpDownTheSingleColor.Location = new System.Drawing.Point(111, 33);
            this.numericUpDownTheSingleColor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTheSingleColor.Name = "numericUpDownTheSingleColor";
            this.numericUpDownTheSingleColor.Size = new System.Drawing.Size(35, 20);
            this.numericUpDownTheSingleColor.TabIndex = 13;
            this.numericUpDownTheSingleColor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTheSingleColor.ValueChanged += new System.EventHandler(this.NumericUpDownTheSingleColor_ValueChanged);
            // 
            // PrintTheColor
            // 
            this.PrintTheColor.Location = new System.Drawing.Point(6, 19);
            this.PrintTheColor.Name = "PrintTheColor";
            this.PrintTheColor.Size = new System.Drawing.Size(100, 34);
            this.PrintTheColor.TabIndex = 12;
            this.PrintTheColor.Text = "Print";
            this.PrintTheColor.UseVisualStyleBackColor = true;
            this.PrintTheColor.Click += new System.EventHandler(this.PrintTheColor_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.AddNewPrinter);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(569, 457);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Print Spool";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // AddNewPrinter
            // 
            this.AddNewPrinter.Location = new System.Drawing.Point(408, 25);
            this.AddNewPrinter.Name = "AddNewPrinter";
            this.AddNewPrinter.Size = new System.Drawing.Size(120, 35);
            this.AddNewPrinter.TabIndex = 5;
            this.AddNewPrinter.Text = "Add New Printer";
            this.AddNewPrinter.UseVisualStyleBackColor = true;
            this.AddNewPrinter.Click += new System.EventHandler(this.AddNewPrinter_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.StopPrintSpool);
            this.groupBox3.Controls.Add(this.RestartPrintSpool);
            this.groupBox3.Controls.Add(this.StartPrintSpool);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(385, 69);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Print Spool";
            // 
            // StopPrintSpool
            // 
            this.StopPrintSpool.Location = new System.Drawing.Point(258, 19);
            this.StopPrintSpool.Name = "StopPrintSpool";
            this.StopPrintSpool.Size = new System.Drawing.Size(120, 35);
            this.StopPrintSpool.TabIndex = 4;
            this.StopPrintSpool.Text = "Stop Print Spool";
            this.StopPrintSpool.UseVisualStyleBackColor = true;
            this.StopPrintSpool.Click += new System.EventHandler(this.StopPrintSpool_Click);
            // 
            // RestartPrintSpool
            // 
            this.RestartPrintSpool.Location = new System.Drawing.Point(6, 19);
            this.RestartPrintSpool.Name = "RestartPrintSpool";
            this.RestartPrintSpool.Size = new System.Drawing.Size(120, 35);
            this.RestartPrintSpool.TabIndex = 0;
            this.RestartPrintSpool.Text = "Restart Print Spool";
            this.RestartPrintSpool.UseVisualStyleBackColor = true;
            this.RestartPrintSpool.Click += new System.EventHandler(this.RestartPrintSpool_Click);
            // 
            // StartPrintSpool
            // 
            this.StartPrintSpool.Location = new System.Drawing.Point(132, 19);
            this.StartPrintSpool.Name = "StartPrintSpool";
            this.StartPrintSpool.Size = new System.Drawing.Size(120, 35);
            this.StartPrintSpool.TabIndex = 3;
            this.StartPrintSpool.Text = "Start Print Spool ";
            this.StartPrintSpool.UseVisualStyleBackColor = true;
            this.StartPrintSpool.Click += new System.EventHandler(this.StartPrintSpool_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 501);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Printer Helper";
            this.contextMenuOfCommands.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BWGirdUpDownNumeric)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RinbowUpDownNumeric)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTheSingleColor)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button FindPriners;
        private System.Windows.Forms.Button PrintBWGrid;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ContextMenuStrip contextMenuOfCommands;
        private System.Windows.Forms.ToolStripMenuItem DeletePrinter;
        private System.Windows.Forms.ToolStripMenuItem QueueOfPrinter;
        private System.Windows.Forms.ToolStripMenuItem PropertiesOfPrinter;
        private System.Windows.Forms.ToolStripMenuItem SendTestPage;
        public System.Windows.Forms.ListBox ListOfPrintersListBox;
        private System.Windows.Forms.ToolStripMenuItem sendFileToPrinterToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown BWGirdUpDownNumeric;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button PrintTheRainbowBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown RinbowUpDownNumeric;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button RestartPrintSpool;
        private System.Windows.Forms.Button StartPrintSpool;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button StopPrintSpool;
        private System.Windows.Forms.Button AddNewPrinter;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button PrintTheColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownTheSingleColor;
        private System.Windows.Forms.ComboBox ListOfColorsForPrint;
    }
}

