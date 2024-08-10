using Cr7Sund.Utility;
using Cr7Sund.IocContainer;
namespace Cr7Sund.IocContainer
{

    public class InjectionBinding : Binding, IInjectionBinding
    {
        public InjectionBinding(Binder.BindingResolver resolver):base(resolver)
        {
            
        }


        private void ValidBindingType(Type objType)
        {
            var keyType = Key;
            if (!keyType.IsAssignableFrom(objType))
            {
                throw new Exception($"{InjectionExceptionType.ILLEGAL_BINDING_VALUE}: Injection cannot bind a type ({objType}) that does not extend or implement the binding type ({keyType}).");
            }
        }


        #region IInjectionBinding implementation
        public bool IsCrossContext
        {
            get;
            private set;
        }

        public bool IsToInject
        {
            get;
            private set;
        } = true;

        public InjectionBindingType Type
        {
            get;
            set;
        } = InjectionBindingType.DEFAULT;

        public IInjectionBinding AsDefault()
        {
            //If already a value, this mapping is redundant
            if (Type == InjectionBindingType.VALUE)
            {
                return this;
            }

            Type = InjectionBindingType.DEFAULT;
            if (resolver != null)
            {
                resolver(this);
            }
            return this;
        }

        public IInjectionBinding AsPool()
        {
            //If already a value, this mapping is redundant
            if (Type == InjectionBindingType.VALUE)
            {
                return this;
            }

            Type = InjectionBindingType.POOL;
            if (resolver != null)
            {
                resolver(this);
            }
            return this;
        }

        public IInjectionBinding AsSingleton()
        {
            //If already a value, this mapping is redundant
            if (Type == InjectionBindingType.VALUE)
            {
                return this;
            }

            Type = InjectionBindingType.SINGLETON;
            if (resolver != null)
            {
                resolver(this);
            }

            return this;
        }

        public IInjectionBinding AsCrossContext()
        {
            IsCrossContext = true;
            if (resolver != null)
            {
                resolver(this);
            }
            return this;
        }

        public IInjectionBinding ToInject(bool value)
        {
            IsToInject = value;
            return this;
        }
        #endregion

        #region IBinding implementation
        public new IInjectionBinding Bind<T>()
        {
            return base.Bind<T>() as IInjectionBinding;
        }


        public new IInjectionBinding To(object o)
        {
            Type = InjectionBindingType.VALUE;
            SetValue(o);
            return this;
        }

        public IInjectionBinding SetValue(object o)
        {
            var objType = o.GetType();
            ValidBindingType(objType);

            base.To(o);
            return this;
        }

        public new IInjectionBinding To<T>()
        {
            ValidBindingType(typeof(T));
            return base.To<T>() as IInjectionBinding;
        }

        public IInjectionBinding To(Type type)
        {
            ValidBindingType(type);
            return base.To(type) as IInjectionBinding;
        }

        public new IInjectionBinding ToName(object name)
        {
            return base.ToName(name) as IInjectionBinding;
        }

        public new IInjectionBinding Weak()
        {
            return base.Weak() as IInjectionBinding;
        }
        #endregion
    }
}
