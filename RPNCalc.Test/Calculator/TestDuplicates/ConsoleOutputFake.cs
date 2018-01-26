using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Csd.Calculator.TestDuplicates
{
    sealed class ConsoleOutputFake : StringWriter
    {
        public ConsoleOutputFake()
        {
            GetStringBuilder().Clear();
        }

        public String[] GetStrings()
        {
            this.Flush();
            String output = this.GetStringBuilder().ToString().Trim();
            
            return Regex.Split(output, "\\r\\n");
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
