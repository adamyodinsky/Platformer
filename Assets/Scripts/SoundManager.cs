using UnityEngine;

public class SoundManager : MonoBehaviour
{   
    [SerializeField] AudioSource ManagerAudioSource;

    public void PlaySound(AudioClip clip, float volume = 0.5f)
    {
        ManagerAudioSource.PlayOneShot(clip, volume);
    }
}
