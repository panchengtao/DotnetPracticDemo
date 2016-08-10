using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DependencyInjection.Extensions
{
    public class Injection
    {
        private readonly ConcurrentDictionary<Type, Type> _typeMapping = new ConcurrentDictionary<Type, Type>();

        public virtual void Register<TFrom, TTo>()
        {
            Register(typeof(TFrom), typeof(TTo));
        }

        private void Register(Type from, Type to)
        {
            _typeMapping[from] = to;
        }

        public object GetService<T>()
        {
            return GetService(typeof(T));
        }

        private object GetService(Type serviceType)
        {
            Type type;
            if (!_typeMapping.TryGetValue(serviceType, out type))
            {
                type = serviceType;
            }
            if (type.GetTypeInfo().IsInterface || type.GetTypeInfo().IsAbstract)
            {
                return null;
            }

            var constructor = GetConstructor(type);
            if (null == constructor)
            {
                return null;
            }

            var arguements = constructor.GetParameters().Select(p => GetService(p.ParameterType)).ToArray();
            var service = constructor.Invoke(arguements);
            InitializeInjectedProperties(service);
            InvokeInjectedMethods(service);
            return service;
        }

        protected virtual void InvokeInjectedMethods(object service)
        {
            service.GetType().GetMethods().Where(m => m.GetCustomAttribute<InjectionAttribute>() != null)
                .ToList()
                .ForEach(m =>
                {
                    var arguements = m.GetParameters().Select(p => GetService(p.ParameterType)).ToArray();
                    m.Invoke(service, arguements);

                });
        }

        protected virtual void InitializeInjectedProperties(object service)
        {
                service.GetType()
                    .GetProperties()
                    .Where(p => p.CanWrite && p.GetCustomAttribute<InjectionAttribute>() != null)
                    .ToList().ForEach(p =>
                    {
                        p.SetValue(service, GetService(p.PropertyType));
                    });
        }

        protected virtual ConstructorInfo GetConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            return constructors.FirstOrDefault(c => c.GetCustomAttribute<InjectionAttribute>() != null) ??
                   constructors.FirstOrDefault();
        }

    }
}
