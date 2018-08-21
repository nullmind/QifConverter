using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tests
{
    [TestClass]
    public class FormatterV2Tests
    {
        [TestMethod]
        public void TestCoopParser()
        {
            var formatType = BusinessLogic.FormatType.FormatTypeEnum.Coop;
            var inputLines = File.ReadAllLines(@"..\..\TestFiles\Coop\Coop150101-150701.txt", Encoding.GetEncoding("windows-1252")).ToList();

            var formatter = new BusinessLogic.FormatterV2(formatType, "2014");
            var result = formatter.ConvertText(inputLines);
        }

        [TestMethod]
        public void TestSwedbankParser()
        {
            var formatType = BusinessLogic.FormatType.FormatTypeEnum.Swedbank;
            var inputLines = File.ReadAllLines(@"..\..\TestFiles\Swedbank\Service0101-0820.csv", Encoding.GetEncoding("windows-1252")).ToList();

            var formatter = new BusinessLogic.FormatterV2(formatType, "2018");
            var result = formatter.ConvertText(inputLines);
        }
    }
}
