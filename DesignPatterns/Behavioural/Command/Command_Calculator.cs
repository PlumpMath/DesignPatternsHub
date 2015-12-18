using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioural.Command.Calculator
{
    /* 
        Encapsulate a request as an object, thereby letting you parameterize clients with different requests,
        queue or log requests, and support undoable operations.
    */

    /// <summary>
    /// The 'Command' abstract class
    /// </summary>
    public abstract class Command_Calculator
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    public class CalculatorCommand : Command_Calculator
    {
        private char _operator;
        private int _operand;
        private Calculator _calculator;

        // Constructor
        public CalculatorCommand(Calculator calculator,
          char @operator, int operand)
        {
            this._calculator = calculator;
            this._operator = @operator;
            this._operand = operand;
        }

        // Gets operator
        public char Operator
        {
            set { _operator = value; }
        }

        // Get operand
        public int Operand
        {
            set { _operand = value; }
        }

        // Execute new command
        public override void Execute()
        {
            _calculator.Operation(_operator, _operand);
        }

        // Unexecute last command
        public override void UnExecute()
        {
            _calculator.Operation(Undo(_operator), _operand);
        }

        // Returns opposite operator for given operator
        private char Undo(char @operator)
        {
            switch (@operator)
            {
                case '+': return '-';
                case '-': return '+';
                case '*': return '/';
                case '/': return '*';
                default:
                    throw new ArgumentException("@operator");
            }
        }
    }

    /// <summary>
    /// The 'Receiver' class
    /// </summary>
    public class Calculator
    {
        private int _curr = 0;

        public void Operation(char @operator, int operand)
        {
            switch (@operator)
            {
                case '+': _curr += operand; break;
                case '-': _curr -= operand; break;
                case '*': _curr *= operand; break;
                case '/': _curr /= operand; break;
            }

            var writer = AutoFacInstance.Container.Resolve<IOutputWriter>();
            writer.Write(string.Format(
              "Current value = {0,3} (following {1} {2})",
              _curr, @operator, operand));
        }

        // not part of pattern just to check.
        public int GetValue()
        {
            return _curr;
        }
    }

    /// <summary>
    /// The 'Invoker' class
    /// </summary>
    public class User
    {
        // Initializers
        private Calculator _calculator = new Calculator();
        private List<Command_Calculator> _commands = new List<Command_Calculator>();
        private int _current = 0;
        private IOutputWriter writer = AutoFacInstance.Container.Resolve<IOutputWriter>();

        public void Redo(int levels)
        {
            writer.Write(string.Format("\n---- Redo {0} levels ", levels));
            // Perform redo operations
            for (int i = 0; i < levels; i++)
            {
                if (_current < _commands.Count - 1)
                {
                    Command_Calculator command = _commands[_current++];
                    command.Execute();
                }
            }
        }

        public void Undo(int levels)
        {
            writer.Write(string.Format("\n---- Undo {0} levels ", levels));
            // Perform undo operations
            for (int i = 0; i < levels; i++)
            {
                if (_current > 0)
                {
                    Command_Calculator command = _commands[--_current] as Command_Calculator;
                    command.UnExecute();
                }
            }
        }

        public void Compute(char @operator, int operand)
        {
            // Create command operation and execute it
            Command_Calculator command = new CalculatorCommand(
              _calculator, @operator, operand);
            command.Execute();
            
            // Add command to undo list
            _commands.Add(command);
            _current++;
        }

        // just to check - not part of pattern
        public int ReadCalculatorValue()
        {
            return _calculator.GetValue();
        }
    }
}
