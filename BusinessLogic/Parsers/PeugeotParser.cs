using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Model;
using System.Text.RegularExpressions;

namespace BusinessLogic.Parsers
{
    public class PeugeotParser : IParser
    {
        readonly Regex DetailsRegex = new Regex(@"(?<date>\d\d\d\d-\d\d-\d\d)\t(?<action>[ \w]+)\t(?<desc>[\w,' &\*\.\/-]+)\t(?<placeOfPurchase>[- \w\d\.\/]*)\t\t(?<payment>-*)(?<kronor>[\d ]+),(?<ore>\d\d)\s");
        public ParseResult ParseLines(List<string> inputLines)
        {
            var result = new ParseResult();

            foreach (var line in inputLines)
            {
                if(string.IsNullOrEmpty(line))
                    continue;

                var transaction = GetTransactionDetails(line);
                if(transaction == null)
                    throw new Exception("Failed to parse Peugeot transaction.");
                result.Transactions.Add(transaction);
            }

            return result;
        }

        private Transaction GetTransactionDetails(string line)
        {
            var match = DetailsRegex.Match(line);
            if (match.Success)
            {
                var date = DateTime.Parse(match.Groups["date"].Value);
                var isExpense = string.IsNullOrWhiteSpace(match.Groups["payment"].Value);
                var trans = new Transaction(isExpense);
                trans.Date = date;
                trans.Kronor = int.Parse(match.Groups["kronor"].Value);
                trans.Ore = int.Parse(match.Groups["ore"].Value);
                trans.Description = string.Format("{0} ({1})", match.Groups["desc"].Value.Trim(), match.Groups["placeOfPurchase"]);
                return trans;
            }

            return null;

        }
    }
}
