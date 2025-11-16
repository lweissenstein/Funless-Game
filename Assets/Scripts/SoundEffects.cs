using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip bulletHitClip;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f; 
        audioSource.volume = 1f;
    }

    public void PlayBulletHit(Vector3 position)
    {
        if (bulletHitClip != null)
        {
            // clip with a volume multiplier
            audioSource.PlayOneShot(bulletHitClip, 2.0f);
        }
    }
}
