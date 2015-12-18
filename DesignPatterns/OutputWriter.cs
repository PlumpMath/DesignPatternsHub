using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class OutputWriter : IOutputWriter
    {
        public List<string> Outputs { get; set; }
        
        public void Write(string output)
        {
            if (Outputs == null)
            {
                Outputs = new List<string>();
            }

            Outputs.Add(output);
        }
    }

    public interface IOutputWriter
    {
        void Write(string output);
    }
}
