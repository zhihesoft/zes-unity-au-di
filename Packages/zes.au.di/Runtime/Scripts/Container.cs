using System;
using System.Collections.Generic;

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
        private Dictionary<Type, InjectDef> defs = new Dictionary<Type, InjectDef>();

        public Container parent
        {
            get { return _parent; }
        }

        public Container CreateChild()
        {
            return new Container(this);
        }

        public T Resolve<T>()
        {
            return default(T);
        }
    }
}
