using System.Collections.Generic;
using UnityEngine;

public class SwingSfx : MonoBehaviour
{
    public List<AudioClip> swing;
    public List<AudioClip> swingHeavy;
    private AudioSource swingSource;
    private void Start()
    {
        swingSource = GetComponent<AudioSource>();
    }
    void PlaySwingSfx()
    {
        AudioClip clip;
        clip = swing[Random.Range(0, swing.Count)];
        swingSource.clip = clip;
        swingSource.volume = Random.Range(0.2f, 0.4f);
        swingSource.pitch = Random.Range(0.8f, 1.2f);
        swingSource.Play();
    }
    void PlaySwingHeavySfx()
    {
        AudioClip clip;
        clip = swingHeavy[Random.Range(0, swingHeavy.Count)];
        swingSource.clip = clip;
        swingSource.volume = Random.Range(0.2f, 0.4f);
        swingSource.pitch = Random.Range(0.8f, 1.2f);
        swingSource.Play();
    }
}
