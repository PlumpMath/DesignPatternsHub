using Autofac;
using DesignPatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternTests
{
    public abstract class BaseTests
    {
        protected IContainer GetAutoFacContainer(IOutputWriter writer)
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(writer).As<IOutputWriter>();
            return builder.Build();
        }
    }
}
