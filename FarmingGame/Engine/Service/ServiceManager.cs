using System;
using System.Linq;
using System.Reflection;

namespace FarmingGame.Engine.Service;

public static class ServiceManager
{
    public static void RegisterServices()
    {
        var types = Assembly.GetExecutingAssembly().GetTypes();
        var serviceTypes = types
            .Where(t => 
                t.GetCustomAttributes(typeof(ServiceAttribute), false).Length > 0);

        foreach (var type in serviceTypes)
        {
            var instance = type
                .GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?
                .GetValue(null);
            Console.WriteLine($"Initialized service: {type.Name}");
        }
    }
}