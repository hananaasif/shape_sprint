using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesScript : MonoBehaviour
{
    [SerializeField, Range(0, 500f)]
    private float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotationAmount = speed * Time.deltaTime;
        transform.Rotate(0, 0, rotationAmount);
    }
}
