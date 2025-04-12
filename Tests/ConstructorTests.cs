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
        public void DecimalIntConstructor_EqualsExpected()
        {
            int integralValue = 10;
            int decimalValue = 5;

            //loop 1-6, multiply expected scale by 10 each loop
            for (int i = 1, expectedDecimalScale = 10; i <= 6; i++, expectedDecimalScale *= 10)
            {
                int decimalDigits = i;

                FInt expectedInt = integralValue;
                FInt expectedDecimal = decimalValue.FI() / expectedDecimalScale;
                FInt expectedValue = expectedInt + expectedDecimal;

                FInt result = new FInt(integralValue, decimalValue, decimalDigits);

                Assert.AreEqual(expectedValue, result);
            }
        }

        [TestMethod]
        public void DecimalIntConstructor_ThrowsOnNegativeValues()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FInt(-1, 1, 1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FInt(1, -1, 1));
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(FInt.PRECISION + 1)]
        public void DecimalIntConstructor_ThrowsOnInvalidNumDecimalDigits(int decimalDigits)
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
        public void DecimalIntConstructor_ThrowsOnInvalidDecimal(int decimalValue, int decimalDigits)
        {
            Assert.ThrowsException<ArgumentException>(() => new FInt(1, decimalValue, decimalDigits));
        }

        [TestMethod]
        public void DecimalLongConstructor_EqualsExpected()
        {
            long integralValue = 10;
            int decimalValue = 5;

            //loop 1-6, multiply expected scale by 10 each loop
            for (int i = 1, expectedDecimalScale = 10; i <= 6; i++, expectedDecimalScale *= 10)
            {
                int decimalDigits = i;

                FInt expectedInt = integralValue;
                FInt expectedDecimal = decimalValue.FI() / expectedDecimalScale;
                FInt expectedValue = expectedInt + expectedDecimal;

                FInt result = new FInt(integralValue, decimalValue, decimalDigits);

                Assert.AreEqual(expectedValue, result);
            }
        }

        [TestMethod]
        public void DecimalLongConstructor_ThrowsOnNegativeValues()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FInt(-1L, 1, 1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FInt(1L, -1, 1));
        }

        [TestMethod]
        public void DecimalLongConstructor_ThrowsOnIntegralOverflow()
        {
            Assert.ThrowsException<OverflowException>(() => new FInt(long.MaxValue, 0, 1));

            long scale = 1;
            for(int i = 0; i < FInt.PRECISION; i++)
            {
                scale *= 10;
            }

            long integralValue = (long.MaxValue / scale) + 1;

            //assert that our integral value is one more than the truncated max
            Assert.IsTrue(FInt.Truncate(FInt.MaxValue) == integralValue - 1);

            Assert.ThrowsException<OverflowException>(() => new FInt(integralValue, 0, 1));
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(FInt.PRECISION + 1)]
        public void DecimalLongConstructor_ThrowsOnInvalidNumDecimalDigits(int decimalDigits)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new FInt(1L, 1, decimalDigits));
        }

        [DataTestMethod]
        [DataRow(10, 1)]
        [DataRow(100, 2)]
        [DataRow(1_000, 3)]
        [DataRow(10_000, 4)]
        [DataRow(100_000, 5)]
        [DataRow(1_000_000, 6)]
        public void DecimalLongConstructor_ThrowsOnInvalidDecimal(int decimalValue, int decimalDigits)
        {
            Assert.ThrowsException<ArgumentException>(() => new FInt(1L, decimalValue, decimalDigits));
        }

        [TestMethod]
        public void DecimalLongConstructor_ThrowsOnOverflow()
        {
            long scale = 1;
            for (int i = 0; i < FInt.PRECISION; i++)
            {
                scale *= 10;
            }

            //get the max integral value based on PRECISION
            long integralValue = (long.MaxValue / scale);

            //get the decimal value as a long
            long decimalValueLong = long.MaxValue - (integralValue * scale);

            //assert we're not losing data when we convert to an int
            Assert.IsTrue(decimalValueLong < int.MaxValue);

            //convert decimal to an int
            int decimalValue = (int)decimalValueLong;

            //assert that our integral value is equal to the truncated max
            Assert.IsTrue(integralValue == FInt.Truncate(FInt.MaxValue));

            //assert that our decimal value is the same as the decimal on the max value
            Assert.IsTrue(decimalValue == (FInt.MaxValue.Decimal * scale));

            Assert.ThrowsException<OverflowException>(() => new FInt(integralValue, decimalValue + 1, FInt.PRECISION));
        }

        [TestMethod]
        public void DecimalConstructors_ShouldProduceSameValues()
        {
            int integralValue = 1;
            int decimalValue = 5;
            int numDecimalDigits = 1;
            
            FInt intResult = new FInt(integralValue, decimalValue, numDecimalDigits);
            FInt longResult = new FInt((long)integralValue, decimalValue, numDecimalDigits);

            Assert.AreEqual(intResult, longResult);
        }
    }
}