using Cr7Sund.Utility;
namespace Cr7Sund.IocContainer
{

    public interface IInjectionBinder : IBinder, IInstanceProvider
    {
        ///<summary> Get or set an Injector to use. By default, Injector instantiates it's own, but that can be overridden.</summary>
        IInjector Injector { get; set; }

        /// <summary>
        ///     Retrieve an Instance based on a key/name combo.
        ///     ex. `injectionBinder.Get<cISomeInterface>(SomeEnum.MY_ENUM);
        /// </summary>
        T GetInstance<T>(object name);

        /// <summary>
        ///     Retrieve an Instance based on a key/name combo.
        ///     ex. `injectionBinder.Get(typeof(ISomeInterface), SomeEnum.MY_ENUM);`
        /// </summary>
        object GetInstance(Type key, object name);

        /// <summary>
        ///     Release an Instance based on a instance/name combo.
        /// </summary>
        void ReleaseInstance(object instance, object name = null);

        /// <summary>
        ///     Reflect all the types in the list
        ///     Return the number of types in the list, which should be equal to the list length
        /// </summary>
        int ReflectAll();

        /// <summary>
        ///     Reflect all the types currently registered with InjectionBinder
        ///     Return the number of types reflected, which should be equal to the number
        ///     of concrete classes you've mapped.
        /// </summary>
        int Reflect(List<Type> list);


        new IInjectionBinding Bind<T>();
        new IInjectionBinding GetBinding<T>(object name = null);
        IInjectionBinding GetBinding(Type key);
        IInjectionBinding GetBinding(Type key, object name);
    }

}