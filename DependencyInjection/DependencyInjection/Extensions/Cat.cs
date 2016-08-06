using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace DependencyInjection.Extensions
{
    public class Cat
    {
        private readonly ConcurrentDictionary<Type, Type> typeMapping = new ConcurrentDictionary<Type, Type>();

        public virtual void Register<TFrom, TTo>()
        {
            Register(typeof (TFrom), typeof (TTo));
        }

        private void Register(Type from, Type to)
        {
            typeMapping[from] = to;
        }

        public object GetService<T>()
        {
            return GetService(typeof (T));
        }

        private object GetService(Type serviceType)
        {
            Type type;
            if (!typeMapping.TryGetValue(serviceType, out type))
            {
                type = serviceType;
            }
            if (type.IsInterface || type.IsAbstract)
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
            var methods =
                service.GetType().GetMethods().Where(m => m.GetCustomAttribute<InjectionAttribute>() != null).ToArray();
            Array.ForEach(methods, m =>
            {
                var arguements = m.GetParameters().Select(p => GetService(p.ParameterType)).ToArray();
                m.Invoke(service, arguements);
            });
        }

        protected virtual void InitializeInjectedProperties(object service)
        {
            var properties =
                service.GetType()
                    .GetProperties()
                    .Where(p => p.CanWrite && p.GetCustomAttribute<InjectionAttribute>() != null)
                    .ToArray();
            Array.ForEach(properties, p => p.SetValue(service, GetService(p.PropertyType)));
        }

        protected virtual ConstructorInfo GetConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            return constructors.FirstOrDefault(c => c.GetCustomAttribute<InjectionAttribute>() != null) ??
                   constructors.FirstOrDefault();
        }
    }
}