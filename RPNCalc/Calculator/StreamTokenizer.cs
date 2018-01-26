using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Csd.Calculator
{
    public class StreamTokenizer
    {
        private readonly TextReader _inputStream;

        private readonly Queue<String> _tokensRead = new Queue<string>();


        /**
         * Default constructor: uses Console.in for input stream
         * 
         */
        public StreamTokenizer()
        {
            _inputStream = Console.In;
        }

        /**
         * @param TextReader
         *
         */
        public StreamTokenizer(TextReader inputStream)
        {
            _inputStream = inputStream;
        }

        /**
         * @param String
         * 
         */
        public StreamTokenizer(String inputString)
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
