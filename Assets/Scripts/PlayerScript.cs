using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField]
    private GameObject Manager;
    private InputManager inputManager;
    [SerializeField]
    private GameObject cube;
    
    [SerializeField]
    private GameObject sphere;
    private GameObject currentShape;
    private string namee;

    private Coroutine shapeShift;

    [SerializeField , Range(0,0.5f)]
    private float increaseAmount=0.04f;
    [SerializeField]
    private float threshhold = 0f;

    private float initialWidth;
    private float initialHeight;

    [SerializeField]
    private float maxHeight = 2f;
    [SerializeField]
    private float minHeight = 0.3f;

    [SerializeField]
    private float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = Manager.GetComponent<InputManager>();
        currentShape = Instantiate(cube, transform.position, transform.rotation, transform);
        namee = "Cube";


    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.V))
        {
            convertShape();
        }
        
    }
    private void convertShape()
    {
        Vector3 position = currentShape.transform.position;
        Quaternion rotation = currentShape.transform.rotation;
        Destroy(currentShape);
        if (namee == "Cube")
        {
            currentShape = Instantiate(sphere, position, rotation, transform);
            namee = "Sphere";
        }
        else if (namee == "Sphere")
        {
            currentShape = Instantiate(cube, position, rotation, transform);
            namee = "Cube";
        }
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
                currentShape.transform.localScale = new Vector3(newWidth, newHeight, transform.localScale.z);
                Debug.Log("Height" + " Width" + currentShape.transform.localScale);

            }
            else
            {
                Debug.Log("Height Limit Reached");
            }
            


            yield return null;
    }







    }

}
