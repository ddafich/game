using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    [Header("audio clips")]
    public AudioClip background;
    public AudioClip player_move;
    public AudioClip player_attack;
    public AudioClip player_death;
    public AudioClip player_damaged;
    public AudioClip slime_move;
    
    public AudioClip slime_damaged;
    public AudioClip slime_death;

    public AudioClip coinPickup;
    public AudioClip powerUp;
    public AudioClip chestOpen;
    public AudioClip doorOpen;
    public AudioClip doorClose;
    private void Start()
    {
        bgmSource.clip = background;
        bgmSource.Play();
        DontDestroyOnLoad(gameObject);
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.volume = Random.Range(0.2f, 0.4f);
        sfxSource.pitch = Random.Range(0.8f, 1.2f);
        sfxSource.PlayOneShot(clip);
        Debug.Log("Playing clip: " + clip.name);
    }
    
}
