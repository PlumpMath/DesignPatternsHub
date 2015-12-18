using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioural.Command.Robot
{
    public interface IRobotCommand
    {
        void Execute();
    }

    /// <summary>
    /// The 'Command' abstract class
    /// </summary>
    public abstract class RobotCommand : IRobotCommand
    {
        public abstract void Execute();
    }

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    public class RobotMoveForwardCommand : RobotCommand
    {
        private IRobot _robot;

        // Constructor
        public RobotMoveForwardCommand(IRobot robot)
        {
            this._robot = robot;
        }        
        
        // Execute new command
        public override void Execute()
        {
            _robot.MoveForward();
        }                
    }

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    public class RobotTurnLeftCommand : RobotCommand
    {
        private IRobot _robot;

        // Constructor
        public RobotTurnLeftCommand(IRobot robot)
        {
            this._robot = robot;
        }

        // Execute new command
        public override void Execute()
        {
            _robot.TurnLeft();              
        }
    }

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    public class RobotTurnRightCommand : RobotCommand
    {
        private IRobot _robot;

        // Constructor
        public RobotTurnRightCommand(IRobot robot)
        {
            this._robot = robot;
        }

        // Execute new command
        public override void Execute()
        {
            this._robot.TurnRight();
        }
    }

    public interface IRobot
    {
        void MoveForward();
        void TurnRight();
        void TurnLeft();
        string GetLocation();             
    }

    public enum Facing
    {
        N, E, S, W
    }

    /// <summary>
    /// The 'Receiver' class
    /// </summary>
    public class Robot : IRobot
    {
        public  IBoundary Boundary { get; set; }

        public Robot(int x = 0, int y = 0, Facing facing = Facing.N)
        {
            this.X = x;
            this.Y = y;
            this.FacingDirection = facing;
        }

        private int X { get; set; }
        
        private int Y { get; set; }

        private Facing FacingDirection { get; set; }
        
        private bool CanMoveForward()
        {
            if (Boundary == null)
                return true;

            if (Boundary.GetMaxHeight() > Y && FacingDirection == Facing.N)
            {
                return true;
            }

            if (Boundary.GetMaxWidth() > X && FacingDirection == Facing.E)
            {
                return true;
            }

            if (FacingDirection == Facing.S && Y > 0)
            {
                return true;
            }

            if (FacingDirection == Facing.W && X > 0)
            {
                return true;
            }

            return false;
        }
        
        public void MoveForward()
        {
            if (!CanMoveForward())
            {
                return;
            }

            if (FacingDirection == Facing.N)
            {
                Y++;
            }
            else if (FacingDirection == Facing.E)
            {
                X++;                
            }
            else if (FacingDirection == Facing.S)
            {
                Y--;
            }
            else if (FacingDirection == Facing.W)
            {
                X--;
            }
        }

        private void Turn(Facing facing)
        {
            FacingDirection = facing;
        }

        public void TurnRight()
        {
            if (FacingDirection == Facing.N)
            {
                Turn(Facing.E);
            }
            else if (FacingDirection == Facing.E)
            {
                Turn(Facing.S);
            }
            else if (FacingDirection == Facing.S)
            {
                Turn(Facing.W);
            }
            else if (FacingDirection == Facing.W)
            {
                Turn(Facing.N);
            }
        }

        public void TurnLeft()
        {
            if (FacingDirection == Facing.N)
            {
                Turn(Facing.W);
            }
            else if (FacingDirection == Facing.W)
            {
                Turn(Facing.S);
            }
            else if (FacingDirection == Facing.S)
            {
                Turn(Facing.E);
            }
            else if (FacingDirection == Facing.E)
            {
                Turn(Facing.N);
            }
        }

        public string GetLocation()
        {
            return string.Format("{0} {1} {2}", X, Y, FacingDirection.ToString());
        }
    }

    public interface IBoundary
    {
        int GetMaxWidth();
        int GetMaxHeight();
    }

    public class Warehouse : IBoundary
    {
        public Warehouse(int x = 0, int y = 0)
        {
            this.X = x;
            this.Y = y;
        }

        private int X { get; set; }
        private int Y { get; set; }

        public int GetMaxWidth() { return X; }
        public int GetMaxHeight() { return Y; }
    }
    
    /// <summary>
    /// The 'Invoker' class
    /// </summary>
    public class RobotController 
    {
        // Initializers
        private IRobot _robot;
        private List<IRobotCommand> _commands = new List<IRobotCommand>();
        private IOutputWriter writer = AutoFacInstance.Container.Resolve<IOutputWriter>();
        
        public RobotController(IRobot robot)
        {
            _robot = robot;
        }

        public void Compute(string operations)
        {
            foreach (var @operator in operations.ToCharArray())
            {
                if (@operator == '^')
                {                    
                    // Create command operation and execute it                    
                    var command = new RobotMoveForwardCommand(_robot);
                    command.Execute();
                }

                if (@operator == '<')
                {
                    // Create command operation and execute it
                    var command = new RobotTurnLeftCommand(_robot);
                    command.Execute();
                }

                if (@operator == '>')
                {
                    // Create command operation and execute it
                    var command = new RobotTurnRightCommand(_robot);
                    command.Execute();
                }

                writer.Write(_robot.GetLocation());
            }
        }        
    }

    
}
