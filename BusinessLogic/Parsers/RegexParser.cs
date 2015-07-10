using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BusinessLogic.Model;

namespace BusinessLogic.Parsers
{
    public class RegexParser : IParser
    {
        public RegexParser(FormatType.FormatTypeEnum formatType)
        {
            var regex = GetRegexForFormatType(formatType);

        }

        private static Regex GetRegexForFormatType(FormatType.FormatTypeEnum formatType)
        {
            switch (formatType)
            {
                case FormatType.FormatTypeEnum.SjPrio:
                    return new Regex(@"(?<date1>\d\d-\d\d)\s+(?<date2>\d\d-\d\d)\s+(?<desc>[\w $#'-@&¦{]+)\s+(?<loc>[\w $@#'-{}]+)\s+(?<cur>[\w]*)\s+(?<utlbelopp>[\d, ]+)\s+(?<kronor>-?[\d ]+),(?<ore>[\d]{2})");
                //Uppdaterar eftersom det verkar ha lagts till valutafält + utl valuta värde.
                //return new Regex(@"(?<date1>\d\d-\d\d)\s+(?<date2>\d\d-\d\d)\s+(?<desc>[\w $#'-@&]+)\s+(?<loc>[\w $@#']+)\s+(?<kronor>-?[\d ]+),(?<ore>[\d]{2})");
                //return new Regex(@"(?<date1>(\d\d-\d\d))\s+(?<date2>(\d\d-\d\d))\s+(?<desc>(.+))\s{2}(?<loc>(.+))\s+(?<kronor>(-?[\d ]+)),(?<ore>([\d]{2}))");
                case FormatType.FormatTypeEnum.Swedbank:
                    //7 Clnr 12 Kontonr 21 Kontonamn 8 valuta 17 Bokföringsdatum
                    // 19 Transdatum 36 Referens 24? Kontohändelse 14 Belopp
                    return new Regex(@"(?<clnr>[\d\s]{7})(?<kontonr>[\d\s]{12})(?<kontonamn>[\w\s-]{21})(?<valuta>[\w\s]{8})(?<date1>[\d\s-]{17})(?<date2>[\d\s-]{19})(?<desc>[\w\s+\{\}@-]{36})(?<kontohand>[\w\s-\./]{24})(?<kronor>[- \d]+),(?<ore>\d\d)");
                    //return new Regex(@"(?<clnr>[\d\s]{7})(?<kontonr>[\d\s]{12})(?<kontonamn>[\w\s]{21})(?<valuta>[\w\s]{8})(?<date1>[\d\s-]{17})(?<date2>[\d\s-]{19})(?<desc>[\w\s]{36})(?<kontohand>[\w\s]{24})(?<amount>.*)");
                //return new Regex(@"(?<date1>\d\d-\d\d-\d\d)\s(?<date2>\d\d-\d\d-\d\d)\s+(?<desc>[-:&/*\\' \w\d\(\)]+)\s+(?<kronor>-?[ \d]+),(?<ore>\d\d)\s");
                case FormatType.FormatTypeEnum.SwedbankOld:
                    return new Regex(@"(?<date1>\d\d-\d\d-\d\d)\s(?<date2>\d\d-\d\d-\d\d)\s+(?<desc>[-:&/*\\' \w\d\(\)]+)\s+(?<kronor>-?[ \d]+),(?<ore>\d\d)\s");
                case FormatType.FormatTypeEnum.Seb:
                    return new Regex(@"(?<date1>\d\d\d\d-\d\d-\d\d)\s(?<date2>\d\d\d\d-\d\d-\d\d)\s\d{10}[LS]?\s(?<desc>[-:&/*\\' \w\d\.>]+)\s(?<kronor>[-\d ]+),(?<ore>\d\d)\s");
                case FormatType.FormatTypeEnum.Coop:
                    return new Regex(@"(?<date1>\d\d\d\d-\d\d-\d\d)\s+(?<action>\w+)\s+(?<purchaser>[\w ]+)\s+(?<placeOfPurchase>[ \w\d]+)\s+(?<kronor>[-\d ]+),(?<ore>\d\d)\s");
                case FormatType.FormatTypeEnum.ICA:
                    return new Regex(@"(?<date1>\d\d\d\d-\d\d-\d\d)\s+(?<placeOfPurchase>[ \w\d,]+)\s(?<kronor>[-\d \t]+),(?<ore>\d\d)");
                default:
                    throw new ArgumentOutOfRangeException("formatType");
            }
        }

        public ParseResult ParseLines(List<string> inputLines)
        {
            throw new NotImplementedException();
        }
    }
}
