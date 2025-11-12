using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PinkCake
{
    public class CompositionRoot
    {
        private Dictionary<Type, List<object>> _Implementations = new Dictionary<Type, List<object>>();
        private Dictionary<Type, object> _Builders = new Dictionary<Type, object>();

        public CompositionRoot Register<T, TImplementation>() 
            where TImplementation : T
        {
            List<object> implementations = null;
            List<object> factoryImplementations = null;
            Func<object> implementation = null;

            if (!_Implementations.TryGetValue(typeof(T), out implementations))
            {
                implementations = new List<object>();
                _Implementations[typeof(T)] = implementations;
            }

            if (!_Implementations.TryGetValue(typeof(Func<T>), out factoryImplementations))
            {
                factoryImplementations = new List<object>();
                _Implementations[typeof(Func<T>)] = factoryImplementations;
            }

            if (typeof(TImplementation).IsInterface || typeof(TImplementation).IsAbstract)
            {
                if (typeof(T) == typeof(TImplementation))
                {
                    throw new Exception(typeof(TImplementation).Name + " is not a concrete type and so cannot be identical to " + typeof(T).Name);
                }

                implementation = new Func<object>(() => (object)Resolve<TImplementation>());
            }
            else
            {
                implementation = new Func<object>(() => (object)Build<TImplementation>());
            }

            implementations.Add(implementation);
            factoryImplementations.Add(new Func<object>(() => implementation));

            return this;
        }

        public T Resolve<T>()
        {
            if(typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                Expression<Func<IEnumerable<object>>> template = () => ResolveAll<object>();
                var items = (template.Body as MethodCallExpression).Method.GetGenericMethodDefinition().MakeGenericMethod(typeof(T).GetGenericTypeDefinition().GenericTypeArguments[0]).Invoke(null, Array.Empty<object>());

                return (T)items;
            }

            var implementations = _Implementations[typeof(T)];
            return (T)((Func<object>)implementations[0])();
        }

        private IEnumerable<T> ResolveAll<T>()
        {
            return _Implementations[typeof(T)]
                .Select(i => ((Func<object>)i)())
                .Cast<T>()
                .ToList();
        }

        public T Build<T>()
        {
            object builder = null;

            if(!_Builders.TryGetValue(typeof(T), out builder))
            {
                var constructors = typeof(T).GetConstructors();

                if(constructors.Length != 1)
                {
                    throw new Exception("Cannot find single constructor for " + typeof(T).Name);
                }

                var instanceParam = Expression.Parameter(typeof(CompositionRoot));
                var constructor = constructors[0];
                var paramBuilders = new List<object>();
                Expression<Func<CompositionRoot, object>> exampleMethod = cr =>  cr.Resolve<object>();
                var templateMethod = (exampleMethod.Body as MethodCallExpression).Method.GetGenericMethodDefinition();

                foreach(var constructorParam in constructor.GetParameters())
                {
                    var paramBuild = Expression.Lambda<Func<object>>(Expression.Convert(
                        Expression.Call(
                            Expression.Constant(this), templateMethod.MakeGenericMethod(constructorParam.ParameterType)),
                        typeof(object)))
                        .Compile();
                    paramBuilders.Add(paramBuild);
                }

                builder = new Func<object>(() =>
                {
                    var constParams = paramBuilders
                        .Cast<Func<object>>()
                        .Select(pb => pb())
                        .ToArray();

                    return constructor.Invoke(constParams);
                });

                _Builders.Add(typeof(T), builder);
            }

            return (T)((Func<object>)(builder))();
        }
    }
}
