using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Csd.Calculator
{
    public class StreamTokenizerTest
    {

        private static void VerifyInput(IEnumerable<string> expectedTokens, StreamTokenizer input)
        {
            Assert.True(input.TokenAvailable());
            foreach (var token in expectedTokens)
            {
                Assert.AreEqual(token, input.NextToken());
            }
            Assert.False(input.TokenAvailable());
        }

        [Test]
        public void ConstructorFakeStreamInput()
        {
            var input = new StreamTokenizer("12 5 9");

            String[] expectedTokens = { "12", "5", "9" };

            VerifyInput(expectedTokens, input);
        }

        [Test]
        public void NextTokenMixedOperandOperatorWhitespace()
        {
            var input = new StreamTokenizer("\t12 \t5\n9 + * -10 /");

            String[] expectedTokens = {"12","5","9","+","*","-10","/"};
            VerifyInput(expectedTokens, input);
        }

        [Test]
        public void NextTokenWhitespaceDelimitersRequired()
        {
            var input = new StreamTokenizer("\t12 \t 5  \t9   +*-10/");

            String[] expectedTokens = {"12","5","9","+*-10/"};

            VerifyInput(expectedTokens, input);
        }

    }
}
