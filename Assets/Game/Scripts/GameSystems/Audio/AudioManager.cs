using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip reward;


    public void PlayReward()
    {
        if (this.audioSource.isPlaying)
        {
            this.audioSource.Stop();
        }
        this.audioSource.PlayOneShot(this.reward);
    }
}