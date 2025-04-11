using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DivisionTests
    {
        [TestMethod]
        public void EqualValues_ShouldBeEqual()
        {
            int value = 7;

            FInt f1 = new FInt(value);
            FInt f2 = new FInt(value);

            Assert.IsTrue(f1 == f2);
            Assert.IsTrue(f1.Equals(f2));
            Assert.IsTrue(f2.Equals(f1));
            Assert.IsFalse(f1 != f2);
            Assert.IsFalse(f2 != f1);
        }

        [TestMethod]
        public void DifferentValues_ShouldNotBeEqual()
        {
            int value1 = 3;
            int value2 = 7;

            FInt f1 = new FInt(value1);
            FInt f2 = new FInt(value2);

            Assert.IsFalse(f1 == f2);
            Assert.IsFalse(f1.Equals(f2));
            Assert.IsFalse(f2.Equals(f1));
            Assert.IsTrue(f1 != f2);
            Assert.IsTrue(f2 != f1);
        }

        [DataTestMethod]
        [DataRow(-1, -1, true)]
        [DataRow(0, 0, true)]
        [DataRow(1, 1, true)]
        [DataRow(-1, -2, true)]
        [DataRow(0, -1, true)]
        [DataRow(1, 0, true)]
        [DataRow(2, 1, true)]
        [DataRow(-2, -1, false)]
        [DataRow(-1, 0, false)]
        [DataRow(0, 1, false)]
        [DataRow(1, 2, false)]
        public void GreaterThanOrEqualTo_ShouldWorkAsExpected(int value1, int value2, bool expectedResult)
        {
            FInt f1 = new FInt(value1);
            FInt f2 = new FInt(value2);

            Assert.AreEqual(expectedResult, f1 >= f2);
        }

        [DataTestMethod]
        [DataRow(-1, -2, true)]
        [DataRow(0, -1, true)]
        [DataRow(1, 0, true)]
        [DataRow(2, 1, true)]
        [DataRow(-1, -1, false)]
        [DataRow(0, 0, false)]
        [DataRow(1, 1, false)]
        [DataRow(-2, -1, false)]
        [DataRow(-1, 0, false)]
        [DataRow(0, 1, false)]
        [DataRow(1, 2, false)]
        public void GreaterThan_ShouldWorkAsExpected(int value1, int value2, bool expectedResult)
        {
            FInt f1 = new FInt(value1);
            FInt f2 = new FInt(value2);

            Assert.AreEqual(expectedResult, f1 > f2);
        }

        [DataTestMethod]
        [DataRow(-1, -1, true)]
        [DataRow(0, 0, true)]
        [DataRow(1, 1, true)]
        [DataRow(-2, -1, true)]
        [DataRow(-1, 0, true)]
        [DataRow(0, 1, true)]
        [DataRow(1, 2, true)]
        [DataRow(-1, -2, false)]
        [DataRow(0, -1, false)]
        [DataRow(1, 0, false)]
        [DataRow(2, 1, false)]
        public void LessThanOrEqualTo_ShouldWorkAsExpected(int value1, int value2, bool expectedResult)
        {
            FInt f1 = new FInt(value1);
            FInt f2 = new FInt(value2);

            Assert.AreEqual(expectedResult, f1 <= f2);
        }

        [DataTestMethod]
        [DataRow(-2, -1, true)]
        [DataRow(-1, 0, true)]
        [DataRow(0, 1, true)]
        [DataRow(1, 2, true)]
        [DataRow(-1, -1, false)]
        [DataRow(0, 0, false)]
        [DataRow(1, 1, false)]
        [DataRow(-1, -2, false)]
        [DataRow(0, -1, false)]
        [DataRow(1, 0, false)]
        [DataRow(2, 1, false)]
        public void LessThan_ShouldWorkAsExpected(int value1, int value2, bool expectedResult)
        {
            FInt f1 = new FInt(value1);
            FInt f2 = new FInt(value2);

            Assert.AreEqual(expectedResult, f1 < f2);
        }
    }
}