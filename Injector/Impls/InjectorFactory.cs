using Cr7Sund.FrameWork.Util;

using System;
namespace Cr7Sund.IocContainer
{

    public class InjectorFactory : IInjectorFactory
    {
        public object Get(IInjectionBinding binding)
        {
            return Get(binding, null);
        }

        public object Get(IInjectionBinding binding, object[] args)
        {
            var type = binding.Type;

            switch (type)
            {
                case InjectionBindingType.SINGLETON:
                    return SingletonOf(binding, args);
                case InjectionBindingType.VALUE:
                    return ValueOf(binding);
            }

            return InstanceOf(binding, args);
        }

        private object InstanceOf(IInjectionBinding binding, object[] args)
        {
            return CreateFromValue(binding.Value.SingleValue, args);
        }

        private object ValueOf(IInjectionBinding binding)
        {
            object bindingValue = binding.Value.SingleValue;

            if (bindingValue.GetType().IsInstanceOfType(typeof(Type)))
                throw new Exception($"{InjectionExceptionType.TYPE_AS_VALUE_INJECTION}: Inject a type into binder as value");

            return bindingValue;
        }

        private object SingletonOf(IInjectionBinding binding, object[] args)
        {
            object bindingValue = binding.Value.SingleValue;

            AssertUtil.NotNull(bindingValue, InjectionExceptionType.EXISTED_VALUE_INJECTION);

            if (bindingValue.GetType().IsInstanceOfType(typeof(Type)))
            {
                object o = CreateFromValue(bindingValue, args);
                if (o == null)
                {
                    return null;
                }

                ((InjectionBinding)binding).SetValue(o);
            }

            return binding.Value.SingleValue;
        }

        /// Call the Activator to attempt instantiation the given object
        private object CreateFromValue(object o, object[] args)
        {
            var value = o is Type ? o as Type : o.GetType();

            object retVal;
            {
                if (args == null || args.Length == 0)
                {
                    retVal = Activator.CreateInstance(value);
                }
                else
                {
                    retVal = Activator.CreateInstance(value, args);
                }


                return retVal;
            }
        }
    }
}
