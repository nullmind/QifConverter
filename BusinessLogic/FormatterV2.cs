using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class FormatterV2
    {
        private string _yearToUse;
        private FormatType.FormatTypeEnum _formatType;
        public FormatterV2(FormatType.FormatTypeEnum formatType, string yearToUse)
        {
            _yearToUse = yearToUse;
            _formatType = formatType;
        }

        public QifConversionResult ConvertText(List<string> inputLines)
        {
            var result = new QifConversionResult();


            var parser = ParserFactory.GetParser(_formatType);

            var parseResult = parser.ParseLines(inputLines);

            var outputCreator = new QifOutputCreator();
            result.Output = outputCreator.CreateOutput(parseResult, _formatType);
            result.Success = true;
                
            return result;
        }
    }
}
