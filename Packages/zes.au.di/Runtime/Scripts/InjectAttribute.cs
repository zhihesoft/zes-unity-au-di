using System;

namespace Au.DI
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
        public InjectAttribute(string token)
        {
            this.token = token;
        }

        public InjectAttribute(int token)
        {
            this.token = token.ToString();
        }

        public readonly string token;
    }
}
