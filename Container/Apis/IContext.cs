namespace Cr7Sund.IocContainer
{
    /// <summary>
    /// Represents a context for managing instances and dependencies.
    /// </summary>
    public interface IContext : IDisposable, IInstanceProvider
    {
        /// <summary>
        /// Registers a new context to this one.
        /// </summary>
        /// <param name="context">The context to add.</param>
        void AddContext(IContext context);

        /// <summary>
        /// Removes a context from this one.
        /// </summary>
        /// <param name="context">The context to remove.</param>
        void RemoveContext(IContext context);


        /// <summary>
        /// Binds an instance to a specific type.
        /// </summary>
        /// <typeparam name="T">The type to bind the instance to.</typeparam>
        /// <param name="value">The instance to bind.</param>
        void BindInstance<T>(object value);

        /// <summary>
        /// Binds an instance to a specific type with a given name.
        /// </summary>
        /// <typeparam name="T">The type to bind the instance to.</typeparam>
        /// <param name="value">The instance to bind.</param>
        /// <param name="name">The name to associate with the binding.</param>
        void BindInstance<T>(object value, string name);

        /// <summary>
        /// Binds an instance as a cross-context service.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to associate with the instance.</typeparam>
        /// <param name="value">The instance to bind.</param>
        void BindInstanceAsCrossContext<TKey>(object value);

        /// <summary>
        /// Binds an instance as a cross-context service with a given name.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to associate with the instance.</typeparam>
        /// <param name="value">The instance to bind.</param>
        /// <param name="name">The name to associate with the binding.</param>
        void BindInstanceAsCrossContext<TKey>(object value, object name);

        /// <summary>
        /// Binds a type as a singleton service.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to associate with the singleton.</typeparam>
        /// <typeparam name="TValue">The type of the singleton instance.</typeparam>
        void BindSingleton<TKey, TValue>();

        /// <summary>
        /// Binds a type as a singleton service with a given name.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to associate with the singleton.</typeparam>
        /// <typeparam name="TValue">The type of the singleton instance.</typeparam>
        /// <param name="name">The name to associate with the singleton.</param>
        void BindSingleton<TKey, TValue>(object name);

        /// <summary>
        /// Binds a type as both a cross-context and singleton service.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to associate with the service.</typeparam>
        /// <typeparam name="TValue">The type of the service instance.</typeparam>
        void BindCrossContextAndSingleton<TKey, TValue>();

        /// <summary>
        /// Binds a type as both a cross-context and singleton service with a given name.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to associate with the service.</typeparam>
        /// <typeparam name="TValue">The type of the service instance.</typeparam>
        /// <param name="name">The name to associate with the service.</param>
        void BindCrossContextAndSingleton<TKey, TValue>(object name);

        /// <summary>
        /// Unbinds a type or named instance from the context.
        /// </summary>
        /// <typeparam name="T">The type to unbind.</typeparam>
        /// <param name="name">The name associated with the binding to unbind. If <c>null</c>, unbinds all instances of the type.</param>
        void Unbind<T>(object name = null);

        /// <summary>
        /// Requests that the provided target be injected with dependencies.
        /// </summary>
        /// <param name="target">The target to inject dependencies into.</param>
        /// <returns>The target with injected dependencies.</returns>
        object Inject(object target);

        /// <summary>
        /// Clears the injections from the provided instance. Note that only public fields will be uninjected, not constructor injections.
        /// </summary>
        /// <param name="target">The instance to clear injections from.</param>
        void Deject(object target);
    }
}
