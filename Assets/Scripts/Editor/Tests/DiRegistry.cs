using Au.DI;
using NUnit.Framework;
using UnityEngine.TestTools;

[Singleton]
[Registry(typeof(DiTest))]
[Registry(typeof(DiTestInterface), typeof(DiTestClass))]
public class DiRegistry : IPrebuildSetup, IPostBuildCleanup
{
    [Test]
    public void TestRegistry()
    {
        var reg = Container.root.Resolve<DiRegistry>();
        var inf = Container.root.Resolve<DiTestInterface>();
        var tst = Container.root.Resolve<DiTest>();

        Assert.IsNotNull(reg);
        Assert.IsNotNull(tst);
        Assert.IsTrue(inf.GetType() == typeof(DiTestClass));
    }

    public void Cleanup()
    {
        Container.root.Reset();
    }

    public void Setup()
    {
        Container.root.Reset();
    }
}
