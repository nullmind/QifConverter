using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLogic;

namespace WinFront
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Qif file|*.qif";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var availableFormatType in Formatter.GetAvailableFormatTypes())
            {
                cbFormat.Items.Add(availableFormatType);
            }
            cbFormat.DisplayMember = "DisplayName";
            cbFormat.SelectedIndex = 0;


            rtbInput.Text = @"09-10 	09-12 	KRONANS DROGHANDEL 127 	V#STER$S 			212,00
09-09 	09-12 	ÖRONMOTTAGNINGEN 	VÄSTERÅS 			300,00
09-09 	09-12 	2 RUM OCH KOK 	VESTERAS 			320,00";
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK && File.Exists(openFileDialog1.FileName))
            {
                rtbInput.Clear();
                var allText = File.ReadAllText(openFileDialog1.FileName, System.Text.Encoding.GetEncoding("iso-8859-1"));
                rtbInput.AppendText(allText);
            }
        }

        private void btnConvertToQif_Click(object sender, EventArgs e)
        {
            var selectedFormatType = (FormatType)cbFormat.SelectedItem;

            List<string> inputLines = GetInputAsStringList();

            var conversionResult = Formatter.ConvertTextToQif(selectedFormatType.InternalValue, inputLines);

            rtbLog.Clear();
            rtbOutput.Clear();
            rtbOutput.AppendText(conversionResult.Output);
            tabControl1.SelectedTab = tabPageOutput;

            DoPostConversionLogging(conversionResult);
        }

        private void DoPostConversionLogging(QifConversionResult conversionResult)
        {
            if (conversionResult.Success)
                lblSuccess.BackColor = Color.Chartreuse;
            else
                lblSuccess.BackColor = Color.Red;

            if (conversionResult.FailedLines.Count > 0)
            {
                AddLine(rtbLog, "Failed lines detected!");
                foreach (var failedLine in conversionResult.FailedLines)
                {
                    AddLine(rtbLog, failedLine);
                }
            }
        }

        private void AddLine(RichTextBox richTextBox, string message)
        {
            richTextBox.AppendText(string.Format("{0}{1}", message, Environment.NewLine));
        }

        private List<string> GetInputAsStringList()
        {
            return new List<string>(rtbInput.Lines);
        }

        private void btnExportToFile_Click(object sender, EventArgs e)
        {
            var dialogResult = saveFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var fileName = saveFileDialog1.FileName;
                File.WriteAllLines(fileName, rtbOutput.Lines);
            }
        }
    }
}
