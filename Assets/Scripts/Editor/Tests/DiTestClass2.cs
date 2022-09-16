namespace Au.DI
{
    [Injectable]
    internal class DiTestClass2
    {
        public DiTestClass2(DiTestClass test1)
        {
            this.test1 = test1;
        }

        public readonly DiTestClass test1;

    }
}
