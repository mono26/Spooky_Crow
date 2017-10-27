using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource source;
    public AudioClip clip;
    public void Awake()
    {
        source = Camera.main.GetComponent<AudioSource>();
    }

    public void PlayClip()
    {
        Debug.Log("Lo REPRODUJE Wey");
        source.PlayOneShot(clip, 0.2f);
    }
}
