namespace Botox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class Botox
    {
        private readonly static Dictionary<Type, object> ResolveCache = new Dictionary<Type, object>();

        static Botox()
        {
        }

        public static void Registrer<T>(T resolutionObject)
        {
            ResolveCache.Add(typeof(T), resolutionObject);
        }

        public static void Registrer<T, TC>(TC resolutionObject) where TC : T
        {
            ResolveCache.Add(typeof(T), resolutionObject);
        }

        private static object Resolve(Type type)
        {
            if (!ResolveCache.ContainsKey(type))
            {
                throw new NotSupportedException("No resolve exists for : " + type);
            }
            return ResolveCache[type];
        }

        public static T Resolve<T>()
        {
            var resolvedObject = Resolve(typeof(T));
            return (T)resolvedObject;
        }

        public static T CreateInstanceOf<T>()
        {
            var constructor = GetNoneConstructor(typeof(T));
            var list = new List<object>();
            foreach (var parameterInfo in constructor.GetParameters())
            {
                var typeName = parameterInfo.ParameterType.FullName + "," + parameterInfo.ParameterType.Assembly;
                var type = Type.GetType(typeName);
                list.Add(Resolve(type));
            }
            return (T)constructor.Invoke(list.ToArray());
        }

        private static ConstructorInfo GetNoneConstructor(Type type)
        {
            var constructorWithParameters = type.GetConstructors().First(constructor => constructor.GetParameters().Length > 0);
            if (constructorWithParameters == null)
                throw new NotImplementedException("Non default constructor not implemented in :" + type);

            return constructorWithParameters;
        }

        public static void ClearAll()
        {
            ResolveCache.Clear();
        }
    }
}
