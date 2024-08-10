using Cr7Sund.Utility;
namespace Cr7Sund.IocContainer
{
    public abstract class Context : IContext
    {
        protected List<IContext> _contexts;

        public virtual IInjectionBinder InjectionBinder { get; private set; }


        public Context()
        {
            Init();
        }
        
        public virtual void AddContext(IContext context)
        {
            AssertUtil.IsFalse(_contexts.Contains(context));
            _contexts.Add(context);
        }
        public virtual void RemoveContext(IContext context)
        {
            if (_contexts.Contains(context))
            {
                _contexts.Remove(context);
            }
        }

        public virtual void Dispose()
        {
            // AssertUtil.LessOrEqual(_contexts.Count, 0);

            _contexts = null;
            InjectionBinder.Dispose();
            InjectionBinder = null;
        }

        #region  Injector Adapter

        public void BindInstance<T>(object value)
        {
            InjectionBinder.Bind<T>().To(value);
        }
        public void BindInstance<T>(object value, string name)
        {
            InjectionBinder.Bind<T>().To(value).ToName(name);
        }
        public void BindInstanceAsCrossContext<TKey>(object value)
        {
            InjectionBinder.Bind<TKey>().To(value).AsCrossContext();
        }
        public void BindInstanceAsCrossContext<TKey>(object value, object name)
        {
            InjectionBinder.Bind<TKey>().To(value).AsCrossContext().ToName(name);
        }
        public void BindSingleton<TKey, TValue>()
        {
            InjectionBinder.Bind<TKey>().To<TValue>().AsSingleton();
        }
        public void BindSingleton<TKey, TValue>(object name)
        {
            InjectionBinder.Bind<TKey>().To<TValue>().AsSingleton().ToName(name);
        }

        public void BindCrossContextAndSingleton<TKey, TValue>()
        {
            InjectionBinder.Bind<TKey>().To<TValue>().AsCrossContext().AsSingleton();
        }
        public void BindCrossContextAndSingleton<TKey, TValue>(object name)
        {
            InjectionBinder.Bind<TKey>().To<TValue>().ToName(name).AsCrossContext().AsSingleton();
        }

        public void Unbind<T>(object name = null)
        {
            InjectionBinder.Unbind<T>(name);
        }

        public object Inject(object target)
        {
            return InjectionBinder.Injector.Inject(target);
        }

        public void Deject(object target)
        {
            InjectionBinder.Injector.Deject(target);
        }

        public T GetInstance<T>()
        {
            return InjectionBinder.GetInstance<T>();
        }

        public object GetInstance(Type key)
        {
            return InjectionBinder.GetInstance(key);
        }
        
        protected virtual void Init()
        {
            _contexts = new List<IContext>();
            InjectionBinder = new InjectionBinder();
        }

        #endregion
    }
}
