using System;
using System.Linq;
using System.Reflection;
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

            // register all interfaces 
            builder.RegisterAssemblyTypes(Assembly.Load(nameof(MediaPlayer)))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            return builder.Build();
        }
    }
}
