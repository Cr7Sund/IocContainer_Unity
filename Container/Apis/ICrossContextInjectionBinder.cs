namespace Cr7Sund.IocContainer
{

    public interface ICrossContextInjectionBinder : IInjectionBinder
    {
        /// <summary>
        ///     Cross-context Injection Binder is shared across all child contexts
        /// </summary>
        IInjectionBinder CrossContextBinder { get; set; }
    }
    
}