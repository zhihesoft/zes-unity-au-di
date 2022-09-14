using System;

namespace Au.DI
{
    public class InjectableAttribute : Attribute
    {
        public InjectableAttribute() { }

        public InjectableAttribute(Lifecycle lifecycle)
        {
            this.lifecycle = lifecycle;
        }

        public readonly Lifecycle lifecycle = Lifecycle.Transient;
    }
}
