using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesignPatterns;
using DesignPatterns.Creational;
using Autofac;

namespace DesignPatternTests.Creational
{
    [TestClass]
    public class AbstractFactoryTests : BaseTests
    {        

        [TestMethod]
        public void AbstractFactory_Works()
        {
            // arrange
            var outputWriter = new OutputWriter();
            AutoFacInstance.Container = base.GetAutoFacContainer(outputWriter);
            
            // Create and run the African animal world
            var africa = new AfricaFactory();
            var africanWorld = new AnimalWorld(africa);
            
            // Create and run the American animal world
            var america = new AmericaFactory();
            var americanWorld = new AnimalWorld(america);

            // act
            africanWorld.RunFoodChain();
            americanWorld.RunFoodChain();

            // assert
            Assert.AreEqual(outputWriter.Outputs.Count, 2);
            Assert.IsNotNull(outputWriter.Outputs);
            Assert.IsNotNull(outputWriter.Outputs.Find(x => x == "Lion eats Wildebeest"));
            Assert.IsNotNull(outputWriter.Outputs.Find(x => x == "Wolf eats Bison"));
        }
    }
}
