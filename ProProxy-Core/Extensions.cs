using System;
using System.Reflection;
using ImpromptuInterface;

namespace ProProxy
{
    public static class Extensions
    {
        public static IProxy<T> Proxy<T>(this T instance, IShell shell)
        {
            Type a = shell.CorrespondingProxy;
            Type b = a.MakeGenericType(typeof(T));
            return Activator.CreateInstance(b, BindingFlags.CreateInstance, null, instance) as IProxy<T>;
        }

        // Individual value constructor

        public static IProxy<T> ProxyWith<T>(this T instance, IShell withShell)
        {
            if (expr)
            {
                
            }
            var product = new LogProxy<T>(logOnEntry, logOnExit, logOnFailure, instance).ActLike<I>();

            return product;
        }
    }
}