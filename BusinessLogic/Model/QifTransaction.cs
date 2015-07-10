using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic.Model
{
    public class QifTransaction
    {
        Transaction _transaction;
        public QifTransaction(Transaction transaction)
        {
            _transaction = transaction;
        }

        public override string ToString()
        {
            var date = _transaction.Date.ToString("yyyy-MM-dd");
            var desc = _transaction.Description;
            var val = string.Format("{0}{1},{2}", _transaction.IsExpenseTransaction ? "-" : "", _transaction.Kronor, _transaction.Ore);
            return GetQifText(date, desc, val);
        }

        private string GetQifText(string date, string desc, string val)
        {
            //string foo = CapitalizeWords(desc.ToLower());
            var finalDesc = GetFinalDescription(desc);
            return string.Format("D{0}{1}T{2}{1}P{3}{1}^{1}", date, Environment.NewLine, val, finalDesc);
        }

        private string GetFinalDescription(string desc)
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

        private string Capitalize(string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }

    }
}
