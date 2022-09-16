using Au.DI.Providers;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Assertions;

namespace Au.DI
{
    public class Container
    {
        private static Container _root;

        public static Container root
        {
            get
            {
                if (_root == null)
                {
                    _root = new Container(null);
                }
                return _root;
            }
        }

        private Container(Container parent)
        {
            _parent = parent;
        }

        private Container _parent;
        private Dictionary<string, Provider> _providers = new Dictionary<string, Provider>();

        public Container parent => _parent;

        public void Reset()
        {
            _providers.Clear();
        }

        public Container CreateChild()
        {
            return new Container(this);
        }

        public bool HasToken(string token)
        {
            return _providers.ContainsKey(token);
        }

        public object Resolve(Type type)
        {
            return Resolve(type.FullName, type);
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public T Resolve<T>(int token)
        {
            return (T)Resolve(token.ToString(), typeof(T));
        }

        public T Resolve<T>(string token)
        {
            return (T)Resolve(token.ToString(), typeof(T));
        }

        public object Resolve(string token, Type type)
        {
            // search current container
            if (_providers.TryGetValue(token, out var provider))
            {
                return provider.GetValue(this);
            }

            //search parent contaner
            var p = parent;
            while (p != null)
            {
                if (p.HasToken(token))
                {
                    var r = p.Resolve(token, type);
                    Assert.IsNotNull(r);
                    return r;
                }
                p = p.parent;
            }

            // no existed instance found, create new one

            // if singleton
            var singleton = type.GetCustomAttribute<SingletonAttribute>();
            if (singleton != null)
            {
                root.Register(type);
                return root.Resolve(type);
            }

            // if injectable
            var injectable = type.GetCustomAttribute<InjectableAttribute>();
            if (injectable != null)
            {
                if (injectable.lifecycle == Lifecycle.Transient)
                {
                    var prov = Provider.UseType(type);
                    return prov.GetValue(this);
                }
                else if (injectable.lifecycle == Lifecycle.Singleton)
                {
                    root.Register(type);
                    return root.Resolve(type);
                }
                else
                {
                    Register(type);
                    return Resolve(type);
                }
            }

            throw new Exception($"{token} ({type}) is not injectable");
        }


        public void Register(string token, Provider provider)
        {
            Assert.IsFalse(_providers.ContainsKey(token));
            _providers.Add(token, provider);
        }

        public void Register(int token, Provider provider)
        {
            Register(token.ToString(), provider);
        }

        public void Register(Type type, Type useType = null)
        {
            Register(type.FullName, Provider.UseType(useType ?? type));
        }

        public void Register<T>()
        {
            Register(typeof(T));
        }

        public void Register<T, C>() where C : T
        {
            Register(typeof(T), typeof(C));
        }
    }
}
