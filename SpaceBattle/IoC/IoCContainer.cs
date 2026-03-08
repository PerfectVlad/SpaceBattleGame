using System;
using System.Collections.Generic;

namespace SpaceBattle.IoC
{
    // IoC контейнер
    public static class IoCContainer
    {
        private static readonly Dictionary<string, Func<object[], object>> dependencies 
            = new Dictionary<string, Func<object[], object>>();
        
        public static void Register(string key, Func<object[], object> resolver)
        {
            dependencies[key] = resolver;
        }
        
        public static T Resolve<T>(string key, params object[] args)
        {
            if (!dependencies.ContainsKey(key))
                throw new ArgumentException($"Dependency not found: {key}");
                
            return (T)dependencies[key](args);
        }
    }
}