using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class ObstacleCollider : MonoBehaviour
{
    private int count = 0;
    [SerializeField]
    private CubeObstacle obstacle;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("Breakkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
        obstacle.breakIt();
    }
}
