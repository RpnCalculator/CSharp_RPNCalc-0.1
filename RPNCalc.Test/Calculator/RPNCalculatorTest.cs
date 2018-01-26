using System;
using System.IO;
using Csd.Calculator.TestDuplicates;
using NUnit.Framework;
using Moq;


namespace Csd.Calculator
{
    public class RPNCalculatorTest
    {
        private ConsoleOutputFake _fakeOutput;
        private RPNCalculator _calc;

        [SetUp]
        public void SetUp()
        {
            _fakeOutput = new ConsoleOutputFake();
            _calc = new RPNCalculator(_fakeOutput);
        }

        private void VerifyOutput(String[] expectedOutput)
        {
            String[] consoleOutput = _fakeOutput.GetStrings();

            Assert.AreEqual(expectedOutput.Length, consoleOutput.Length,"output line count");
            for (int i = 0; i < expectedOutput.Length; i++)
            {
               Assert.AreEqual(expectedOutput[i],consoleOutput[i]);
            }
        }

        [Test]
        public void SimpleAddition_GoodOutputForPlus()
        {
            TextReader input = new StringReader("5 3 +");
            String[] expectedOutput = { "[5]", "[5, 3]", "[5, 3, +]" };
            _calc.ProcessInput(new StreamTokenizer(input));

            VerifyOutput(expectedOutput);
        }

    }
}
