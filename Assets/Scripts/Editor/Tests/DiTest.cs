using Au.DI;
using Au.DI.Providers;
using NUnit.Framework;
using UnityEngine.TestTools;

public class DiTest : IPrebuildSetup, IPostBuildCleanup
{
    public static int count = 0;

    [Test]
    public void SingletonResolve()
    {
        count = 0;
        var s = Container.root.Resolve<DiSingletonClass>();
    }

    [Test]
    public void InjectableResolve()
    {
        count = 0;

        Container.root.Reset();

        var s = Container.root.Resolve<DiDefaultClass>();
        Assert.IsTrue(s.GetType() == typeof(DiDefaultClass));
        var ss = Container.root.CreateChild().Resolve<DiDefaultClass>();
        Assert.AreNotEqual(s, ss);

        var child = Container.root.CreateChild();
        var childClass = child.Resolve<DiContainerClass>();
        var rootClass = Container.root.Resolve<DiContainerClass>();

        Assert.IsNotNull(childClass, "create instance in children");
        Assert.IsNotNull(rootClass, "create instance in root");
        Assert.AreNotEqual(childClass, rootClass);

        var grandChildClass = child.CreateChild().Resolve<DiContainerClass>();
        Assert.AreEqual(grandChildClass, childClass, "grand child can get resolved instance from parent");
    }

    [Test]
    public void Registers()
    {
        count = 0;
        var container = Container.root.CreateChild();
        container.Register("teststring", Provider.UseValue("abc"));
        Assert.AreEqual(container.Resolve<string>("teststring"), "abc");
        container.Register("testint", Provider.UseValue(1024));
        Assert.AreEqual(container.Resolve<int>("testint"), 1024);

        var child = container.CreateChild();
        Assert.AreEqual(child.Resolve<string>("teststring"), "abc");
        Assert.AreEqual(child.Resolve<int>("testint"), 1024);

        child.Register<DiTestInterface, DiTestClass>();
        var di = child.Resolve<DiTestInterface>();
        Assert.AreEqual(di.GetType(), typeof(DiTestClass));

        child.Register<DiTestClass>();
        Assert.AreEqual(child.Resolve<DiTestClass>().GetType(), typeof(DiTestClass));

        var t2 = child.Resolve<DiTestClass2>();
        Assert.AreEqual(t2.test1.GetType(), typeof(DiTestClass));

    }


    public void Setup()
    {
        Container.root.Reset();
        count = 0;
        Container.root.Register("test_string", Provider.UseValue("hello world"));
    }

    public void Cleanup()
    {
        Container.root.Reset();
    }
}

