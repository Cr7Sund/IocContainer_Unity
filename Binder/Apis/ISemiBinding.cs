using Cr7Sund.Utility;
namespace Cr7Sund.IocContainer
{

    public interface ISemiBinding : IManagedList
    {
        /// <summary>  Set or get the constraint.  </summary>
        BindingConstraintType Constraint { get; set; }

        /// <summary>
        ///     A secondary constraint that ensures that this SemiBinding will never contain multiple values equivalent to
        ///     each other.
        /// </summary>
        bool UniqueValue { get; set; }
        PoolInflationType InflationType { get; set; }
        object SingleValue { get; }

        object this[int index] { get; set; }


        object[] Clone();
    }

}