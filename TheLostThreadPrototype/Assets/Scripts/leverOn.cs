using UnityEngine;

public class leverOn : MonoBehaviour
{
    [SerializeField] private Animation animation;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSound;

    public void On()
    {
        animation.Play("Machine turn on");
        if (openSound)
            audioSource.PlayOneShot(openSound);
    }
}