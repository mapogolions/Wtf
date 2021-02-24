using System;
using System.Diagnostics;
using System.Linq;

namespace Multifaceted
{
    // origin: https://github.com/dotnet/runtime/blob/master/src/libraries/Microsoft.Extensions.DependencyInjection.Abstractions/src/ServiceDescriptor.cs
    public class Descriptor
    {
        public Type ServiceType { get; }
        public Type ImplementationType { get; }
        public object Instance { get; }
        public Func<IServiceProvider, object> Factory { get; }

        public Type GetImplementationType()
        {
            if (ImplementationType is { }) return ImplementationType;
            if (Instance is { }) return Instance.GetType();
            if (Factory is { })
            {
                var genericArgs = Factory.GetType().GetGenericArguments();
                Debug.Assert(genericArgs.Count() == 2);
                return genericArgs[1];
            }

            throw new InvalidOperationException("Implementation type missed");
        }
    }

    namespace RightWay
    {
        public interface IDescriptor
        {
            Type ServiceType { get; }
            Type GetImplementationType();
        }

        public class FactoryDescriptor : IDescriptor
        {
            private readonly Lazy<Type> _implementationType;

            public FactoryDescriptor(Type serviceType, Func<IServiceProvider, object> factory)
            {
                ServiceType = serviceType;
                Factory = factory;
                _implementationType = new(() =>
                {
                    var genericArgs = Factory.GetType().GetGenericArguments();
                    Debug.Assert(genericArgs.Count() == 2);
                    return genericArgs[1];
                });
            }

            public Type ServiceType { get; }
            public Func<IServiceProvider, object> Factory { get; }

            public Type GetImplementationType() => _implementationType.Value;
        }


        public class InstanceDescriptor : IDescriptor
        {
            public InstanceDescriptor(Type serviceType, object instance)
            {
                ServiceType = serviceType;
                Instance = instance;
            }

            public Type ServiceType { get; }
            public object Instance { get; }

            public Type GetImplementationType() => Instance.GetType();
        }


        public class ImplementationTypeDecriptor : IDescriptor
        {
            public ImplementationTypeDecriptor(Type serviceType, Type implementationType)
            {
                ServiceType = serviceType;
                ImplementationType = implementationType;
            }

            public Type ServiceType { get; }
            public Type ImplementationType { get; }

            public Type GetImplementationType() => ImplementationType;
        }
    }
}
