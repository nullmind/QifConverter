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


            rtbInput.Text = @"84244  9049846315  Privatkonto          SEK     14-12-29         14-12-29           AKTIEINVEST                         Autogiro                     -1 000,00
";


            txtYear.Text = DateTime.Today.Year.ToString();
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

            Formatter.YearToUse = txtYear.Text;

            switch (selectedFormatType.InternalValue)
            {
                case FormatType.FormatTypeEnum.SjPrio:
                case FormatType.FormatTypeEnum.Seb:
                case FormatType.FormatTypeEnum.ICA:
                    {
                        var conversionResult = Formatter.ConvertTextToQif(selectedFormatType.InternalValue, inputLines);
                        conversionResult.FormatOutput();

                        rtbLog.Clear();
                        rtbOutput.Clear();
                        rtbOutput.AppendText(conversionResult.Output);
                        tabControl1.SelectedTab = tabPageOutput;

                        DoPostConversionLogging(conversionResult);
                    }
                    break;
                case FormatType.FormatTypeEnum.Coop:
                case FormatType.FormatTypeEnum.Swedbank:
                case FormatType.FormatTypeEnum.Peugeot:
                    {
                        try
                        {
                            var formatter = new FormatterV2(selectedFormatType.InternalValue, txtYear.Text);
                            var result = formatter.ConvertText(inputLines);

                            rtbLog.Clear();
                            rtbOutput.Clear();
                            rtbOutput.AppendText(result.Output);
                            tabControl1.SelectedTab = tabPageOutput;
                            DoPostConversionLogging(result);
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(string.Format("Exception caught: {0}", exc.Message));
                        }

                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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


        private void rtbInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
