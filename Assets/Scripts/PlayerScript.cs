using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;
using UnityEngine.Splines;
using Unity.VisualScripting;

public class PlayerScript : MonoBehaviour
{

    [SerializeField]
    private GameObject Manager;
    private InputManager inputManager;
    [SerializeField]
    private GameObject cube;
    
    [SerializeField]
    private GameObject sphere;
    [SerializeField]
    private GameObject currentShape;

    private string namee;

    private Coroutine shapeShift;

    [SerializeField , Range(0,1f)]
    private float increaseAmount;
    [SerializeField]
    private float threshhold = 0f;

    private float initialWidth;
    private float initialHeight;

    [SerializeField]
    private float maxHeight;
    [SerializeField]
    private float minHeight;

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private SplineAnimate splineScript;


    [Header("Virtual Camera Rotation Settings")]
    //Virtual Camera settings
    [SerializeField] private float distance = 5.0f; // Distance from the player
    [SerializeField] private float speed = 30.0f; // Speed of rotation
    [SerializeField] private float pitch = 10.0f; // Pitch angle (up/down rotation)
    [SerializeField] private float yaw = 0.0f; 


    // Start is called before the first frame update
    void Start()
    {
       

        inputManager = Manager.GetComponent<InputManager>();
        
        setCamera(currentShape);
        namee = "Cube";
        saveSize();


    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            splineScript.Pause();
            if (splineScript.IsPlaying)
            {
                splineScript.Pause();
            }
            else
            {
                splineScript.Play();
            }
            
        }

    }
    //Saves the original size to calculate max and min size
    private void saveSize()
    {
        maxHeight = 1.7f * currentShape.transform.localScale.y;
        minHeight = 0.4f * currentShape.transform.localScale.y;
        increaseAmount = 0.04f * currentShape.transform.localScale.x;


    }
    //Sets the camera to follow the player shape
    private void setCamera(GameObject target)
    {
        virtualCamera.LookAt = target.transform;
        virtualCamera.Follow = target.transform;

    }
    // Update is called once per frame
    
    public void convertShape()
    {
        Debug.Log("Converting Shape(Function)");

        Vector3 position = currentShape.transform.position;
        Quaternion rotation = currentShape.transform.rotation;
        Destroy(currentShape);
        if (namee == "Cube")
        {
            currentShape = Instantiate(sphere, position, rotation,transform);
            namee = "Sphere";
        }
        else if (namee == "Sphere")
        {
            currentShape = Instantiate(cube, position, rotation, transform);
            namee = "Cube";
        }
        saveSize();
        setCamera(currentShape);


    }
    public void convertToCube()
    {
        if(namee!="Cube")
        {
            Vector3 position = currentShape.transform.position;
            Quaternion rotation = currentShape.transform.rotation;
            Destroy(currentShape);
            currentShape = Instantiate(cube, position, rotation, transform);
            namee = "Cube";
        }
        saveSize();
        setCamera(currentShape);
    }
    public void convertToSphere()
    {
        if (namee != "Sphere")
        {
            Vector3 position = currentShape.transform.position;
            Quaternion rotation = currentShape.transform.rotation;
            Destroy(currentShape);
            currentShape = Instantiate(sphere, position, rotation, transform);
            namee = "Sphere";
        }
        saveSize();
        setCamera(currentShape);
    }

    public void shiftshape(Vector2 startPosition)
    {

        Debug.Log("Shape Shiftinggggggggg");
        initialWidth=currentShape.transform.localScale.x;
        initialHeight=currentShape.transform.localScale.y;
        shapeShift = StartCoroutine(shapeShiftCoroutine(startPosition));

    }
    public void stopShapeShift()
    {
        StopCoroutine(shapeShift);
        shapeShift = null;
    }
    private IEnumerator shapeShiftCoroutine(Vector2 startPosition)
    {
        Debug.Log("Shape ShiftingFunctionnnnnn");
        while(true)
        {
            
            Debug.Log("Shape Shifting");
            Vector3 direction = inputManager.PrimaryPosition() - startPosition;
            Debug.Log("PrimaryPosition" + inputManager.PrimaryPosition());
            Debug.Log("startPosition" + startPosition);

            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            float changeInHeight = 0f;
            if (Vector2.Dot(Vector2.up, direction2D) > threshhold)
            {
                changeInHeight = 1f;
                Debug.Log("11111111111111111111");
            }
            else if (Vector2.Dot(Vector2.down, direction2D) > threshhold)
            {
                changeInHeight = -1f;
                Debug.Log("00000000000000000000");

            }
            float currentHeight = currentShape.transform.localScale.y;
            float newHeight = currentHeight + increaseAmount * changeInHeight;

            // Calculate the new width required to maintain the same area
            float currentWidth = currentShape.transform.localScale.x;
            float newWidth = (initialWidth * initialHeight) / newHeight;

            if(newHeight<maxHeight && newHeight>minHeight)
            {
                // Apply the new scale to the GameObject
                Debug.Log("Height" + " Width" + currentShape.transform.localScale);
                if (namee == "Sphere")
                    currentShape.transform.localScale = new Vector3(newHeight, newHeight, newHeight);

                else
                    currentShape.transform.localScale = new Vector3(newWidth, newHeight, currentShape.transform.localScale.z);

                
                Debug.Log("Height" + " Width" + currentShape.transform.localScale);

            }
            else
            {
                Debug.Log("Height Limit Reached");
            }
            


            yield return null;
    }







    }

    public void LevelCompleted()
    {
        yaw += speed * Time.deltaTime;
        float horizontalOffset = Mathf.Cos(yaw * Mathf.Deg2Rad) * distance;
        float verticalOffset = Mathf.Sin(yaw * Mathf.Deg2Rad) * distance;
        Vector3 offset = new Vector3(horizontalOffset, 0, verticalOffset);

        // Apply pitch
        Vector3 position = currentShape.transform.position + offset;
        position.y += Mathf.Sin(pitch * Mathf.Deg2Rad) * distance;

        // Set camera position and look at the player
        
        virtualCamera.transform.LookAt(currentShape.transform.position + Vector3.up * (distance * Mathf.Sin(pitch * Mathf.Deg2Rad)));
        // Keep the camera looking at the player
        virtualCamera.transform.LookAt(currentShape.transform.position);

    }

}
