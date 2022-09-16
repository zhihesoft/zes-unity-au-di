using Au.DI.Providers;
using System;

namespace Au.DI
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegistryAttribute : Attribute
    {
        public RegistryAttribute(Type type)
        {
            this.type = type;
            this.useType = null;
        }

        public RegistryAttribute(Type type, Type useType)
        {
            this.type = type;
            this.useType = useType;
        }

        public readonly Type type;
        public readonly Type useType;
    }
}
