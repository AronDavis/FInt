namespace Tests
{
    [TestClass]
    public class ConstructorTests
    {
        [TestMethod]
        public void EmptyConstructorShouldEqualZero()
        {
            FInt fint = new FInt();

            Assert.AreEqual(0, fint);
        }

        [TestMethod]
        public void IntConstructorShouldEqualInt()
        {
            int intValue = 7;

            FInt fint = new FInt(intValue);

            Assert.AreEqual(intValue, fint);
        }

        [TestMethod]
        public void BREAK_TESTS()
        {
            Assert.AreEqual(0, 2);
        }
    }
}