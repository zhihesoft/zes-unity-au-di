using System;

namespace Au.DI.Providers
{
    internal class FactoryProvider<T> : Provider
    {
        public FactoryProvider(Func<Container, T> factory)
        {
            this.factory = factory;
        }

        private readonly Func<Container, T> factory;

        protected override object GetValue(Container container)
        {
            return factory(container);
        }
    }
}
