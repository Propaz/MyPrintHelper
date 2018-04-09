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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PropertiesOfPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.SendTestPage = new System.Windows.Forms.ToolStripMenuItem();
            this.QueueOfPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletePrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.AdditionalPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.print_grid_btn = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // findprinter_btn
            // 
            this.findprinter_btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.findprinter_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.findprinter_btn.Location = new System.Drawing.Point(12, 367);
            this.findprinter_btn.Name = "findprinter_btn";
            this.findprinter_btn.Size = new System.Drawing.Size(410, 34);
            this.findprinter_btn.TabIndex = 0;
            this.findprinter_btn.Text = "Find Printers";
            this.findprinter_btn.UseVisualStyleBackColor = true;
            this.findprinter_btn.Click += new System.EventHandler(this.FindprinterBtnClick);
            // 
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.ForeColor = System.Drawing.Color.Black;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(12, 23);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(516, 338);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PropertiesOfPrinter,
            this.SendTestPage,
            this.QueueOfPrinter,
            this.DeletePrinter,
            this.AdditionalPropertiesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(186, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1_Opening);
            // 
            // PropertiesOfPrinter
            // 
            this.PropertiesOfPrinter.Name = "PropertiesOfPrinter";
            this.PropertiesOfPrinter.Size = new System.Drawing.Size(185, 22);
            this.PropertiesOfPrinter.Text = "Properties";
            this.PropertiesOfPrinter.Click += new System.EventHandler(this.PropertiesToolStripMenuItem_Click);
            // 
            // SendTestPage
            // 
            this.SendTestPage.Name = "SendTestPage";
            this.SendTestPage.Size = new System.Drawing.Size(185, 22);
            this.SendTestPage.Text = "Windows Test Page";
            this.SendTestPage.Click += new System.EventHandler(this.SendTestPage_Click);
            // 
            // QueueOfPrinter
            // 
            this.QueueOfPrinter.Name = "QueueOfPrinter";
            this.QueueOfPrinter.Size = new System.Drawing.Size(185, 22);
            this.QueueOfPrinter.Text = "Queue of Printer";
            this.QueueOfPrinter.Click += new System.EventHandler(this.QueueOfPrinter_Click);
            // 
            // DeletePrinter
            // 
            this.DeletePrinter.Name = "DeletePrinter";
            this.DeletePrinter.Size = new System.Drawing.Size(185, 22);
            this.DeletePrinter.Text = "Delete the Printer";
            this.DeletePrinter.Click += new System.EventHandler(this.Deleteprinter_Click);
            // 
            // AdditionalPropertiesToolStripMenuItem
            // 
            this.AdditionalPropertiesToolStripMenuItem.Name = "AdditionalPropertiesToolStripMenuItem";
            this.AdditionalPropertiesToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.AdditionalPropertiesToolStripMenuItem.Text = "Additional Properties";
            this.AdditionalPropertiesToolStripMenuItem.Click += new System.EventHandler(this.AdditionalPropertiesToolStripMenuItem_Click);
            // 
            // print_grid_btn
            // 
            this.print_grid_btn.Location = new System.Drawing.Point(428, 367);
            this.print_grid_btn.Name = "print_grid_btn";
            this.print_grid_btn.Size = new System.Drawing.Size(100, 34);
            this.print_grid_btn.TabIndex = 7;
            this.print_grid_btn.Text = "Print the Grid";
            this.print_grid_btn.UseVisualStyleBackColor = true;
            this.print_grid_btn.Click += new System.EventHandler(this.PrintTheGridBtnClick);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument1_PrintPage);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Printer List";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 407);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.print_grid_btn);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.findprinter_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Printer Helper at 09.04.2018";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button findprinter_btn;
        private System.Windows.Forms.Button print_grid_btn;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DeletePrinter;
        private System.Windows.Forms.ToolStripMenuItem QueueOfPrinter;
        private System.Windows.Forms.ToolStripMenuItem PropertiesOfPrinter;
        private System.Windows.Forms.ToolStripMenuItem SendTestPage;
        public System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolStripMenuItem AdditionalPropertiesToolStripMenuItem;
    }
}

