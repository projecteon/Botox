namespace Botox
{
    using System.Collections.Generic;
    using System.Reflection;

    public class ContructorCache
    {
        public ContructorCache(ConstructorInfo constructor,List<object> resolvedParamters)
        {
            this.Constructor = constructor;
            this.ResolvedParamters = resolvedParamters;
        }

        public ConstructorInfo Constructor { get; private set; }

        public List<object> ResolvedParamters{ get; private set; }
    }
}
