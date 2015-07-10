using BusinessLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public interface IOutputCreator
    {
        string CreateOutput(ParseResult parsedData, FormatType.FormatTypeEnum formatType);
    }
}
