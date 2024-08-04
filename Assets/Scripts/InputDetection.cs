using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetection : MonoBehaviour
{
    [SerializeField]
    private GameObject Manager;
    private InputManager inputManager;

    [SerializeField]
    private GameObject trail;

    [SerializeField]
    private float swipeDistanceMin = .2f;
    [SerializeField]
    private float maxTime = 2f;
    [SerializeField, Range(0f, 1f)]
    private float DirectionThreshhold = 0.5f;


    private Vector2 startPosition;
    private float startTime;

    private Vector2 endPosition;
    private float endTime;

    [SerializeField]
    private PlayerScript playerScript;


    private void Awake()
    {

        inputManager = Manager.GetComponent<InputManager>();
    }

    private void OnEnable()
    {

        //Subscribe to the OnStartTouch Event so that when the event is trigerred, SwipeStart is called
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
        inputManager.DoubleTapEvent += changeShape;
    }

    private void OnDisable()
    {
        //UnSubscribe from the OnStartTouch Event 

        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
        inputManager.DoubleTapEvent -= changeShape;

    }
    private void SwipeStart(Vector2 position, float time)
    {
        Debug.Log("SwipeStart");

        startPosition = position;
        startTime = time;
        playerScript.shiftshape(startPosition);


    }
    private void SwipeEnd(Vector2 position, float time)
    {
        Debug.Log("SwipeEnd");
        playerScript.stopShapeShift();
        endPosition = position;
        endTime = time;
        DetectSwipe();

    }

    private void DetectSwipe()
    {
        Debug.Log("DetectSwipe");
        Debug.Log("StartPosition" + startPosition + " End Position" + endPosition);
        if (Vector2.Distance(startPosition, endPosition) >= swipeDistanceMin && (endTime - startTime) <= maxTime)
        {

            Debug.DrawLine(startPosition, endPosition, Color.red, 50f);
            Debug.Log("Drawing Lineee");
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }

    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > DirectionThreshhold)
        {
            Debug.Log("Swipe Up Bossss");
        }
        else if (Vector2.Dot(Vector2.down, direction) > DirectionThreshhold)
        {
            Debug.Log("Swipe Down Bossss");
        }
        else if (Vector2.Dot(Vector2.right, direction) > DirectionThreshhold)
        {
            Debug.Log("Swipe RIght Bossss");
        }
        else if (Vector2.Dot(Vector2.left, direction) > DirectionThreshhold)
        {
            Debug.Log("Swipe Left Bossss");
        }
    }

    private void changeShape()
    {
        playerScript.convertShape();
    }
}
