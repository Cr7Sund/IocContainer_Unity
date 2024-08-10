namespace Cr7Sund.IocContainer
{

    public interface IManagedList : IDisposable
    {
        // Length of values
        int Count { get; }
        /// Add a value to this List.
        IManagedList Add(object value);

        /// Add a set of values to this List.
        IManagedList Add(object[] list);

        /// Remove a value from this List.
        IManagedList Remove(object value);

        /// Remove a set of values from this List.
        IManagedList Remove(object[] list);

        bool Contains(object o);
    }

}