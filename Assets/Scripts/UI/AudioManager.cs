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
    private void Start()
    {
        bgmSource.clip = background;
        bgmSource.Play();
        DontDestroyOnLoad(gameObject);
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    
}
