using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ParseTests
    {
        private void _assertParse(string s, int expectedSign, long expectedInteger, int expectedDecimal, int expectedNumDecimalDigits)
        {
            FInt expectedResult = expectedSign * new FInt(expectedInteger, expectedDecimal, expectedNumDecimalDigits);
            FInt result = FInt.Parse(s);
            Assert.AreEqual(expectedResult, result);
        }

        [DataTestMethod]
        [DataRow("0", 0, 0, 1)]
        [DataRow("0.0", 0, 0, 1)]
        [DataRow("0.5", 0, 5, 1)]
        [DataRow(".5", 0, 5, 1)]
        [DataRow("1.0", 1, 0, 1)]
        [DataRow("1.", 1, 0, 1)]
        [DataRow("1.5", 1, 5, 1)]
        [DataRow("0.12", 0, 12, 2)]
        [DataRow("0.123", 0, 123, 3)]
        [DataRow("0.1234", 0, 1234, 4)]
        [DataRow("0.12345", 0, 12345, 5)]
        [DataRow("0.123456", 0, 123456, 6)]
        [DataRow("0.1234567", 0, 123456, 6)]
        [DataRow("9223372036854.775807", 9223372036854, 775807, 6)]
        public void Parse_ShouldWorkAsExpected(string s, long expectedIntegral, int expectedDecimal, int expectedNumDecimalDigits)
        {
            _assertParse(s, 1, expectedIntegral, expectedDecimal, expectedNumDecimalDigits);
            _assertParse($"-{s}", -1, expectedIntegral, expectedDecimal, expectedNumDecimalDigits);
        }
    }
}