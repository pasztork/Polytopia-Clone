using System;

namespace Util
{
    public class Factory<TBaseClass>
    {
        public TBaseClass Create<TDerivedClass>() where TDerivedClass : TBaseClass
        {
            return (TBaseClass)Activator.CreateInstance(typeof(TDerivedClass));
        }
    }
}