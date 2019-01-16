using Microsoft.VisualStudio.TestTools.UnitTesting;
using TxtLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TxtLogger.Tests
{
    [TestClass()]
    public class TxtLoggerTests
    {
        [TestMethod()]
        public void TxtLoggerTest()
        {
            TxtLogger logger = new TxtLogger();

            Assert.IsNotNull(logger);
        }

    }
}