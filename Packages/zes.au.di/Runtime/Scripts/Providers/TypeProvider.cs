using System;
using System.Collections.Generic;

namespace Au.DI.Providers
{
    internal class TypeProvider : Provider
    {
        public TypeProvider(Type type)
        {
            this.type = type;
        }

        object cache;
        readonly Type type;

        protected override object GetValue(Container container)
        {
            if (cache != null)
            {
                return cache;
            }

            var ctor = type.GetConstructors()[0];
            var pis = ctor.GetParameters();
            var list = new List<object>();
            foreach (var pi in pis)
            {
                var o = container.Resolve(pi.ParameterType);
                list.Add(o);
            }
            cache = ctor.Invoke(list.ToArray());
            return cache;
        }
    }
}
