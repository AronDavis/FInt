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

        [DataTestMethod]
        [DataRow(0, 1, 0, 0)]
        [DataRow(1, 1, 1, 0)]
        [DataRow(4, 2, 2, 0)]
        [DataRow(1, 2, 0, 5)]
        [DataRow(1, 4, 0, 25)]
        [DataRow(3, 2, 1, 5)]
        [DataRow(7, 3, 2, 333333)]
        public void DivisionByFInt_ShouldWorkAsExpected(int left, int right, int expectedWhole, int expectedDecimal)
        {
            void assertDivision(int l, int r, int ew, int ed)
            {
                FInt result = l.FI() / r.FI();

                FInt shiftedDecimal = ed;

                while (shiftedDecimal.Decimal != 0)
                {
                    shiftedDecimal *= 10;
                }

                Assert.AreEqual(ew.FI(), result.Sign * ((result.Sign * result) - result.Decimal));
                Assert.AreEqual(ed.FI(), shiftedDecimal);
            }

            assertDivision(left, right, expectedWhole, expectedDecimal);
            assertDivision(-left, right, -expectedWhole, expectedDecimal);
            assertDivision(left, -right, -expectedWhole, expectedDecimal);
            assertDivision(-left, -right, expectedWhole, expectedDecimal);
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

                Assert.AreEqual(ew.FI(), result.Sign * ((result.Sign * result) - result.Decimal));
                Assert.AreEqual(ed.FI(), shiftedDecimal);
            }

            FInt numerator = left.FI() / right.FI();
            FInt denominator = left2.FI() / right2.FI();

            assertDivision(numerator, denominator, expectedWhole, expectedDecimal);
            assertDivision(-numerator, denominator, -expectedWhole, expectedDecimal);
            assertDivision(numerator, -denominator, -expectedWhole, expectedDecimal);
            assertDivision(-numerator, -denominator, expectedWhole, expectedDecimal);
        }

        [DataTestMethod]
        [DataRow(0, 1, 0, 0)]
        [DataRow(1, 1, 1, 0)]
        [DataRow(4, 2, 2, 0)]
        [DataRow(1, 2, 0, 5)]
        [DataRow(1, 4, 0, 25)]
        [DataRow(3, 2, 1, 5)]
        [DataRow(7, 3, 2, 333333)]
        public void DivisionByInt_ShouldWorkAsExpected(int left, int right, int expectedWhole, int expectedDecimal)
        {
            void assertDivision(int l, int r, int ew, int ed)
            {
                FInt result = l.FI() / r;

                FInt shiftedDecimal = ed;

                while (shiftedDecimal.Decimal != 0)
                {
                    shiftedDecimal *= 10;
                }

                Assert.AreEqual(ew.FI(), result.Sign * ((result.Sign * result) - result.Decimal));
                Assert.AreEqual(ed.FI(), shiftedDecimal);
            }

            assertDivision(left, right, expectedWhole, expectedDecimal);
            assertDivision(-left, right, -expectedWhole, expectedDecimal);
            assertDivision(left, -right, -expectedWhole, expectedDecimal);
            assertDivision(-left, -right, expectedWhole, expectedDecimal);
        }

        [DataTestMethod]
        [DataRow(0, 1, 0, 0)]
        [DataRow(1, 1, 1, 0)]
        [DataRow(4, 2, 2, 0)]
        [DataRow(1, 2, 0, 5)]
        [DataRow(1, 4, 0, 25)]
        [DataRow(3, 2, 1, 5)]
        [DataRow(7, 3, 2, 333333)]
        public void DivisionByLong_ShouldWorkAsExpected(int left, long right, int expectedWhole, int expectedDecimal)
        {
            void assertDivision(int l, long r, int ew, int ed)
            {
                FInt result = l.FI() / r;

                FInt shiftedDecimal = ed;

                while (shiftedDecimal.Decimal != 0)
                {
                    shiftedDecimal *= 10;
                }

                Assert.AreEqual(ew.FI(), result.Sign * ((result.Sign * result) - result.Decimal));
                Assert.AreEqual(ed.FI(), shiftedDecimal);
            }

            assertDivision(left, right, expectedWhole, expectedDecimal);
            assertDivision(-left, right, -expectedWhole, expectedDecimal);
            assertDivision(left, -right, -expectedWhole, expectedDecimal);
            assertDivision(-left, -right, expectedWhole, expectedDecimal);
        }
    }
}