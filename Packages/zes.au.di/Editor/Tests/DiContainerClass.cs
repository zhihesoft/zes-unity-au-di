namespace Au.DI
{
    [Injectable(Lifecycle.ContainerScoped)]
    internal class DiContainerClass
    {
        public DiContainerClass()
        {
            DiTest.count++;
        }


        public int count => DiTest.count;
    }
}
