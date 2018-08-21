using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Model;
using System.Text.RegularExpressions;

namespace BusinessLogic.Parsers
{
    public class SwedbankParser : IParser
    {
        readonly Regex DetailsRegex = new Regex(@"(?<date>\d\d\d\d-\d\d-\d\d)\t(?<action>[ \w]+)\t(?<desc>[\w,' &\*\.\/-]+)\t(?<placeOfPurchase>[- \w\d\.\/]*)\t\t(?<payment>-*)(?<kronor>[\d ]+),(?<ore>\d\d)\s");
        public ParseResult ParseLines(List<string> inputLines)
        {
            var result = new ParseResult();

            foreach (var line in inputLines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                var transaction = GetTransactionDetails(line);
                if (transaction == null)
                    throw new Exception("Failed to parse Swedbank transaction.");
                result.Transactions.Add(transaction);
            }

            return result;
        }

        private Transaction GetTransactionDetails(string line)
        {
            // Floristutbildnin	2018-05-31	2018-05-31	-9 500,00	0

            var parts = line.Split(';');

            if (parts.Length >= 5)
            {
                var date = DateTime.Parse(parts[2]);
                var money = parts[3].Replace(" ", "");
                var moneyParts = money.Split(',');

                var isExpense = false;
                var trans = new Transaction(isExpense);
                trans.Date = date;
                trans.Kronor = int.Parse(moneyParts[0]);
                trans.Ore = int.Parse(moneyParts[1]);
                trans.Description = parts[0];


                var foo = trans.ToString();
                return trans;
            }

            //var match = DetailsRegex.Match(line);
            //if (match.Success)
            //{
            //    var date = DateTime.Parse(match.Groups["date"].Value);
            //    var isExpense = string.IsNullOrWhiteSpace(match.Groups["payment"].Value);
            //    var trans = new Transaction(isExpense);
            //    trans.Date = date;
            //    trans.Kronor = int.Parse(match.Groups["kronor"].Value);
            //    trans.Ore = int.Parse(match.Groups["ore"].Value);
            //    trans.Description = string.Format("{0} ({1})", match.Groups["desc"].Value.Trim(), match.Groups["placeOfPurchase"]);
            //    return trans;
            //}

            return null;

        }
    }
}
