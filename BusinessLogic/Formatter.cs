using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    public class Formatter
    {
        public static List<FormatType> GetAvailableFormatTypes()
        {
            var list = new List<FormatType>
                           {
                               new FormatType("SJ Prio", FormatType.FormatTypeEnum.SjPrio),
                               new FormatType("Swedbank", FormatType.FormatTypeEnum.Swedbank),
                               new FormatType("Seb", FormatType.FormatTypeEnum.Seb),
                               new FormatType("Coop", FormatType.FormatTypeEnum.Coop),
                               new FormatType("Peugeot", FormatType.FormatTypeEnum.Peugeot),
                               new FormatType("ICA", FormatType.FormatTypeEnum.ICA)
                           };

            return list;
        }

        public static QifConversionResult ConvertTextToQif(FormatType.FormatTypeEnum formatType, List<string> inputLines)
        {
            var conversionResult = new QifConversionResult {Header = GetQifHeader(formatType)};



            ConvertLinesToQif(formatType, inputLines, conversionResult);

            conversionResult.Success = conversionResult.FailedLines.Count < 1;
            return conversionResult;
        }

        private static void ConvertLinesToQif(FormatType.FormatTypeEnum formatType, IEnumerable<string> inputLines, QifConversionResult conversionResult)
        {
            var regex = GetRegexForFormatType(formatType);

            foreach (var inputLine in inputLines)
            {
                if(string.IsNullOrWhiteSpace(inputLine))
                    continue;
                var match = regex.Match(inputLine);
                if (match.Success)
                {
                    string qifText = ConvertMatchToQif(formatType, match);
                    if (qifText == null) return;
                    conversionResult.OutputLines.Append(qifText);
                }
                else
                {
                    conversionResult.FailedLines.Add(inputLine);
                }
            }
        }

        private static string ConvertMatchToQif(FormatType.FormatTypeEnum formatType, Match match)
        {
            switch (formatType)
            {
                case FormatType.FormatTypeEnum.SjPrio:
                    return ConvertSjPrioMatchToQif(match);
                case FormatType.FormatTypeEnum.Swedbank:
                    return ConvertSwedbankMatchToQif(match);
                case FormatType.FormatTypeEnum.SwedbankOld:
                    return ConvertSwedbankMatchToQifOld(match);
                case FormatType.FormatTypeEnum.Seb:
                    return ConvertSebMatchToQif(match);
                case FormatType.FormatTypeEnum.Coop:
                    return ConvertCoopMatchToQif(match);
                case FormatType.FormatTypeEnum.ICA:
                    return ConvertICAMatchToQif(match);
                default:
                    throw new ArgumentOutOfRangeException("formatType");
            }
        }

        private static string ConvertICAMatchToQif(Match match)
        {
            var date = match.Groups["date1"].Value;
            var placeOfPurchase = match.Groups["placeOfPurchase"].Value;
            var val = GetValueFromMatch(match);

            return GetQifText(date, placeOfPurchase, val);
        }

        private static string ConvertCoopMatchToQif(Match match)
        {
            var date = match.Groups["date1"].Value;
            var action = match.Groups["action"].Value;
            if (action == "PG-inbetalning")
                return null;
            var purchaser = match.Groups["purchaser"].Value;
            var placeOfPurchase = match.Groups["placeOfPurchase"].Value;
            var val = GetValueFromMatch(match);
            var finalDesc = string.Format("{0} ({1})", placeOfPurchase, purchaser);

            return GetQifText(date, finalDesc, val);
        }

        private static string ConvertSwedbankMatchToQif(Match match)
        {
            var date2 = match.Groups["date2"].Value.Trim();
            var desc = match.Groups["desc"].Value.Trim();
            var val = GetValueFromMatch(match);

            var date = string.Format("20{0}", date2);

            return GetQifText(date, desc, val);
        }

        private static string ConvertSwedbankMatchToQifOld(Match match)
        {
            var date2 = match.Groups["date2"].Value;
            var desc = match.Groups["desc"].Value;
            var val = GetValueFromMatch(match);

            var date = string.Format("20{0}", date2);

            return GetQifText(date, desc, val);
        }

        private static string ConvertSebMatchToQif(Match match)
        {
            var date2 = match.Groups["date2"].Value;
            var desc = match.Groups["desc"].Value;

            string val = GetValueFromMatch(match);

            var transDateMatch = Regex.Match(desc, @"(?<transdate>/\d\d-\d\d-\d\d)");


            string date = date2;
            if (transDateMatch.Groups["transdate"].Success)
            {
                var transdate = transDateMatch.Groups["transdate"].Value;

                desc = desc.Replace(transdate, "").Trim();
                transdate = transdate.TrimStart('/');
                var yearToUse = GetYearToUse();
                date = string.Format("{0}{1}", yearToUse.Substring(0, 2), transdate);
            }


            return GetQifText(date, desc, val);
        }

        private static string ConvertSjPrioMatchToQif(Match match)
        {
            var date1 = match.Groups["date1"].Value;
            var desc = match.Groups["desc"].Value;
            var loc = match.Groups["loc"].Value;
            //var kronor = match.Groups["kronor"];
            //var ore = match.Groups["ore"];

            //var kronorCleaned = kronor.Value.Replace(" ", "");
            //var val = string.Format("{0},{1}", kronorCleaned, ore.Value);

            var val = GetValueFromMatch(match);
            val = InvertValue(val);

            string yearToUse = GetYearToUse();
            var date = string.Format("{0}-{1}", yearToUse, date1);

            var descToUse = string.Format("{0} ({1})", desc.Trim(), loc.Trim());


            return GetQifText(date, descToUse, val);
        }

        private static string InvertValue(string val)
        {
            if (val.StartsWith("-"))
                return val.Replace("-", "");
            return string.Format("-{0}", val);
        }

        private static string GetValueFromMatch(Match match)
        {
            var kronor = match.Groups["kronor"];
            var ore = match.Groups["ore"];

            var kronorCleaned = kronor.Value.Replace(" ", "");
            return string.Format("{0},{1}", kronorCleaned, ore.Value);
        }

        private static string GetQifText(string date, string desc, string val)
        {
            //string foo = CapitalizeWords(desc.ToLower());
            var finalDesc = GetFinalDescription(desc);
            return string.Format("D{0}{1}T{2}{1}P{3}{1}^{1}", date, Environment.NewLine, val, finalDesc);
        }

        private static string GetFinalDescription(string desc)
        {
            desc = desc.Trim();
            if (desc == "AEA")
                return desc;
            string replace = Capitalize(desc.ToLower());
            replace = Regex.Replace(replace, "Ica", "ICA");
            replace = Regex.Replace(replace, "Aea", "AEA");
            replace = Regex.Replace(replace, @"Ab(?<post>\s+)", "AB${post}");
            return replace;
        }

        private static string GetYearToUse()
        {
            return YearToUse;
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
                    return new Regex(@"(?<clnr>[\d\s]{7})(?<kontonr>[\d\s]{12})(?<kontonamn>[\w\s-]{21})(?<valuta>[\w\s]{8})(?<date1>[\d\s-]{17})(?<date2>[\d\s-]{19})(?<desc>[/\w\s+\{\}@-]{36})(?<kontohand>[\w\s-\./]{24})(?<kronor>[- \d]+),(?<ore>\d\d)");
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

        private static string GetQifHeader(FormatType.FormatTypeEnum formatType)
        {

            switch (formatType)
            {
                case FormatType.FormatTypeEnum.SjPrio:
                    return @"!Account
NChecking Account
TCCard
!Type:CCard";
                case FormatType.FormatTypeEnum.Swedbank:
                case FormatType.FormatTypeEnum.Seb:
                case FormatType.FormatTypeEnum.Coop:
                case FormatType.FormatTypeEnum.ICA:
                    return @"!Account
NChecking Account
TBank
!Type:Bank";
                default:
                    throw new ArgumentOutOfRangeException("formatType");
            }
        }

        private static string Capitalize(string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }

        public static string CapitalizeWords(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length == 0)
                return value;

            char[] result = value.ToCharArray();
            result[0] = char.ToUpper(result[0]);
            for (int i = 1; i < result.Length; ++i)
            {
                if (char.IsWhiteSpace(result[i - 1]))
                    result[i] = char.ToUpper(result[i]);
            }
            return new string(result);
        }

        public static string YearToUse { get; set; }
    }
}
