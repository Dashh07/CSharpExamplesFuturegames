using NUnit.Framework;
using System;
using System.Collections.Generic;

public interface IService {
    // Executed when the service is registered.
    public void Initialize();
    public void Dispose();
}

/// <summary>
/// Generic Service Locator which is a singleton.
/// </summary>
internal sealed class ServiceLocator : Singleton<ServiceLocator> {
    private ServiceLocator() { }

    private readonly Dictionary<Type, object> registry = new();

    public void RegisterService<T>(T serviceInstance) where T : IService {
        registry[typeof(T)] = serviceInstance;
        serviceInstance.Initialize();
    }

    public T GetService<T>() where T : IService {
        T serviceInstance = (T)registry[typeof(T)];
        return serviceInstance;
    }

    public void RemoveService<T>() where T : IService {
        T serviceInstance = (T)registry[typeof(T)];
        serviceInstance.Dispose();
        registry.Remove(typeof(T));
    }
}
