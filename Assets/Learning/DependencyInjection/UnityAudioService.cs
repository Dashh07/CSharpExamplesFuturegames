using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public interface IAudioService {
    void PlaySound(string soundId);
}

public class UnityAudioService : IAudioService {
    private readonly AudioSource audioSource;
    private AsyncOperationHandle<AudioClip> handle;

    public UnityAudioService(AudioSource audioSource) {
        this.audioSource = audioSource;
    }
    public void PlaySound(string soundId) {
        handle = Addressables.LoadAssetAsync<AudioClip>($"Sounds/{soundId}");
        handle.Completed += PlayClip;

    }
    private void PlayClip(AsyncOperationHandle<AudioClip> clip) {
        audioSource.clip = clip.Result;
        audioSource.Play();
    }

    public void OnDispose() {
        if(!audioSource.isPlaying) {
            handle.Release();
            Resources.UnloadUnusedAssets();
        }
    }
}


// in case we decide later that we want to use an external audio service, like FMOD or some others, since the behavior is the same we can just implement the same interface
// and change the implementation in the constructor. This way we don't have to change the code of the classes that use the audio service.'
public class SomeOtherAudioService : IAudioService {
    public void PlaySound(string soundId) {
        throw new System.NotImplementedException();
    }
}
