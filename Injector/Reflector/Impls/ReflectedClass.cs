using System;
using System.Reflection;
using Cr7Sund.IocContainer;
namespace Cr7Sund.IocContainer
{

    public class ReflectedClass : IReflectedClass
    {
        public bool PreGenerated { get; set; }
        public ConstructorInfo Constructor { get; set; }
        public int ConstructorParameterCount { get; set; }
        public MethodInfo PostConstructor { get; set; }
        public Tuple<Type, object, FieldInfo>[] Fields { get; set; }
    }
}
