using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace Tests
{
    [TestClass]
    public class DivisionTests
    {
        [TestMethod]
        public void DivisionByZero_ShouldThrowException()
        {
            Assert.ThrowsException<DivideByZeroException>(() => 1.FI() / 0.FI());
            Assert.ThrowsException<DivideByZeroException>(() => 1.FI() / 0);
            Assert.ThrowsException<DivideByZeroException>(() => 1.FI() / (long)0);
        }

        private void _assertDivision(FInt result, int expectedWhole, int expectedDecimal)
        {
            FInt shiftedDecimal = expectedDecimal;

            while (shiftedDecimal.Decimal != 0)
            {
                shiftedDecimal *= 10;
            }

            Assert.AreEqual(expectedWhole.FI(), FInt.Truncate(result));
            Assert.AreEqual(expectedDecimal.FI(), shiftedDecimal);
        }

        private void _assertDivision<T1, T2>(T1 left, T2 right, Func<T1, T2, FInt> divide, int expectedWhole, int expectedDecimal)
            where T1 : INumber<T1>
            where T2 : INumber<T2>
        {
            _assertDivision(divide(left, right), expectedWhole, expectedDecimal);
            _assertDivision(divide(-left, right), -expectedWhole, expectedDecimal);
            _assertDivision(divide(left, -right), -expectedWhole, expectedDecimal);
            _assertDivision(divide(-left, -right), expectedWhole, expectedDecimal);
        }

        [DataTestMethod]
        [DataRow(0, 1, 0, 0)]
        [DataRow(1, 1, 1, 0)]
        [DataRow(4, 2, 2, 0)]
        [DataRow(1, 2, 0, 5)]
        [DataRow(1, 4, 0, 25)]
        [DataRow(3, 2, 1, 5)]
        [DataRow(7, 3, 2, 333333)]
        public void Division_ShouldWorkAsExpected(int left, int right, int expectedWhole, int expectedDecimal)
        {
            Func<int, int, FInt> divisionByFInt = (l, r) => l.FI() / r.FI();
            Func<int, int, FInt> divisionByInt = (l, r) => l.FI() / r;
            Func<int, long, FInt> divisionByLong = (l, r) => l.FI() / r;

            _assertDivision(left, right, divisionByFInt, expectedWhole, expectedDecimal);
            _assertDivision(left, right, divisionByInt, expectedWhole, expectedDecimal);
            _assertDivision(left, right, divisionByLong, expectedWhole, expectedDecimal);
        }

        [DataTestMethod]
        [DataRow(1, 2, 1, 4, 2, 0)]
        [DataRow(2, 10, 3, 10, 0, 666666)]
        public void DivisionWithFractions_ShouldWorkAsExpected(int left, int right, int left2, int right2, int expectedWhole, int expectedDecimal)
        {
            void assertDivision(FInt l, FInt r, int ew, int ed)
            {
                FInt result = l / r;

                FInt shiftedDecimal = ed;

                while (shiftedDecimal.Decimal != 0)
                {
                    shiftedDecimal *= 10;
                }

                Assert.AreEqual(ew.FI(), FInt.Truncate(result));
                Assert.AreEqual(ed.FI(), shiftedDecimal);
            }

            FInt numerator = left.FI() / right.FI();
            FInt denominator = left2.FI() / right2.FI();

            assertDivision(numerator, denominator, expectedWhole, expectedDecimal);
            assertDivision(-numerator, denominator, -expectedWhole, expectedDecimal);
            assertDivision(numerator, -denominator, -expectedWhole, expectedDecimal);
            assertDivision(-numerator, -denominator, expectedWhole, expectedDecimal);
        }
    }
}