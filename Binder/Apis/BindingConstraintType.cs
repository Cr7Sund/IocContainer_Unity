namespace Cr7Sund.IocContainer
{
    public enum BindingConstraintType
    {
        /// Constrains a SemiBinding to carry no more than one item in its Value
        ONE = 0,
        /// Constrains a SemiBinding to carry a list of items in its Value
        MANY = 1,
        /// Instructs the Binding to apply a Pool instead of a SemiBinding
        POOL = 2
    }
}