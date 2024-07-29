using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] brokenPieces = new GameObject[3];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void breakIt()
    {
        foreach(GameObject piece in brokenPieces) 
        {
            piece.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
