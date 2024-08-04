using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip gamePlay;
    [SerializeField]
    private AudioClip Win;

    private AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = gamePlay;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWin()
    {
        audioSource.clip = Win;
        audioSource.Play();
    }
}
