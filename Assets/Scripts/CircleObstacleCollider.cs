using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObstacleCollider : MonoBehaviour
{
    [SerializeField]
    private CircleObstacle obstacle;
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
