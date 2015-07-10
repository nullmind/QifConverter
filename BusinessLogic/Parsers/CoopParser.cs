using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Model;
using System.Text.RegularExpressions;

namespace BusinessLogic.Parsers
{
    public class CoopParser : IParser
    {
        readonly Regex DateRegex = new Regex(@"(?<day>\d\d) (?<month>\w+) (?<year>\d\d\d\d)");
        readonly Regex PurchaserRegex = new Regex(@".*Köpav (?<purchaser>.*)");
        readonly Regex PaymentRegex = new Regex(@"(?<desc>.*)\t(?<kronor>[\d ]+),(?<ore>\d\d)\skr");
        readonly Regex ReplacementCardFeeRegex = new Regex(@"Ersättningskortsavgift\t-(?<kronor>[\d ]+),(?<ore>\d\d)\skr");
        readonly Regex DetailsRegex = new Regex(@"(?<desc>.*\s)-(?<kronor>\d+),(?<ore>\d+)\skr");
        readonly List<string> Months = new List<string> { "januari", "februari", "mars", "april", "maj", "juni", "juli", "augusti", "september", "oktober", "november", "december" };

        bool _detailsExpected = false;

        DateTime _currentDate;
        string _currentPurchaser;

        public ParseResult ParseLines(List<string> inputLines)
        {
            var result = new ParseResult();

            foreach (var line in inputLines)
            {
                // Är det ett nytt datum?
                var cleansedLine = CheckForNewDateAndSetIt(line);

                if (_detailsExpected)
                {
                    var transaction = GetTransactionDetails(cleansedLine);
                    if (transaction == null)
                    {
                        throw new Exception("COOP: Details expected, no match found.");
                    }
                    result.Transactions.Add(transaction);
                    _detailsExpected = false;
                }
                else
                {
                    var purchaser = CheckLineForPurchaser(cleansedLine);
                    if (purchaser != null)
                    {
                        _currentPurchaser = purchaser;
                        _detailsExpected = true;
                        continue;
                    }

                    var payment = CheckLineForPayment(cleansedLine);
                    if (payment != null)
                    {
                        result.Transactions.Add(payment);
                        _detailsExpected = false;
                        continue;
                    }

                    var replacementCardFee = CheckLineForReplacementCardFee(cleansedLine);
                    if (replacementCardFee != null)
                    {
                        result.Transactions.Add(replacementCardFee);
                        _detailsExpected = false;
                        continue;
                    }
                    throw new Exception("COOP: no match found in no details block.");

                }
            }

            return result;
        }

        private string CheckForNewDateAndSetIt(string line)
        {
            var match = DateRegex.Match(line);
            if (match.Success)
            {
                var day = int.Parse(match.Groups["day"].Value);
                var year = int.Parse(match.Groups["year"].Value);

                var monthString = match.Groups["month"].Value;

                var month = Months.IndexOf(monthString) + 1;

                _currentDate = new DateTime(year, month, day);
                line = line.Replace(string.Format("{0} {1} {2}", match.Groups["day"], match.Groups["month"], match.Groups["year"]), "");
            }

            return line;
        }

        private string CheckLineForPurchaser(string line)
        {
            var match = PurchaserRegex.Match(line);
            if (match.Success)
            {
                return match.Groups["purchaser"].Value;
            }
            return null;
        }

        private Transaction CheckLineForPayment(string line)
        {
            var match = PaymentRegex.Match(line);
            if (match.Success)
            {
                var trans = new Transaction(false);
                var kronorString = match.Groups["kronor"].Value;
                trans.Kronor = int.Parse(kronorString.Replace(" ", ""));
                trans.Ore = int.Parse(match.Groups["ore"].Value);
                trans.Date = _currentDate;
                trans.Description = match.Groups["desc"].Value;
                return trans;
            }
            return null;
        }

        private Transaction CheckLineForReplacementCardFee(string line)
        {
            var match = ReplacementCardFeeRegex.Match(line);
            if (match.Success)
            {
                var trans = new Transaction(true);
                var kronorString = match.Groups["kronor"].Value;
                trans.Kronor = int.Parse(kronorString.Replace(" ", ""));
                trans.Ore = int.Parse(match.Groups["ore"].Value);
                trans.Date = _currentDate;
                trans.Description = "Ersättningskortsavgift";
                return trans;
            }
            return null;
        }

        private Transaction GetTransactionDetails(string line)
        {
            var match = DetailsRegex.Match(line);
            if (match.Success)
            {
                var trans = new Transaction(true);
                trans.Date = _currentDate;
                trans.Kronor = int.Parse(match.Groups["kronor"].Value);
                trans.Ore = int.Parse(match.Groups["ore"].Value);
                trans.Description = string.Format("{0} ({1})", match.Groups["desc"].Value.Trim(), _currentPurchaser);
                return trans;
            }

            return null;

        }
    }
}
