using Cr7Sund.Utility;
namespace Cr7Sund.IocContainer
{
    public abstract class CrossContext : Context, ICrossContext
    {
        protected ICrossContextInjectionBinder _crossContextInjectionBinder;

        public override IInjectionBinder InjectionBinder
        {
            get
            {
                return _crossContextInjectionBinder;
            }
        }


        protected override void Init()
        {
            _contexts = new List<IContext>();
            _crossContextInjectionBinder = new CrossContextInjectionBinder();
        }

        public sealed override void AddContext(IContext context)
        {
            base.AddContext(context);
            if (context is ICrossContext crossContext)
            {
                AssignCrossContext(crossContext);
            }
        }
        public sealed override void RemoveContext(IContext context)
        {
            if (context is ICrossContext crossContext)
            {
                RemoveCrossContext(crossContext);
            }
            base.RemoveContext(context);
        }

        private void AssignCrossContext(ICrossContext childContext)
        {
            AssertUtil.NotNull(_crossContextInjectionBinder.CrossContextBinder, ContextExceptionType.EmptyCrossContext);

            if (childContext is Context context &&
                context.InjectionBinder is CrossContextInjectionBinder childContextBinder)
            {
                AssertUtil.IsNull(childContextBinder.CrossContextBinder, ContextExceptionType.DuplicateCrossContext);

                childContextBinder.CrossContextBinder = new CrossContextInjectionBinder();
                childContextBinder.CrossContextBinder.CopyFrom(_crossContextInjectionBinder.CrossContextBinder);
            }
        }

        private void RemoveCrossContext(ICrossContext childContext)
        {
            if (childContext is Context context &&
                context.InjectionBinder is CrossContextInjectionBinder childContextBinder)
            {
                // since cross context is only from unique context;
                childContextBinder.CrossContextBinder.RemoveAll();
                childContextBinder.CrossContextBinder = null;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            _crossContextInjectionBinder.Dispose();
            _crossContextInjectionBinder = null;
        }
    }
}
