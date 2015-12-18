using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesignPatterns;
using DesignPatterns.Behavioural.Observer;

namespace DesignPatternTests.Behavioural
{
    /// <summary>
    /// Summary description for ObserverTests
    /// </summary>
    [TestClass]
    public class ObserverTests : BaseTests
    {
        [TestMethod]
        public void Observer_GoogleObserved()
        {
            var outputWriter = new OutputWriter();
            AutoFacInstance.Container = base.GetAutoFacContainer(outputWriter);

            Observer.Application.Run();

            Assert.IsNotNull(outputWriter.Outputs);
            Assert.IsNotNull(outputWriter.Outputs.Find(x => x == "Google Observed"));

        }
    }
}
