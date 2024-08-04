using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects: MonoBehaviour
{
    [Header("Audio Clips")]


    [SerializeField]
    private AudioClip collectible;
    [SerializeField]
    private AudioClip breakClip;
    [SerializeField]
    private AudioClip Perfect;
    [SerializeField]
    private AudioClip Coins;
    [SerializeField]
    private AudioClip Party;
    [SerializeField]
    private AudioClip Turn;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playCollectibleSound()
    {
        audioSource.clip= collectible;
        audioSource.Play();
    }
    public void playBreakSound()
    {
        audioSource.clip = breakClip;
        audioSource.Play();
    }
    public void playPerfectSound()
    {
        audioSource.clip = Perfect;
        audioSource.Play();
    }
    public void playPartySound()
    {
        audioSource.clip = Party;
        audioSource.Play();
    }
    public void playCoinSound()
    {
        //audioSource.clip = Coins;
        //audioSource.Play();
    }
    public void playTurnSound()
    {
        audioSource.clip = Turn;
        audioSource.Play();
    }
}
