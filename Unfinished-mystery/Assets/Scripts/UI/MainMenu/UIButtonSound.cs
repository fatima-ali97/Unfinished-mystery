using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void PlayHover()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }
}