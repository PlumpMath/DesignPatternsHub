using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesignPatterns.Behavioural.Command.Calculator;
using DesignPatterns.Behavioural.Command.GOF;
using DesignPatterns.Behavioural.Command.Robot;
using DesignPatterns;
using System.Collections.Generic;

namespace DesignPatternTests.Behavioural
{
    [TestClass]
    public class CommandTests : BaseTests
    {
        [TestMethod]
        public void Command_Works()
        {
            // arrange
            User user = new User();

            // act / assert

            // User presses calculator buttons
            user.Compute('+', 100);
            Assert.AreEqual(user.ReadCalculatorValue(), 100);

            user.Compute('-', 50);
            Assert.AreEqual(user.ReadCalculatorValue(), 50);

            user.Compute('*', 10);
            Assert.AreEqual(user.ReadCalculatorValue(), 500);

            user.Compute('/', 2);
            Assert.AreEqual(user.ReadCalculatorValue(), 250);

            // Undo 4 commands
            user.Undo(4);
            Assert.AreEqual(user.ReadCalculatorValue(), 0);

            // Redo 3 commands
            user.Redo(3);
            Assert.AreEqual(user.ReadCalculatorValue(), 500);            
        }

        [TestMethod]
        public void Command_GangOfFourWorks()
        {
            var outputWriter = new OutputWriter();
            AutoFacInstance.Container = base.GetAutoFacContainer(outputWriter);

            Command_GangOfFour.Application.Run();

            Assert.IsNotNull(outputWriter.Outputs);
            Assert.IsNotNull(outputWriter.Outputs.Find(x => x == "Called Receiver.Action()"));

        }

        [TestMethod]
        public void Command_RobotWorks()
        {
            var outputWriter = new OutputWriter();
            AutoFacInstance.Container = base.GetAutoFacContainer(outputWriter);

            // arrange
            var warehouse = new Warehouse(5, 5);            
            var robot = new Robot(1, 2, Facing.N) { Boundary = warehouse };
            
            // act 
            var controller = new RobotController(robot);                
            controller.Compute("<^<^<^<^^");   
                                     
            
            // assert
            Assert.AreEqual(robot.GetLocation(), "1 3 N");
        }

    }
}
