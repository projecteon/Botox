namespace Botox
{
    using System;
    using System.Collections.Generic;

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

        internal static object Resolve(Type type)
        {
            if (CannotResolve(type))
            {
                throw new NotSupportedException("No resolve exists for : " + type);
            }
            return ResolveCache[type];
        }

        internal static bool CannotResolve(Type type)
        {
            return !ResolveCache.ContainsKey(type);
        }

        public static T Resolve<T>()
        {
            var resolvedObject = Resolve(typeof(T));
            return (T)resolvedObject;
        }

        public static T CreateInstanceOf<T>()
        {
            var typeCache = ConstructorResolver.FindConstructor<T>();
            return (T)typeCache.Constructor.Invoke(typeCache.ResolvedParamters.ToArray());
        }

        public static T CreateInstanceOf<T>(Type type) 
        {
            if(!typeof(T).IsAssignableFrom(type))
                throw new InvalidCastException(type + " is not assignable from " + typeof(T));
            
            var typeCache = ConstructorResolver.FindConstructor(type);
            return (T)typeCache.Constructor.Invoke(typeCache.ResolvedParamters.ToArray());
        }   

        public static void ClearAll()
        {
            ResolveCache.Clear();
        }
    }
}
