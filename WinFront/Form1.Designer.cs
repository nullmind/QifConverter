namespace WinFront
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
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.rtbInput = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageInput = new System.Windows.Forms.TabPage();
            this.tabPageOutput = new System.Windows.Forms.TabPage();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnConvertToQif = new System.Windows.Forms.Button();
            this.lblSuccess = new System.Windows.Forms.Label();
            this.btnExportToFile = new System.Windows.Forms.Button();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageInput.SuspendLayout();
            this.tabPageOutput.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbFormat
            // 
            this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.Location = new System.Drawing.Point(16, 582);
            this.cbFormat.Margin = new System.Windows.Forms.Padding(4);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(249, 24);
            this.cbFormat.TabIndex = 0;
            // 
            // txtInputFile
            // 
            this.txtInputFile.Location = new System.Drawing.Point(16, 16);
            this.txtInputFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(419, 22);
            this.txtInputFile.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(445, 16);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(100, 28);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "Select file";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // rtbInput
            // 
            this.rtbInput.Location = new System.Drawing.Point(0, 0);
            this.rtbInput.Margin = new System.Windows.Forms.Padding(4);
            this.rtbInput.Name = "rtbInput";
            this.rtbInput.Size = new System.Drawing.Size(1028, 478);
            this.rtbInput.TabIndex = 3;
            this.rtbInput.Text = "09-10 \t09-12 \tKRONANS DROGHANDEL 127 \tV#STER$S \t\t\t212,00";
            this.rtbInput.TextChanged += new System.EventHandler(this.rtbInput_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageInput);
            this.tabControl1.Controls.Add(this.tabPageOutput);
            this.tabControl1.Controls.Add(this.tabPageLog);
            this.tabControl1.Location = new System.Drawing.Point(17, 49);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1040, 511);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPageInput
            // 
            this.tabPageInput.Controls.Add(this.rtbInput);
            this.tabPageInput.Location = new System.Drawing.Point(4, 25);
            this.tabPageInput.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageInput.Name = "tabPageInput";
            this.tabPageInput.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageInput.Size = new System.Drawing.Size(1032, 482);
            this.tabPageInput.TabIndex = 0;
            this.tabPageInput.Text = "Input";
            this.tabPageInput.UseVisualStyleBackColor = true;
            // 
            // tabPageOutput
            // 
            this.tabPageOutput.Controls.Add(this.rtbOutput);
            this.tabPageOutput.Location = new System.Drawing.Point(4, 25);
            this.tabPageOutput.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageOutput.Name = "tabPageOutput";
            this.tabPageOutput.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageOutput.Size = new System.Drawing.Size(1032, 482);
            this.tabPageOutput.TabIndex = 1;
            this.tabPageOutput.Text = "Output";
            this.tabPageOutput.UseVisualStyleBackColor = true;
            // 
            // rtbOutput
            // 
            this.rtbOutput.Location = new System.Drawing.Point(0, 0);
            this.rtbOutput.Margin = new System.Windows.Forms.Padding(4);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(1028, 478);
            this.rtbOutput.TabIndex = 4;
            this.rtbOutput.Text = "";
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.rtbLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 25);
            this.tabPageLog.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size(1032, 482);
            this.tabPageLog.TabIndex = 2;
            this.tabPageLog.Text = "Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(0, 0);
            this.rtbLog.Margin = new System.Windows.Forms.Padding(4);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(1028, 478);
            this.rtbLog.TabIndex = 5;
            this.rtbLog.Text = "";
            // 
            // btnConvertToQif
            // 
            this.btnConvertToQif.Location = new System.Drawing.Point(276, 582);
            this.btnConvertToQif.Margin = new System.Windows.Forms.Padding(4);
            this.btnConvertToQif.Name = "btnConvertToQif";
            this.btnConvertToQif.Size = new System.Drawing.Size(143, 28);
            this.btnConvertToQif.TabIndex = 5;
            this.btnConvertToQif.Text = "Convert to QIF";
            this.btnConvertToQif.UseVisualStyleBackColor = true;
            this.btnConvertToQif.Click += new System.EventHandler(this.btnConvertToQif_Click);
            // 
            // lblSuccess
            // 
            this.lblSuccess.AutoSize = true;
            this.lblSuccess.BackColor = System.Drawing.Color.Chartreuse;
            this.lblSuccess.Location = new System.Drawing.Point(553, 20);
            this.lblSuccess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSuccess.Name = "lblSuccess";
            this.lblSuccess.Size = new System.Drawing.Size(40, 34);
            this.lblSuccess.TabIndex = 6;
            this.lblSuccess.Text = "    \r\n        ";
            // 
            // btnExportToFile
            // 
            this.btnExportToFile.Location = new System.Drawing.Point(427, 582);
            this.btnExportToFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportToFile.Name = "btnExportToFile";
            this.btnExportToFile.Size = new System.Drawing.Size(143, 28);
            this.btnExportToFile.TabIndex = 7;
            this.btnExportToFile.Text = "Export to file";
            this.btnExportToFile.UseVisualStyleBackColor = true;
            this.btnExportToFile.Click += new System.EventHandler(this.btnExportToFile_Click);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(853, 16);
            this.txtYear.Margin = new System.Windows.Forms.Padding(4);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(132, 22);
            this.txtYear.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 862);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.btnExportToFile);
            this.Controls.Add(this.lblSuccess);
            this.Controls.Add(this.btnConvertToQif);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtInputFile);
            this.Controls.Add(this.cbFormat);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Gnucash Converter WinForm Edition";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageInput.ResumeLayout(false);
            this.tabPageOutput.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFormat;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.RichTextBox rtbInput;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageInput;
        private System.Windows.Forms.TabPage tabPageOutput;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.Button btnConvertToQif;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Label lblSuccess;
        private System.Windows.Forms.Button btnExportToFile;
        private System.Windows.Forms.TextBox txtYear;
    }
}

