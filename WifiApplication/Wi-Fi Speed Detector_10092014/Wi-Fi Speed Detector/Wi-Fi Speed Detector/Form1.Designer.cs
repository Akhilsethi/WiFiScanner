namespace WiFiSpeedDetector
{
    partial class Form1
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
            this.txtMessageLocalNet = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControlWIFI = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.WifiTextBox = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.ExternalMessageBox = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labelMessages = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControlWIFI.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMessageLocalNet
            // 
            this.txtMessageLocalNet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageLocalNet.Location = new System.Drawing.Point(6, 6);
            this.txtMessageLocalNet.Name = "txtMessageLocalNet";
            this.txtMessageLocalNet.ReadOnly = true;
            this.txtMessageLocalNet.Size = new System.Drawing.Size(1132, 395);
            this.txtMessageLocalNet.TabIndex = 0;
            this.txtMessageLocalNet.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.textBox1.Location = new System.Drawing.Point(135, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(135, 20);
            this.textBox1.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRefresh.Location = new System.Drawing.Point(276, 5);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 24);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "GO";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click_1);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Current System IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(985, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Network Computer Names";
            this.label2.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tabControlWIFI, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelMessages, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.63636F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.36364F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1158, 574);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // tabControlWIFI
            // 
            this.tabControlWIFI.Controls.Add(this.tabPage1);
            this.tabControlWIFI.Controls.Add(this.tabPage2);
            this.tabControlWIFI.Controls.Add(this.tabPage3);
            this.tabControlWIFI.Controls.Add(this.tabPage4);
            this.tabControlWIFI.Location = new System.Drawing.Point(3, 76);
            this.tabControlWIFI.Name = "tabControlWIFI";
            this.tabControlWIFI.SelectedIndex = 0;
            this.tabControlWIFI.Size = new System.Drawing.Size(1152, 462);
            this.tabControlWIFI.TabIndex = 0;
            this.tabControlWIFI.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtMessageLocalNet);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1144, 436);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "IP Information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.WifiTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1144, 436);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Wifi Range";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // WifiTextBox
            // 
            this.WifiTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WifiTextBox.Location = new System.Drawing.Point(6, 5);
            this.WifiTextBox.Name = "WifiTextBox";
            this.WifiTextBox.ReadOnly = true;
            this.WifiTextBox.Size = new System.Drawing.Size(1132, 441);
            this.WifiTextBox.TabIndex = 1;
            this.WifiTextBox.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ExternalMessageBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1144, 436);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "External Web";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // ExternalMessageBox
            // 
            this.ExternalMessageBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExternalMessageBox.Location = new System.Drawing.Point(3, 5);
            this.ExternalMessageBox.Name = "ExternalMessageBox";
            this.ExternalMessageBox.ReadOnly = true;
            this.ExternalMessageBox.Size = new System.Drawing.Size(1138, 441);
            this.ExternalMessageBox.TabIndex = 1;
            this.ExternalMessageBox.Text = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.richTextBox2);
            this.tabPage4.Controls.Add(this.listBox1);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1144, 436);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Local Network";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(3, 12);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(806, 428);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = "";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(970, 41);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(171, 186);
            this.listBox1.TabIndex = 5;
            this.listBox1.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.21429F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.78571F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 874F));
            this.tableLayoutPanel2.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnRefresh, 2, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1148, 35);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // labelMessages
            // 
            this.labelMessages.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelMessages.AutoSize = true;
            this.labelMessages.Location = new System.Drawing.Point(3, 551);
            this.labelMessages.Name = "labelMessages";
            this.labelMessages.Size = new System.Drawing.Size(0, 13);
            this.labelMessages.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.Enabled = false;
            this.progressBar1.Location = new System.Drawing.Point(12, 585);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1158, 26);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 617);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Wi - Fi Speed Detector";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Activated += new System.EventHandler(this.OnFormActivated);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControlWIFI.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtMessageLocalNet;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControlWIFI;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RichTextBox WifiTextBox;
        private System.Windows.Forms.RichTextBox ExternalMessageBox;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label labelMessages;
    }
}

