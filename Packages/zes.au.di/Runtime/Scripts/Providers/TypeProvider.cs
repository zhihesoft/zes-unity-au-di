using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        protected override object OnGetValue(Container container)
        {
            if (cache != null)
            {
                return cache;
            }

            // check registry
            type.GetCustomAttributes<RegistryAttribute>()?.ToList().ForEach(i => container.Register(i.type, i.useType));

            var ctor = type.GetConstructors()[0];
            var pis = ctor.GetParameters();
            var list = new List<object>();
            foreach (var pi in pis)
            {
                var inj = pi.GetCustomAttribute<InjectAttribute>();
                if (inj != null)
                {
                    var o = container.Resolve(inj.token, pi.ParameterType);
                    list.Add(o);
                }
                else
                {
                    var o = container.Resolve(pi.ParameterType);
                    list.Add(o);
                }
            }
            cache = ctor.Invoke(list.ToArray());
            return cache;
        }
    }
}
