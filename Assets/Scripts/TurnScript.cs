using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnScript : MonoBehaviour
{

    private SoundEffects audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource=GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        audioSource.playTurnSound();
    }
}
