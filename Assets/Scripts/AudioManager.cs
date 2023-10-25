using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip intro;
    [SerializeField] private AudioClip ghostMusic;
    private AudioSource clips;
    private ZombieMovement script;
    void Start()
    {
        clips = GetComponent<AudioSource>();
        script = FindObjectOfType<ZombieMovement>();
        StartCoroutine(delayAfterStart());
    }

    private IEnumerator delayAfterStart() {

        clips.clip = intro;
        clips.Play();

        yield return new WaitForSeconds(1.5f);

        clips.clip = ghostMusic;
        clips.loop = true;
        clips.Play();
        script.StartMovement();

    }
}
