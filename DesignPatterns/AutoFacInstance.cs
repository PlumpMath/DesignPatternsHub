using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class AutoFacInstance
    {
        public static IContainer Container { get; set; }

        public static IContainer GetInstance()
        {
            if (Container == null)
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<OutputWriter>().As<IOutputWriter>();
                return builder.Build();
            }        

            return Container;
        }
    }
}
