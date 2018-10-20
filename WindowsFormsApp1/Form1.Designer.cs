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
            this.findprinter_btn = new System.Windows.Forms.Button();
            this.ListOfPrintersListBox = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SendTestPage = new System.Windows.Forms.ToolStripMenuItem();
            this.QueueOfPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.sendFileToPrinterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PropertiesOfPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.print_grid_btn = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.PrintTheRainbowBtn = new System.Windows.Forms.Button();
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // findprinter_btn
            // 
            this.findprinter_btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.findprinter_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.findprinter_btn.Location = new System.Drawing.Point(11, 387);
            this.findprinter_btn.Name = "findprinter_btn";
            this.findprinter_btn.Size = new System.Drawing.Size(217, 58);
            this.findprinter_btn.TabIndex = 0;
            this.findprinter_btn.Text = "Find Printers";
            this.findprinter_btn.UseVisualStyleBackColor = true;
            this.findprinter_btn.Click += new System.EventHandler(this.FindThePrinterBtnClick);
            // 
            // ListOfPrintersListBox
            // 
            this.ListOfPrintersListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListOfPrintersListBox.ContextMenuStrip = this.contextMenuStrip1;
            this.ListOfPrintersListBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ListOfPrintersListBox.ForeColor = System.Drawing.Color.Black;
            this.ListOfPrintersListBox.FormattingEnabled = true;
            this.ListOfPrintersListBox.ItemHeight = 16;
            this.ListOfPrintersListBox.Location = new System.Drawing.Point(11, 7);
            this.ListOfPrintersListBox.Name = "ListOfPrintersListBox";
            this.ListOfPrintersListBox.Size = new System.Drawing.Size(541, 370);
            this.ListOfPrintersListBox.TabIndex = 1;
            this.ListOfPrintersListBox.SelectedIndexChanged += new System.EventHandler(this.ListOfPrintersChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SendTestPage,
            this.QueueOfPrinter,
            this.sendFileToPrinterToolStripMenuItem,
            this.PropertiesOfPrinter,
            this.DeletePrinter});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(177, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // SendTestPage
            // 
            this.SendTestPage.BackColor = System.Drawing.SystemColors.Menu;
            this.SendTestPage.Name = "SendTestPage";
            this.SendTestPage.Size = new System.Drawing.Size(176, 22);
            this.SendTestPage.Text = "Windows Test Page";
            this.SendTestPage.Click += new System.EventHandler(this.SendTestPage_Click);
            // 
            // QueueOfPrinter
            // 
            this.QueueOfPrinter.Name = "QueueOfPrinter";
            this.QueueOfPrinter.Size = new System.Drawing.Size(176, 22);
            this.QueueOfPrinter.Text = "Queue of Printer";
            this.QueueOfPrinter.Click += new System.EventHandler(this.QueueOfPrinter_Click);
            // 
            // sendFileToPrinterToolStripMenuItem
            // 
            this.sendFileToPrinterToolStripMenuItem.BackColor = System.Drawing.SystemColors.Menu;
            this.sendFileToPrinterToolStripMenuItem.Name = "sendFileToPrinterToolStripMenuItem";
            this.sendFileToPrinterToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.sendFileToPrinterToolStripMenuItem.Text = "Send File To Printer";
            this.sendFileToPrinterToolStripMenuItem.Click += new System.EventHandler(this.SendFileToPrinterToolStripMenuItem_Click);
            // 
            // PropertiesOfPrinter
            // 
            this.PropertiesOfPrinter.BackColor = System.Drawing.SystemColors.Window;
            this.PropertiesOfPrinter.Name = "PropertiesOfPrinter";
            this.PropertiesOfPrinter.Size = new System.Drawing.Size(176, 22);
            this.PropertiesOfPrinter.Text = "Properties";
            this.PropertiesOfPrinter.Click += new System.EventHandler(this.PropertiesToolStripMenuItem_Click);
            // 
            // DeletePrinter
            // 
            this.DeletePrinter.BackColor = System.Drawing.Color.Tomato;
            this.DeletePrinter.Name = "DeletePrinter";
            this.DeletePrinter.Size = new System.Drawing.Size(176, 22);
            this.DeletePrinter.Text = "Delete the Printer";
            this.DeletePrinter.Click += new System.EventHandler(this.DeleteThePrinterClick);
            // 
            // print_grid_btn
            // 
            this.print_grid_btn.Location = new System.Drawing.Point(6, 19);
            this.print_grid_btn.Name = "print_grid_btn";
            this.print_grid_btn.Size = new System.Drawing.Size(100, 34);
            this.print_grid_btn.TabIndex = 7;
            this.print_grid_btn.Text = "Print";
            this.print_grid_btn.UseVisualStyleBackColor = true;
            this.print_grid_btn.Click += new System.EventHandler(this.PrintTheGridBtnClick);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintTheGridDocument);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown1.Location = new System.Drawing.Point(112, 31);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(35, 21);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.Tag = "Copies";
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.NumericUpDownCopiesOfGridChanged);
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
            this.groupBox1.Controls.Add(this.print_grid_btn);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(396, 383);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 62);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Print the B/W Grid";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numericUpDown2);
            this.groupBox2.Controls.Add(this.PrintTheRainbowBtn);
            this.groupBox2.Location = new System.Drawing.Point(234, 383);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(156, 62);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Print the Rainbow";
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
            // numericUpDown2
            // 
            this.numericUpDown2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDown2.Location = new System.Drawing.Point(113, 32);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(35, 21);
            this.numericUpDown2.TabIndex = 1;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.NumericUpDown2TheRainbowCopiesChanged);
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
            // printDocument2
            // 
            this.printDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintTheRainbowPage);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 455);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ListOfPrintersListBox);
            this.Controls.Add(this.findprinter_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Printer Helper at 16.10.2018";
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button findprinter_btn;
        private System.Windows.Forms.Button print_grid_btn;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DeletePrinter;
        private System.Windows.Forms.ToolStripMenuItem QueueOfPrinter;
        private System.Windows.Forms.ToolStripMenuItem PropertiesOfPrinter;
        private System.Windows.Forms.ToolStripMenuItem SendTestPage;
        public System.Windows.Forms.ListBox ListOfPrintersListBox;
        private System.Windows.Forms.ToolStripMenuItem sendFileToPrinterToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button PrintTheRainbowBtn;
        private System.Drawing.Printing.PrintDocument printDocument2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
    }
}

