using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class QifConversionResult
    {
        private readonly StringBuilder _outputLines  = new StringBuilder();
        private List<string> _failedLines = new List<string>();

        public string Output
        {
            get;set;
        }

        public void FormatOutput()
        {
            var strb = new StringBuilder();
            strb.AppendLine(Header);
            strb.AppendLine(OutputLines.ToString());

            Output = strb.ToString();
        }

        public bool Success { get; set; }

        public string Header { get; set; }
        public StringBuilder OutputLines
        {
            get { return _outputLines; }
        }

        public List<string> FailedLines
        {
            get { return _failedLines; }
        }
    }
}