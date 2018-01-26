using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Csd.Calculator
{
    public class RPNCalculator
    {
        private readonly TextWriter _output;
        private readonly Stack<string> _stack = new Stack<string>();

        public static void Main(String[] args)
        {
            var calc = new RPNCalculator();
            calc.WriteLine("Enter values followed by operation symbols: ");
            calc.Run(System.Console.In);
            
        }

        public RPNCalculator()
        {
            _output = System.Console.Out;
        }

        public RPNCalculator(TextWriter output)
        {
            _output = output;
        }

        public void WriteLine(String msg)
        {
            _output.WriteLine(msg);
        }

        public void Run(TextReader inputStream)
        {
            var tokenInput = new StreamTokenizer(inputStream);
            while (true)
            {
                ProcessInput(tokenInput);
            }
        }

        public void ProcessInput(StreamTokenizer tokenInput)
        {
            while (tokenInput.TokenAvailable())
            {
                _stack.Push(tokenInput.NextToken());
                WriteStackToOutput(_stack);
            }
        }
    
        private void WriteStackToOutput(ICollection stack)
        {

            if (stack.Count <= 0)
            {
                return;
            }
            Stack tmp = new Stack(stack);
            StringBuilder str = new StringBuilder();

            str.Append("[");
            while (tmp.Count > 0)
            {
                str.Append(tmp.Pop());
                if (tmp.Count > 0)
                {
                    str.Append(", ");
                }
            }

            str.Append("]");

            _output.WriteLine(str.ToString());
            _output.Flush();
        }
       
    }
}
