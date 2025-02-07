using System;
using System.Collections.Generic;
using System.Reflection;


// DI Container Core
public class DiContainer
{
    private readonly Dictionary<Type, object> _singletons = new();
    private readonly Dictionary<Type, Type> _typeMappings = new();
    private readonly Dictionary<Type, Func<object>> _factories = new();
    private readonly Dictionary<Type, List<Action<object>>> _pendingInjections = new();

    // Registration methods
    public void RegisterSingleton<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _typeMappings[typeof(TInterface)] = typeof(TImplementation);
    }

    public void RegisterSingleton<T>(T instance)
    {
        _singletons[typeof(T)] = instance;
        TryResolvePendingInjections(typeof(T), instance);
    }

    public void RegisterFactory<T>(Func<T> factory)
    {
        _factories[typeof(T)] = () => factory();
    }

    // Resolution methods
    public T Resolve<T>()
    {
        var type = typeof(T);
        return (T)Resolve(type);
    }

    private object Resolve(Type type)
    {
        if (_singletons.TryGetValue(type, out var singleton))
            return singleton;

        if (_typeMappings.TryGetValue(type, out var implementationType))
            return CreateInstance(implementationType);

        if (_factories.TryGetValue(type, out var factory))
            return factory();

        if (type.IsClass && !type.IsAbstract)
            return CreateInstance(type);

        throw new DiException($"No registration found for type {type.Name}");
    }

    // Injection system
    public void InjectDependencies(object target)
    {
        var type = target.GetType();
        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            if (field.GetCustomAttribute<InjectAttribute>() != null)
            {
                var dependency = Resolve(field.FieldType);
                field.SetValue(target, dependency);
            }
        }
    }

    private object CreateInstance(Type type)
    {
        var instance = Activator.CreateInstance(type);
        InjectDependencies(instance);
        return instance;
    }

    private void TryResolvePendingInjections(Type type, object instance)
    {
        if (_pendingInjections.TryGetValue(type, out var callbacks))
        {
            foreach (var callback in callbacks)
            {
                callback(instance);
            }
            _pendingInjections.Remove(type);
        }
    }
}



