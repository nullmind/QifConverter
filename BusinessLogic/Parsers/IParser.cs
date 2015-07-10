using BusinessLogic.Model;
using System.Collections.Generic;

namespace BusinessLogic.Parsers
{
    public interface IParser
    {
        ParseResult ParseLines(List<string> inputLines);
    }
}