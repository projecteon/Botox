﻿namespace Botox
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class ConstructorResolver
    {
        private static readonly Dictionary<Type, ContructorCache> CreatedTypesCache =
            new Dictionary<Type, ContructorCache>();

        public static ContructorCache FindConstructor<T>()
        {
            if (CreatedTypesCache.ContainsKey(typeof(T)))
            {
                return CreatedTypesCache[typeof(T)];
            }

            return FindConstructorAndAddToCache<T>();
        }

        private static ContructorCache FindConstructorAndAddToCache<T>()
        {
            var constructor = ConstructorsSortedByParametersDescending(typeof(T));
            if (constructor.Any(ValidConstructor<T>))
            {
                return CreatedTypesCache[typeof(T)];
            }

            throw new NotSupportedException("No contructor found with valid resolutions in cache.");
        }

        private static bool ValidConstructor<T>(ConstructorInfo constructorInfo)
        {
            var list = new List<object>();
            foreach (var parameterInfo in constructorInfo.GetParameters())
            {
                var typeName = parameterInfo.ParameterType.FullName + "," + parameterInfo.ParameterType.Assembly;
                var typeToResolve = Type.GetType(typeName);
                if (Botox.CannotResolve(typeToResolve))
                {
                    return false;
                }

                list.Add(Botox.Resolve(typeToResolve));
            }

            CreatedTypesCache.Add(typeof(T), new ContructorCache(constructorInfo, list));
            return true;
        }

        private static IEnumerable<ConstructorInfo> ConstructorsSortedByParametersDescending(Type type)
        {
            var constructorWithParameters = type.GetConstructors().Where(constructor => constructor.GetParameters().Length > 0);
            if (constructorWithParameters == null)
            {
                throw new NotImplementedException("Non default constructor not implemented in :" + type);
            }

            return constructorWithParameters.OrderByDescending(constructor => constructor.GetParameters().Length);
        }
    }
}