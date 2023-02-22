using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Autofac;
using StepsRecorderAdvanced.Core.Models;
using StepsRecorderAdvanced.Core.Models.Interfaces;

using Microsoft.Extensions.Configuration;
using StepsRecorderAdvanced.Avalonia.GUI.Models;
using StepsRecorderAdvanced.Avalonia.GUI.Models.Interfaces;
using StepsRecorderAdvanced.Avalonia.GUI.Models.MouseHookImpl;

namespace StepsRecorderAdvanced.Avalonia.GUI.DependencyInjection
{
    public static class ContainerConfig
    {
        #region Container Initialization/Configuration

        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            RegisterPlatformSpecificTypes(builder);
            RegisterOpenGenericTypes(builder);
            RegisterTypeModels(builder);
            RegisterTypeViewModels(builder);
            RegisterTypeSingletons(builder);
            
            RegisterAppSettingsConfiguration(builder);
            
            return builder.Build();
        }

        #endregion

        #region specific builder methods

        #region platform specific registration

        private static void RegisterPlatformSpecificTypes(ContainerBuilder builder)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                RegisterLinuxTypes(builder);
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                RegisterOSXTypes(builder);
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                RegisterWindowsTypes(builder);
        }

        private static void RegisterLinuxTypes(ContainerBuilder builder)
        {
            
        }

        private static void RegisterWindowsTypes(ContainerBuilder builder)
        {
            builder.RegisterType<WindowsMouseHook>().As<IMouseHook>();
        }

        private static void RegisterOSXTypes(ContainerBuilder builder)
        {
            
        }

        #endregion

        private static void RegisterOpenGenericTypes(ContainerBuilder builder)
        {
        }
        
        private static void RegisterTypeModels(ContainerBuilder builder)
        {
          //Link up all Models within StepsRecorderAdvanced.Core.Models with the corresponding interfaces. Name-Convention I<NAMEOFTYPE> required
            _ = builder.RegisterAssemblyTypes(Assembly.Load("StepsRecorderAdvanced.Core"))
                    .Where(t => GetMatchingInterface(t) is not null)
                    .As(t => GetMatchingInterface(t)!);
        }

        private static void RegisterTypeViewModels(ContainerBuilder builder)
        {
            //Link up all ViewModels within StepsRecorderAdvanced.Avalonia.GUI.ViewModels with the corresponding interfaces. Name-Convention I<NAMEOFTYPE> required
            _ = builder.RegisterAssemblyTypes(Assembly.Load(Assembly.GetExecutingAssembly().GetName().Name!))
                .Where(t => GetMatchingInterface(t) is not null)
                .As(t => GetMatchingInterface(t)!);
        }
        
        private static void RegisterTypeSingletons(ContainerBuilder builder)
        {
            builder.RegisterType<GUILogger>().As<ILog>().As<IGUILog>().SingleInstance();
            builder.RegisterType<SharedSettings>().As<ISharedSettings>().SingleInstance();

        }
        
        private static void RegisterAppSettingsConfiguration(ContainerBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            builder.RegisterInstance<IConfiguration>(configuration);
        }

        #endregion

        #region helper methods

        private static Type? GetMatchingInterface(Type type) => type.GetInterfaces()
        .FirstOrDefault(t => t.Name == "I" + type.Name);

        #endregion
    }
}