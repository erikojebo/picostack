using System;
using System.Collections.Generic;
using System.Linq;

namespace PicoStack.Core.DependencyInjection
{
    public class Registry
    {
        private readonly Dictionary<Type, Type> _registry = new Dictionary<Type, Type>();

        public void Register<TAbstract, TConcrete>()
            where TConcrete : TAbstract
        {
            _registry[typeof(TAbstract)] = typeof(TConcrete);
        }

        public object Resolve(Type type)
        {
            var typeToCreate = type;

            if (_registry.ContainsKey(type))
                typeToCreate = _registry[type];

            var constructorWithMostParameters = typeToCreate
                .GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length)
                .First();

            var constructorParameters = constructorWithMostParameters
                .GetParameters()
                .Select(x => Resolve(x.ParameterType));

            return constructorWithMostParameters.Invoke(constructorParameters.ToArray());
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
    }
}