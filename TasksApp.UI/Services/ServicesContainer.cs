using System;
using System.Collections.Generic;

namespace TasksApp.UI.Services
{
    public class ServicesContainer
    {
        private Dictionary<Type, Func<object>> _serviceMap = new Dictionary<Type, Func<object>>();

        public void Bind<TInterface, TImplementation>(params object[] constructorArgs) where TImplementation : TInterface
        {
            Type interfaceType = typeof(TInterface);
            Type implementationType = typeof(TImplementation);

            _serviceMap[interfaceType] = () => Activator.CreateInstance(implementationType, constructorArgs);
        }

        public TInterface Get<TInterface>()
        {
            Type interfaceType = typeof(TInterface);

            if (!_serviceMap.ContainsKey(interfaceType))
            {
                throw new InvalidOperationException($"Service of type {interfaceType.Name} is not registered.");
            }

            Func<object> createInstance = _serviceMap[interfaceType];
            return (TInterface)createInstance.Invoke();
        }
    }
}
