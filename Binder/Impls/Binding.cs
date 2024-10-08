using System;
namespace Cr7Sund.IocContainer
{

    public class Binding : IBinding
    {
        protected bool _isWeak;
        protected Type _key;
        protected object _name;
        protected ISemiBinding _value;
        public Binder.BindingResolver resolver;

        public Binding(Binder.BindingResolver resolver)
        {
            this.resolver = resolver;

            _value = new SemiBinding();

            ValueConstraint = BindingConstraintType.ONE;

            Unique = true;
        }

        // only for unit-test binding
        public Binding() : this(null)
        {

        }


        #region IBinding implementation
        public BindingConstraintType ValueConstraint
        {
            get
            {
                return _value.Constraint;
            }
            set
            {
                _value.Constraint = value;
            }
        }

        public bool Unique
        {
            get
            {
                return _value.UniqueValue;
            }
            set
            {
                _value.UniqueValue = value;
            }
        }

        public Type Key
        {
            get
            {
                return _key;
            }
        }
        public ISemiBinding Value
        {
            get
            {
                return _value;
            }
        }

        public object Name
        {
            get
            {
                return _name == null ? BindingConst.NULLOIDNAME : _name;
            }
        }

        public bool IsWeak
        {
            get
            {
                return _isWeak;
            }
        }

        public IBinding Bind<T>()
        {
            return Bind(typeof(T));
        }

        public virtual IBinding Bind(Type key)
        {
            _key = key;

            return this;
        }

        public IBinding To<T>()
        {
            return To(typeof(T));
        }

        public IBinding To(object value)
        {
            // if (MacroDefine.IsRelease)
            // {
            //     if (value.GetType().IsValueType)
            //     {
            //         throw new MyException ($"{value.GetType()} is not referenceType",BinderExceptionType.EXIST_BOXING);
            //     }
            // }

            _value.Add(value);
            if (resolver != null)
                resolver(this);
            return this;
        }

        public IBinding ToName(object name)
        {
            object toName = name == null ? BindingConst.NULLOIDNAME : name;
            object oldName = _name;

            _name = toName;
            if (resolver != null)
                resolver(this, oldName);
            return this;
        }


        public virtual void RemoveValue(object o)
        {
            _value.Remove(o);
        }


        public IBinding Weak()
        {
            _isWeak = true;
            return this;
        }

        public virtual void Dispose()
        {
            _value.Dispose();
        }
        #endregion
    }
    public class BindingConst
    {
        /// Null is an acceptable binding, but dictionaries choke on it, so we map null to this instead.
        // the reason why not use enum is boxing operation cost
        public const string NULLOIDNAME = "NULLOIDNAME";
    }

}