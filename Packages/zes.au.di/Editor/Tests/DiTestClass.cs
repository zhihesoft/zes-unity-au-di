namespace Au.DI
{
    [Injectable()]
    internal class DiTestClass : DiTestInterface
    {

        public DiTestClass()
        {
            DiTest.count++;
        }

        public int count => DiTest.count;
    }
}
