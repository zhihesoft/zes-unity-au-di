namespace Au.DI
{
    [Injectable]
    internal class DiDefaultClass
    {
        public DiDefaultClass()
        {
            DiTest.count++;
        }


        public int count => DiTest.count;
    }
}
