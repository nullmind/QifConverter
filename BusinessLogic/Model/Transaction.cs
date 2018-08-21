using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Model
{
    public class Transaction
    {
        public Transaction(bool isExpense)
        {
            IsExpenseTransaction = isExpense;
        }
        public bool IsExpenseTransaction { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Kronor { get; set; }
        public int Ore { get; set; }

        public override string ToString()
        {
            var foo =  string.Format("{0} {1} {2}{3},{4}", Date, Description, IsExpenseTransaction ? "-" : "", Kronor, Ore.ToString().PadLeft(2, '0'));
            return foo;
        }
    }
}
