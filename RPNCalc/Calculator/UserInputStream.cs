using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Csd.Calculator
{
    public class UserInputStream
    {
        private readonly TextReader _inputStream;

        private readonly Queue<String> _tokensRead = new Queue<string>();


        /**
         * Default constructor: uses Console.in for input stream
         * 
         */
        public UserInputStream()
        {
            _inputStream = Console.In;
        }

        /**
         * @param TextReader
         *
         */
        public UserInputStream(TextReader inputStream)
        {
            _inputStream = inputStream;
        }

        /**
         * @param String
         * 
         */
        public UserInputStream(String inputString)
        {
            _inputStream = new StringReader(inputString);
        }


        /**
         * @return string token, or empty string
         * 
         */
        public String NextToken()
        {
            return TokenAvailable() ? _tokensRead.Dequeue() : "";
        }

        /**
         * @return if token waiting in queue to be processed.
         * 
         */
        public bool TokenAvailable()
        {
            if (_tokensRead.Count <= 0)
            {
                try
                {
                    ParseInputStream();
                }
                catch (IOException e)
                {

                    return false;
                }
            }
            return _tokensRead.Count > 0;
        }

        private void ParseInputStream()
        {
            if (_inputStream.Peek() < 0) return;

            var line = _inputStream.ReadLine();
            if (line == null || line.Length <= 0) return;

            var list = new List<String>();
            list.AddRange(Regex.Split(line, "\\s+"));

            foreach (var token in list.Where(token => token.Trim().Length > 0))
            {
                _tokensRead.Enqueue(token);
            }

            if (_tokensRead.Count <= 0)
            {
                throw new IOException("No input read");
            }
        }
    }
}
