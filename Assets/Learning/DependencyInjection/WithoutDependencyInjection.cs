using UnityEngine;

public class WithoutDependencyInjection : MonoBehaviour {
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        PlaySound();
    }

    private void PlaySound() {
        audioSource.Play();
    }
}
