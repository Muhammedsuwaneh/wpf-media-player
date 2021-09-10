using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace MediaPlayer
{
    /// <summary>
    /// Stores the definition of all the different classes we want to instantiate 
    /// </summary>
    public static class ContainerConfig  
    {
        /// <summary>
        /// Configures and builts the container
        /// </summary>
        /// <returns></returns>
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<WindowResizer>().As<IWindowResizer>();

            return builder.Build();
        }
    }
}
