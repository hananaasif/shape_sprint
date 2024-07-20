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

    [SerializeField]
    private float increaseAmount=0.01f;

    private float initialWidth;
    private float initialHeight;

    private float coolDownTime = 0f;

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
    }
    private IEnumerator shapeShiftCoroutine(Vector2 startPosition)
    {
        Debug.Log("Shape ShiftingFunctionnnnnn");
         
            if (coolDownTime < 0.2f)
            {
                Debug.Log("Waitinggggg");

                coolDownTime += Time.deltaTime;
            }
            else
            {
                coolDownTime = 0;
                Debug.Log("Shape Shifting");
                Vector3 direction = inputManager.PrimaryPosition() - startPosition;
                Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
                float changeInHeight = 0f;
                if (Vector2.Dot(Vector2.up, direction) > 0.5)
                {
                    changeInHeight = 1f;
                }
                else if (Vector2.Dot(Vector2.down, direction) > 0.5)
                {
                    changeInHeight = -1f;
                }
                float currentHeight = transform.localScale.y;
                float newHeight = currentHeight + increaseAmount * changeInHeight;

                // Calculate the new width required to maintain the same area
                float currentWidth = transform.localScale.x;
                float newWidth = (initialWidth * initialHeight) / newHeight;

                // Apply the new scale to the GameObject
                transform.localScale = new Vector3(newWidth, newHeight, transform.localScale.z);
            }
        
        
            
        

        yield return null;
    }

}
