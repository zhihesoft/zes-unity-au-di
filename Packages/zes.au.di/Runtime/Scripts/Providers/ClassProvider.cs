using System;
using UnityEngine.Assertions;

namespace Au.DI.Providers
{
    internal class ClassProvider<T, C> : Provider where C : T
    {
        public ClassProvider()
        {
            typeProvider = new TypeProvider(typeof(C));
        }

        TypeProvider typeProvider;
        C cache;

        protected override object GetValue(Container container)
        {
            if (cache != null)
            {
                return cache;
            }

            var o = typeProvider.GetObject(container);
            Assert.IsNotNull(o);
            cache = (C)o;
            return cache;
        }
    }
}
