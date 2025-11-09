using System;
using UnityEngine;

public class ServicesManager : MonoBehaviour {
    public Player player;

    private void Start() {
        ServiceLocator.Instance.GetService<FirstService>().DoSomething();
        ServiceLocator.Instance.GetService<AnotherService>().DoSomething();

        player = ServiceLocator.Instance.GetService<Player>();
        player.DoSomething();
    }
}

// Some random monobehaviour which we want register as a service
public class AnotherService : MonoBehaviour, IService {
    private void Awake() {
        ServiceLocator.Instance.RegisterService(this);
    }
    public void Initialize() { }

    public void Dispose() {
        Debug.Log("AnotherService disposed");
    }

    public void DoSomething() {
        Debug.Log("AnotherService is doing something");
        ServiceLocator.Instance.GetService<FirstService>().DoSomething();
    }
}

// Some other random monobehaviour which we want register as a service
public class FirstService : MonoBehaviour, IService {
    private void Awake() {
        ServiceLocator.Instance.RegisterService(this);
    }
    public void Initialize() {
        Debug.Log("FirstService initialized");
    }

    public void Dispose() {
        Debug.Log("FirstService disposed");
    }

    public void DoSomething() {
        Debug.Log("FirstService is doing something");
    }
}
