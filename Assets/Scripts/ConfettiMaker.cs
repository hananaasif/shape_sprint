using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class Finish : MonoBehaviour
{

    [Header("Confetti Settings")]
    [SerializeField]
    private GameObject confetti;
    [SerializeField]

    private Material[]  confettiMaterial=new Material[6];
    private bool party = false;


    [Header("Window Settings")]
    [SerializeField]
    private GameObject nextLevelWindow;


    [Header("Managers Settings")]
    [SerializeField]
    private GameObject Manager;
    private InputManager inputManager;
    [SerializeField]
    private GameObject Player;
    private PlayerScript playerScript;

    private Music audioSource;
    private SoundEffects audioSource2;


    // Start is called before the first frame update
    void Start()
    {
        inputManager = Manager.GetComponent<InputManager>();
        playerScript=Player.GetComponent<PlayerScript>();
        audioSource=GameObject.FindGameObjectWithTag("Music").GetComponent<Music>();
        audioSource2 = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<SoundEffects>();


    }

    // Update is called once per frame
    void Update()
    {
        if (party == true)
        {
            Vector3 position = new Vector3(transform.position.x, transform.position.y+ 2.5f, transform.position.z);
            GameObject confettiClone= Instantiate(confetti,position,Quaternion.identity);
            confettiClone.GetComponent<MeshRenderer>().material = confettiMaterial[Random.RandomRange(0,6)];
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        party = true;
        if(other.CompareTag("Player") || other.CompareTag("Cube") || other.CompareTag("Sphere"))
        {
            Rigidbody rb= other.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);

            // Rotate the GameObject by 180 degrees around the Y-axis
            transform.Rotate(0, 180f, 0);

            // Stop the GameObject's movement after jumping
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        audioSource2.playPartySound();
        audioSource.PlayWin();
        Invoke("rotateAroundPlayer", 1f);
        Invoke("showNextLevelScreen", 2f);
    }

    private void showNextLevelScreen()
    {
        if (nextLevelWindow)
        {
            nextLevelWindow.SetActive(true);
            if(inputManager)
            {
                inputManager.disablePlayerControls();
            }
            else
            {

                UnityEngine.Debug.Log("Player Contros not found");

            }

        }
        else
        {
            UnityEngine.Debug.Log("WindowNotFound");

        }
    }
    private void rotateAroundPlayer()
    {
        playerScript.LevelCompleted();
    }
    
}
