using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountSheepSkill_CSharp
{
    static class ComposeMessage
    {
        public static string ComposeCountContents(double breakTime)
        {
            var countContents = "";

            foreach (var i in Enumerable.Range(1, 100))
            {
                countContents += $"<p>羊が{i}匹。<break time='{breakTime}s'/></p>";
            }           

            return countContents;
        }
    }
}
