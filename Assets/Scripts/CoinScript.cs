using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CoinScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField, Range(0, 500f)]
    private float speed = 100f;
    

    private GameObject Source;
    private SoundEffects audioSource;
    void Start()
    {
        Source = GameObject.FindGameObjectWithTag("SoundEffects");
        audioSource = Source.GetComponent<SoundEffects>();
        if (audioSource != null)
            Debug.Log("Yayyyyy");
    }

    // Update is called once per frame
    void Update()
    {
        float rotationAmount = speed * Time.deltaTime;
        transform.Rotate(rotationAmount, 0, 0);
    }
    private void OnTriggerEnter(Collider collision)
    {
        Destroy(gameObject);
        audioSource.playCoinSound();
    }
}
