namespace Au.DI.Providers
{
    internal class ValueProvider : Provider
    {
        public ValueProvider(object value)
        {
            this.value = value;
        }

        private object value;

        protected override object GetValue(Container container)
        {
            return value;
        }
    }
}
