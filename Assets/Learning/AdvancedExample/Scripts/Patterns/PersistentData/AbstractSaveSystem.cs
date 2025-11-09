using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

public interface ISaveSystem {
    void Save();
    void Load();
}

public class AbstractSaveSystem {

    public AbstractSaveSystem() {
        var savedTypes = SearchAllISaveSystem(typeof(ISaveSystem));
    }

    public void RegisterSaveSystem<T>(T saveSystem) {}
    public void UnregisterSaveSystem<T>() {}

    private IEnumerable<Type> SearchAllISaveSystem(Type targetInterface) {
        IEnumerable<Type> classes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => targetInterface.IsAssignableFrom(type) &&
                type.IsClass &&
                !type.IsAbstract);
        return classes;
    }
}
