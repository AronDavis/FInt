using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DivisionTests
    {
        [DataTestMethod]
        [DataRow(0, 1, 0, 0)]
        [DataRow(1, 2, 0, 5)]
        [DataRow(1, 4, 0, 25)]
        [DataRow(3, 2, 1, 5)]
        [DataRow(7, 3, 2, 333333)]
        public void DivisionWithNonIntegerQuotient_ShouldWorkAsExpected(int left, int right, int expectedWhole, int expectedDecimal)
        {
            FInt result = left.FI() / right.FI();

            result = -1.FI() / 2;

            result = result.Decimal;

            FInt shiftedDecimal = expectedDecimal;

            while(shiftedDecimal.Decimal != 0)
            {
                shiftedDecimal *= 10;
            }
            
            Assert.AreEqual(expectedWhole.FI(), result - result.Decimal);
            Assert.AreEqual(expectedDecimal.FI(), shiftedDecimal);
        }

        [DataTestMethod]
        [DataRow(0, 1, 0)]
        [DataRow(1, 1, 1)]
        [DataRow(4, 2, 2)]
        public void DivisionWithIntegerQuotient_ShouldWorkAsExpected(int left, int right, int expectedResult)
        {
            void assertDivision(int l, int r, int e)
            {
                FInt result = l.FI() / r.FI();
                FInt expected = e;

                Assert.AreEqual(expected, result);
            }

            assertDivision(left, right, expectedResult);
            assertDivision(-left, right, -expectedResult);
            assertDivision(left, -right, -expectedResult);
            assertDivision(-left, -right, expectedResult);
        }
    }
}