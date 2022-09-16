namespace Au.DI
{
    [Singleton]
    internal class DiSingletonClass
    {
        public DiSingletonClass()
        {
            _count++;
        }

        static int _count = 0;

        public int count => _count;
    }
}
