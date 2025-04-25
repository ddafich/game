using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public List<AudioClip> footstepFx;

    private AudioSource footstepSource;

    private void Start()
    {
        footstepSource = GetComponent<AudioSource>();
    }
    void PlayFootstep()
    {
        AudioClip clip;
        clip = footstepFx[Random.Range(0, footstepFx.Count)];
        footstepSource.clip = clip;
        footstepSource.volume = Random.Range(0.2f, 0.4f);
        footstepSource.pitch = Random.Range(0.8f, 1.2f);
        footstepSource.Play();
    }
}
