using System;
namespace Cr7Sund.IocContainer
{

    public enum BinderExceptionType
    {
        // bind different binding with same key and same name, and is not weak too
        CONFLICT_IN_BINDER,
        // binding has reached max limit
        BINDING_LIMIT
    }

}