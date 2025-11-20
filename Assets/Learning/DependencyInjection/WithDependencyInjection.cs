using UnityEngine;

public class WithDependencyInjection : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    private AClassDoingForSomething aClassDoingForSomething;

    private void Start() {
        UnityAudioService unityAudioService = new UnityAudioService(audioSource);
        aClassDoingForSomething = new AClassDoingForSomething(unityAudioService);
        aClassDoingForSomething.DoSomething();

        // if we want to change the audio service later, we just need to uncomment this code and comment the previous one
        // SomeOtherAudioService audioService = new SomeOtherAudioService();
        // aClassDoingForSomething = new AClassDoingForSomething(audioService);
        // aClassDoingForSomething.DoSomething();
    }
}

public class AClassDoingForSomething {
    private readonly IAudioService audioService;

    public AClassDoingForSomething(IAudioService audioService)    {
        this.audioService = audioService;
    }

    public void DoSomething()    {
        audioService.PlaySound("jump");
    }
}
