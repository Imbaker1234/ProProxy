using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeCache
{
    public static class DelegateCacheFactory
    {
        private static Dictionary<Type, Dictionary<string, dynamic>> MappedTypes
        = new Dictionary<Type, Dictionary<string, dynamic>>();

        private static readonly Type[] GenericActionArray = {
            typeof(Action<>),
            typeof(Action<,>),
            typeof(Action<,,>),
            typeof(Action<,,,>),
            typeof(Action<,,,,>),
            typeof(Action<,,,,,>),
            typeof(Action<,,,,,,>),
            typeof(Action<,,,,,,,>),
            typeof(Action<,,,,,,,,>),
            typeof(Action<,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,,,>),
            typeof(Action<,,,,,,,,,,,,,,,>)
        };

        private static readonly Type[] GenericFuncArray = {
            typeof(Func<>),
            typeof(Func<,>),
            typeof(Func<,,>),
            typeof(Func<,,,>),
            typeof(Func<,,,,>),
            typeof(Func<,,,,,>),
            typeof(Func<,,,,,,>),
            typeof(Func<,,,,,,,>),
            typeof(Func<,,,,,,,,>),
            typeof(Func<,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,,,>),
            typeof(Func<,,,,,,,,,,,,,,,,>)
        };

        public static Dictionary<string, dynamic> Create<T>(T instance)
        {
            // //If we've already mapped this type
            // if (MappedTypes.ContainsKey(typeof(T))) return MappedTypes[typeof(T)];

            var product = new Dictionary<string, dynamic>();
            foreach (var methodInfo in typeof(T).GetMethods())
            {
                //Get the total number of parameters and add their types to an List.
                List<Type> paramTypes = methodInfo.GetParameters().Select(p => p.ParameterType).ToList();

                //If we have a valid return and this is a func then add the return type the paramTypes list.
                if (methodInfo.ReturnType != typeof(void)) paramTypes.Add(methodInfo.ReturnType);

                //Depending on the return type return an action or func type of the appropriate number
                //of Generic parameters.
                Type delType = methodInfo.ReturnType == typeof(void)
                    ? GenericActionArray[paramTypes.Count - 1]
                    : GenericFuncArray[paramTypes.Count - 1];

                //Using the paramTypes list turn the base delegate type (Func or Action) into the correct
                //generic type such as (Func<string, int, DateTime).
                var finalGenericType = delType.MakeGenericType(paramTypes.ToArray());

                //Because we are dealing with a limitless number of types here we have to go with dynamic
                //as our type for strongDel and just assume that everything has come out okay. We've mapped
                //from the base method and created a strongly typed delegate in its image so everything
                //SHOULD be fine.
                dynamic strongDel = Convert.ChangeType(
                    methodInfo.CreateDelegate(finalGenericType, instance), finalGenericType);

                //Add the delegate name to the cache
                product.Add(methodInfo.Name, strongDel);
            }
            // MappedTypes.Add(typeof(T), product);
            return product;
        }
    }
}