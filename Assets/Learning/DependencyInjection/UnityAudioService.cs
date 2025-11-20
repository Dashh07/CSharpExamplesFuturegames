using UnityEngine;
using UnityEngine.AddressableAssets;

public interface IAudioService {
    void PlaySound(string soundId);
}

public class UnityAudioService : IAudioService {
    private readonly AudioSource audioSource;
    public UnityAudioService(AudioSource audioSource) {
        this.audioSource = audioSource;
    }
    public void PlaySound(string soundId)  {
        audioSource.clip = Addressables.LoadAssetAsync<AudioClip>($"Sounds/{soundId}").WaitForCompletion();
        audioSource.Play();
    }
}


// in case we decide later that we want to use an external audio service, like FMOD or some others, since the behavior is the same we can just implement the same interface
// and change the implementation in the constructor. This way we don't have to change the code of the classes that use the audio service.'
public class SomeOtherAudioService : IAudioService {
    public void PlaySound(string soundId) {
        throw new System.NotImplementedException();
    }
}
