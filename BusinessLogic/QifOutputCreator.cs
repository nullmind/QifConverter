using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    public class QifOutputCreator : IOutputCreator
    {
        public string CreateOutput(ParseResult parsedData, FormatType.FormatTypeEnum formatType)
        {
            var strb = new StringBuilder();
            strb.AppendLine(GetQifHeader(formatType));

            foreach (var transaction in parsedData.Transactions)
            {
                var qifText = new QifTransaction(transaction).ToString();
                strb.AppendLine(qifText);
            }


            return strb.ToString();
        }
        private string GetQifHeader(FormatType.FormatTypeEnum formatType)
        {

            switch (formatType)
            {
                case FormatType.FormatTypeEnum.Peugeot:
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

    }
}
