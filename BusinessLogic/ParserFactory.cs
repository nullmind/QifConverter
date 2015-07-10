using BusinessLogic.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class ParserFactory
    {
        public static IParser GetParser(FormatType.FormatTypeEnum formatType)
        {
            switch(formatType)
            {
                case FormatType.FormatTypeEnum.Coop:
                    return new CoopParser();
                case FormatType.FormatTypeEnum.Peugeot:
                    return new PeugeotParser();
                default:
                    return new RegexParser(formatType);
            }
        }
    }
}
