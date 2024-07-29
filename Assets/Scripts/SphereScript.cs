using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField, Range(0, 500f)]
    private float speed = 100f;

    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //float rotationAmount = speed * Time.deltaTime;
        //transform.Rotate(rotationAmount, 0, 0);
        transform.localScale = new Vector3(transform.localScale.y, transform.localScale.y, transform.localScale.y);


    }
    
}

