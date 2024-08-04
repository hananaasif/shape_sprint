using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float upwardForceMin = 5f;
    public float upwardForceMax = 70f;
    public float lateralForce = 70f;
    public float minSize = 0f;
    public float maxSize = 0.00000001f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Randomize the upward force within the specified range
        float upwardForce = Random.Range(upwardForceMin, upwardForceMax);

        // Apply an upward force with some random lateral force
        Vector3 force = new Vector3(
            Random.Range(-lateralForce, lateralForce),
            upwardForce,
            Random.Range(-lateralForce, lateralForce)
        );

        

        // Randomize the size
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, size);

        // Add the force to the Rigidbody
        rb.AddForce(force, ForceMode.Impulse);

        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
