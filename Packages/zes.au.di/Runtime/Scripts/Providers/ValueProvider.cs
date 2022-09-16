namespace Au.DI.Providers
{
    internal class ValueProvider : Provider
    {
        public ValueProvider(object value)
        {
            this.value = value;
        }

        private object value;

        protected override object OnGetValue(Container container)
        {
            return value;
        }
    }
}
