namespace Cr7Sund.IocContainer
{

    public interface IBinding : IDisposable
    {
        ///<summary> Get the binding's key </summary>
        Type Key { get; }
        object Name { get; }
        ISemiBinding Value { get; }


        /// <summary> Get whether the binding is weak, default false </summary>
        bool IsWeak { get; }
        /// <summary>
        ///     Tie this binding to a Type key
        /// </summary>
        IBinding Bind<T>();
        /// <summary>
        ///     Tie this binding to a key, such as object, string, enum
        /// </summary>
        IBinding Bind(Type type);
        /// <summary>
        ///     Set the Binding's value to a value, such as a string or class instance
        /// </summary>
        IBinding To(object value);
        /// <summary>
        ///     Set the Binding's value to a Type
        /// </summary>
        IBinding To<T>();
        /// <summary>
        ///     Qualify a binding using a value, such as a string or enum
        ///     should be call first otherwise maybe cause an conflict BinderException
        /// </summary>
        IBinding ToName(object name);

        //<summary> Remove a specific value from the binding </summary>
        void RemoveValue(object o);

        /// <summary> Mark a binding as weak, so that any new binding will override it </summary>
        IBinding Weak();
    }

}