/**
 * @interfaceCr7Sund.Framework.Api.IInjectorFactory
 *
 * Interface for the Factory that instantiates all instances.
 *
 * @seeCr7Sund.Framework.Api.IInjector
 */

using System;
namespace Cr7Sund.IocContainer
{

    public interface IInjectorFactory
    {
        /// <summary>  Request instantiation based on the provided binding </summary>
        object Get(IInjectionBinding binding);

        /// <summary>  Request instantiation based on the provided binding and an array of Constructor arguments </summary>
        object Get(IInjectionBinding binding, object[] args);
    }
}
