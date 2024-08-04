using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] brokenPieces = new GameObject[3];
    private bool broken = false;
    [SerializeField]
    private Animator textAnimator;
    private Canvas canvas;

    private SoundEffects audioSource;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        textAnimator = canvas.transform.Find("Perfect").GetComponent<Animator>();
        audioSource = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffects>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void breakIt()
    {
        broken = true;
        audioSource.playBreakSound();
        foreach(GameObject piece in brokenPieces) 
        {
            piece.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!broken)
        {
            audioSource.playPerfectSound();
            textAnimator.SetTrigger("Perfect");
            Handheld.Vibrate();

        }
    }

}
