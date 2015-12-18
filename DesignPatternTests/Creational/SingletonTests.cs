using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesignPatterns.Creational;
using Moq;

namespace DesignPatternTests.Creational
{
    [TestClass]
    public class SingletonTests
    {
        [TestMethod]
        public void Singleton_Works()
        {
            // arrange & act
            var instance = Singleton.Instance();
            var another = Singleton.Instance();

            // assert

            Assert.AreEqual(instance, another);
        }
    }
}
