using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void EmptyConstructor_ShouldEqualZero()
        {
            FInt fint = new FInt();

            Assert.AreEqual(0, fint);
        }

        [TestMethod]
        public void IntConstructor_ShouldEqualInt()
        {
            int intValue = 7;

            FInt fint = new FInt(intValue);

            Assert.AreEqual(intValue, fint);
        }

        [TestMethod]
        public void LongConstructor_ShouldEqualLong()
        {
            long longValue = 7;

            FInt fint = new FInt(longValue);

            Assert.AreEqual(longValue, fint);
        }

        [TestMethod]
        public void LongConstructor_ThrowsOnOverflow()
        {
            Assert.ThrowsException<ArgumentException>(() => new FInt(long.MaxValue));
        }
    }
}