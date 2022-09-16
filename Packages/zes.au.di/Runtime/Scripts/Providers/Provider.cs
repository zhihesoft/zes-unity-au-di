using System;

namespace Au.DI.Providers
{
    public abstract class Provider
    {
        public static Provider UseType(Type type)
        {
            return new TypeProvider(type);
        }

        public static Provider UseType<T>()
        {
            return new TypeProvider(typeof(T));
        }

        public static Provider UseValue<T>(T value)
        {
            return new ValueProvider(value);
        }

        public static Provider Factory<T>(Func<Container, T> factory)
        {
            return new FactoryProvider<T>(factory);
        }

        public object GetValue(Container container)
        {
            return OnGetValue(container);
        }

        protected abstract object OnGetValue(Container container);
    }
}
