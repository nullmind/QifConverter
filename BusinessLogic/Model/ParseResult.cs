using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Model
{
    public class ParseResult
    {
        public List<Transaction> Transactions = new List<Transaction>();

        public List<string> FailedLines { get; set; }
    }
}
