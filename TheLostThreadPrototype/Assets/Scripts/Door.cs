using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animation animation;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSound;

    public void Open()
    {
        animation.Play("Open");
        if (openSound)
            audioSource.PlayOneShot(openSound);
    }
}