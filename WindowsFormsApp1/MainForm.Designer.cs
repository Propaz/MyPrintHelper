namespace PrinterHelper
{
    partial class MainForm
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
            this.FindPrinters = new System.Windows.Forms.Button();
            this.ListOfPrintersListBox = new System.Windows.Forms.ListBox();
            this.contextMenuOfCommands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SendTestPage = new System.Windows.Forms.ToolStripMenuItem();
            this.QueueOfPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.sendFileToPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertiesOfPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.fRPOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintBWGrid = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.GridTestCopies = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RainbowTestPageCopies = new System.Windows.Forms.NumericUpDown();
            this.PrintTheRainbowBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ListOfColorsForPrint = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SingleColorTestPageCopies = new System.Windows.Forms.NumericUpDown();
            this.PrintTheColor = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.PrintServerProperties = new System.Windows.Forms.Button();
            this.AddNewPrinter = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.StopPrintSpool = new System.Windows.Forms.Button();
            this.RestartPrintSpool = new System.Windows.Forms.Button();
            this.StartPrintSpool = new System.Windows.Forms.Button();
            this.contextMenuOfCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridTestCopies)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RainbowTestPageCopies)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SingleColorTestPageCopies)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // FindPrinters
            // 
            this.FindPrinters.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.FindPrinters.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FindPrinters.Location = new System.Drawing.Point(498, 314);
            this.FindPrinters.Name = "FindPrinters";
            this.FindPrinters.Size = new System.Drawing.Size(173, 62);
            this.FindPrinters.TabIndex = 0;
            this.FindPrinters.Text = "Find Printers (F5)";
            this.FindPrinters.UseVisualStyleBackColor = true;
            this.FindPrinters.Click += new System.EventHandler(this.FindThePrinterBtnClick);
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
            this.ListOfPrintersListBox.Size = new System.Drawing.Size(486, 370);
            this.ListOfPrintersListBox.Sorted = true;
            this.ListOfPrintersListBox.TabIndex = 1;
            this.ListOfPrintersListBox.SelectedIndexChanged += new System.EventHandler(this.ListOfPrintersListBox_SelectedIndexChanged);
            // 
            // contextMenuOfCommands
            // 
            this.contextMenuOfCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SendTestPage,
            this.QueueOfPrinter,
            this.sendFileToPrinter,
            this.PropertiesOfPrinter,
            this.DeletePrinter,
            this.fRPOToolStripMenuItem});
            this.contextMenuOfCommands.Name = "contextMenuStrip1";
            this.contextMenuOfCommands.Size = new System.Drawing.Size(176, 136);
            this.contextMenuOfCommands.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // SendTestPage
            // 
            this.SendTestPage.BackColor = System.Drawing.SystemColors.Menu;
            this.SendTestPage.Name = "SendTestPage";
            this.SendTestPage.Size = new System.Drawing.Size(175, 22);
            this.SendTestPage.Text = "Windows Test Page";
            this.SendTestPage.Click += new System.EventHandler(this.SendTestPage_Click);
            // 
            // QueueOfPrinter
            // 
            this.QueueOfPrinter.Name = "QueueOfPrinter";
            this.QueueOfPrinter.Size = new System.Drawing.Size(175, 22);
            this.QueueOfPrinter.Text = "Queue of Printer";
            this.QueueOfPrinter.Click += new System.EventHandler(this.QueueOfPrinter_Click);
            // 
            // sendFileToPrinter
            // 
            this.sendFileToPrinter.BackColor = System.Drawing.SystemColors.Menu;
            this.sendFileToPrinter.Name = "sendFileToPrinter";
            this.sendFileToPrinter.Size = new System.Drawing.Size(175, 22);
            this.sendFileToPrinter.Text = "Send File To Printer";
            this.sendFileToPrinter.Click += new System.EventHandler(this.SendFileToPrinter);
            // 
            // PropertiesOfPrinter
            // 
            this.PropertiesOfPrinter.BackColor = System.Drawing.SystemColors.Window;
            this.PropertiesOfPrinter.Name = "PropertiesOfPrinter";
            this.PropertiesOfPrinter.Size = new System.Drawing.Size(175, 22);
            this.PropertiesOfPrinter.Text = "Properties";
            this.PropertiesOfPrinter.Click += new System.EventHandler(this.PropertiesToolStripMenuItem_Click);
            // 
            // DeletePrinter
            // 
            this.DeletePrinter.BackColor = System.Drawing.Color.Tomato;
            this.DeletePrinter.Name = "DeletePrinter";
            this.DeletePrinter.Size = new System.Drawing.Size(175, 22);
            this.DeletePrinter.Text = "Delete the Printer";
            this.DeletePrinter.Click += new System.EventHandler(this.DeleteThePrinterClick);
            // 
            // fRPOToolStripMenuItem
            // 
            this.fRPOToolStripMenuItem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fRPOToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.fRPOToolStripMenuItem.Name = "fRPOToolStripMenuItem";
            this.fRPOToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.fRPOToolStripMenuItem.Text = "FRPO";
            this.fRPOToolStripMenuItem.Click += new System.EventHandler(this.FRPOToolStripMenuItem_Click);
            // 
            // PrintBWGrid
            // 
            this.PrintBWGrid.Location = new System.Drawing.Point(6, 18);
            this.PrintBWGrid.Name = "PrintBWGrid";
            this.PrintBWGrid.Size = new System.Drawing.Size(116, 34);
            this.PrintBWGrid.TabIndex = 7;
            this.PrintBWGrid.Text = "Print";
            this.PrintBWGrid.UseVisualStyleBackColor = true;
            this.PrintBWGrid.Click += new System.EventHandler(this.PrintTheGridBtnClick);
            // 
            // GridTestCopies
            // 
            this.GridTestCopies.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GridTestCopies.Location = new System.Drawing.Point(132, 31);
            this.GridTestCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GridTestCopies.Name = "GridTestCopies";
            this.GridTestCopies.Size = new System.Drawing.Size(35, 21);
            this.GridTestCopies.TabIndex = 8;
            this.GridTestCopies.Tag = "Copies";
            this.GridTestCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Copies";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PrintBWGrid);
            this.groupBox1.Controls.Add(this.GridTestCopies);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(498, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 62);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Print the Grid Test";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.RainbowTestPageCopies);
            this.groupBox2.Controls.Add(this.PrintTheRainbowBtn);
            this.groupBox2.Location = new System.Drawing.Point(498, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(173, 62);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Print the Rainbow Test";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Copies";
            // 
            // RainbowTestPageCopies
            // 
            this.RainbowTestPageCopies.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RainbowTestPageCopies.Location = new System.Drawing.Point(131, 32);
            this.RainbowTestPageCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RainbowTestPageCopies.Name = "RainbowTestPageCopies";
            this.RainbowTestPageCopies.Size = new System.Drawing.Size(35, 21);
            this.RainbowTestPageCopies.TabIndex = 1;
            this.RainbowTestPageCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // PrintTheRainbowBtn
            // 
            this.PrintTheRainbowBtn.Location = new System.Drawing.Point(7, 19);
            this.PrintTheRainbowBtn.Name = "PrintTheRainbowBtn";
            this.PrintTheRainbowBtn.Size = new System.Drawing.Size(114, 34);
            this.PrintTheRainbowBtn.TabIndex = 0;
            this.PrintTheRainbowBtn.Text = "Print";
            this.PrintTheRainbowBtn.UseVisualStyleBackColor = true;
            this.PrintTheRainbowBtn.Click += new System.EventHandler(this.PrintTheRainbowClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 11);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(685, 413);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.FindPrinters);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.ListOfPrintersListBox);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(677, 387);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ListOfColorsForPrint);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.SingleColorTestPageCopies);
            this.groupBox4.Controls.Add(this.PrintTheColor);
            this.groupBox4.Location = new System.Drawing.Point(498, 142);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(173, 91);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "CMYK or RGB Test";
            // 
            // ListOfColorsForPrint
            // 
            this.ListOfColorsForPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ListOfColorsForPrint.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ListOfColorsForPrint.FormattingEnabled = true;
            this.ListOfColorsForPrint.Items.AddRange(new object[] {
            "Black",
            "Cyan",
            "Magenta",
            "Yellow",
            "Red",
            "Green",
            "Blue",
            "White"});
            this.ListOfColorsForPrint.Location = new System.Drawing.Point(7, 60);
            this.ListOfColorsForPrint.Name = "ListOfColorsForPrint";
            this.ListOfColorsForPrint.Size = new System.Drawing.Size(158, 21);
            this.ListOfColorsForPrint.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Copies";
            // 
            // SingleColorTestPageCopies
            // 
            this.SingleColorTestPageCopies.Location = new System.Drawing.Point(130, 32);
            this.SingleColorTestPageCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SingleColorTestPageCopies.Name = "SingleColorTestPageCopies";
            this.SingleColorTestPageCopies.Size = new System.Drawing.Size(35, 20);
            this.SingleColorTestPageCopies.TabIndex = 13;
            this.SingleColorTestPageCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // PrintTheColor
            // 
            this.PrintTheColor.Location = new System.Drawing.Point(6, 19);
            this.PrintTheColor.Name = "PrintTheColor";
            this.PrintTheColor.Size = new System.Drawing.Size(115, 34);
            this.PrintTheColor.TabIndex = 12;
            this.PrintTheColor.Text = "Print";
            this.PrintTheColor.UseVisualStyleBackColor = true;
            this.PrintTheColor.Click += new System.EventHandler(this.PrintTheColor_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.PrintServerProperties);
            this.tabPage2.Controls.Add(this.AddNewPrinter);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(677, 387);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Print Spool";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // PrintServerProperties
            // 
            this.PrintServerProperties.Location = new System.Drawing.Point(138, 81);
            this.PrintServerProperties.Name = "PrintServerProperties";
            this.PrintServerProperties.Size = new System.Drawing.Size(120, 35);
            this.PrintServerProperties.TabIndex = 6;
            this.PrintServerProperties.Text = "PrintServer Properties";
            this.PrintServerProperties.UseVisualStyleBackColor = true;
            this.PrintServerProperties.Click += new System.EventHandler(this.GetPrintServerProperties);
            // 
            // AddNewPrinter
            // 
            this.AddNewPrinter.Location = new System.Drawing.Point(12, 81);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 430);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Printer Helper";
            this.contextMenuOfCommands.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridTestCopies)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RainbowTestPageCopies)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SingleColorTestPageCopies)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button FindPrinters;
        private System.Windows.Forms.Button PrintBWGrid;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ContextMenuStrip contextMenuOfCommands;
        private System.Windows.Forms.ToolStripMenuItem DeletePrinter;
        private System.Windows.Forms.ToolStripMenuItem QueueOfPrinter;
        private System.Windows.Forms.ToolStripMenuItem PropertiesOfPrinter;
        private System.Windows.Forms.ToolStripMenuItem SendTestPage;
        public System.Windows.Forms.ListBox ListOfPrintersListBox;
        private System.Windows.Forms.ToolStripMenuItem sendFileToPrinter;
        private System.Windows.Forms.NumericUpDown GridTestCopies;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button PrintTheRainbowBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown RainbowTestPageCopies;
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
        private System.Windows.Forms.NumericUpDown SingleColorTestPageCopies;
        private System.Windows.Forms.ComboBox ListOfColorsForPrint;
        private System.Windows.Forms.Button PrintServerProperties;
        private System.Windows.Forms.ToolStripMenuItem fRPOToolStripMenuItem;
    }
}

