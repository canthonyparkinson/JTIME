using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JTIME.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Instant I = Instant.Test01();
            DateTimeOffset D = new DateTimeOffset(2018, 8, 7, 8, 30, 0, TimeSpan.Zero);

            Assert.AreEqual<DateTimeOffset>(D, I);
        }
    }
}
