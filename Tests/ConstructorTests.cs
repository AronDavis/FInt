using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void EmptyConstructor_EqualsZero()
        {
            FInt fint = new FInt();

            Assert.AreEqual(0, fint);
        }

        [TestMethod]
        public void IntConstructor_EqualsInt()
        {
            int expectedValue = 7;

            FInt fint = new FInt(expectedValue);

            Assert.AreEqual(expectedValue, fint);
        }

        [TestMethod]
        public void LongConstructor_EqualsLong()
        {
            long expectedValue = 7;

            FInt fint = new FInt(expectedValue);

            Assert.AreEqual(expectedValue, fint);
        }

        [TestMethod]
        public void LongConstructor_ThrowsOnOverflow()
        {
            Assert.ThrowsException<OverflowException>(() => new FInt(long.MaxValue));
        }


        [TestMethod]
        public void DecimalConstructor_EqualsExpected()
        {
            void assert(int intVal, int decVal, int decDigits, int decScale)
            {
                FInt fint = new FInt(intVal, decVal, decDigits);

                FInt expectedInt = intVal.FI();
                FInt expectedDecimal = decVal.FI() / decScale;

                FInt expectedValue = expectedInt;
                
                if (expectedInt < 0)
                {
                    expectedValue -= expectedDecimal;
                }
                else
                {
                    expectedValue += expectedDecimal;
                }

                Assert.AreEqual(expectedValue, fint);
            }

            int integerValue = 10;
            int decimalValue = 5;

            for (int i = 1, expectedDecimalScale = 10; i <= 6; i++, expectedDecimalScale *= 10)
            {
                int decimalDigits = i;

                assert(integerValue, decimalValue, decimalDigits, expectedDecimalScale);
                assert(-integerValue, decimalValue, decimalDigits, expectedDecimalScale);
            }
        }

        [TestMethod]
        public void DecimalConstructor_ThrowsOnNegativeDecimalValue()
        {
            Assert.ThrowsException<ArgumentException>(() => new FInt(1, -1, 1));
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(FInt.PRECISION + 1)]
        public void DecimalConstructor_ThrowsOnInvalidDecimalDigits(int decimalDigits)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FInt(1, 1, decimalDigits));
        }

        [DataTestMethod]
        [DataRow(10, 1)]
        [DataRow(100, 2)]
        [DataRow(1_000, 3)]
        [DataRow(10_000, 4)]
        [DataRow(100_000, 5)]
        [DataRow(1_000_000, 6)]
        public void DecimalConstructor_ThrowsOnInvalidDecimal(int decimalValue, int decimalDigits)
        {
            Assert.ThrowsException<ArgumentException>(() => new FInt(1, decimalValue, decimalDigits));
        }
    }
}