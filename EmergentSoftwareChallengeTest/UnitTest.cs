using EmergentSoftwareChallenge.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmergentSoftwareChallengeTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CanCompare()
        {
            var comparer = new VersionComparer();
            Assert.AreEqual(0, comparer.Compare(default, default));
            Assert.AreEqual(0, comparer.Compare("", default));
            Assert.AreEqual(0, comparer.Compare(default, ""));
            Assert.AreEqual(-1, comparer.Compare("1", "bad"));
            Assert.AreEqual(1, comparer.Compare("bad", "1"));

            Assert.AreEqual(0, comparer.Compare("1", "1"));
            Assert.AreEqual(0, comparer.Compare("1", "1.0"));
            Assert.AreEqual(0, comparer.Compare("1", "1.0.0"));
            Assert.AreEqual(0, comparer.Compare("1.0", "1.0"));
            Assert.AreEqual(0, comparer.Compare("1.0", "1.0.0"));
            Assert.AreEqual(0, comparer.Compare("1.0.0", "1.0.0"));
            Assert.AreEqual(0, comparer.Compare("1.0.0", "1.0.0."));

            Assert.AreEqual(-1, comparer.Compare("1", "1.1"));
            Assert.AreEqual(-1, comparer.Compare("1", "1.0.1"));
            Assert.AreEqual(-1, comparer.Compare("1.1", "1.1.1"));
            Assert.AreEqual(-1, comparer.Compare("1.1.1", "1.1.2"));

            Assert.AreEqual(1, comparer.Compare("1.1", "1"));
            Assert.AreEqual(1, comparer.Compare("1.0.1", "1"));
            Assert.AreEqual(1, comparer.Compare("1.1.1", "1.1"));
            Assert.AreEqual(1, comparer.Compare("1.1.2", "1.1.1"));

            Assert.AreEqual(0, comparer.Compare("1", " 1 "));
            Assert.AreEqual(0, comparer.Compare("1", " 1. 0"));
            Assert.AreEqual(0, comparer.Compare("1", " 1. 0 . 0 "));
            Assert.AreEqual(0, comparer.Compare("1.0", " 1 . 0 "));
            Assert.AreEqual(0, comparer.Compare("1.0", " 1 . 0 . 0 "));
            Assert.AreEqual(0, comparer.Compare("1.0.0", " 1 . 0 . 0 "));
            Assert.AreEqual(0, comparer.Compare("1.0.0", " 1 . 0 . 0 . "));
        }
    }
}
