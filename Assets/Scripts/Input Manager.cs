using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{

    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    private Camera mainCamera;
    #endregion
    //Reference to the input actions script created using input actions created
    [SerializeField]
    private PlayerActions playerControls;
    private void Awake()
    {
        Debug.Log("Awake");

        mainCamera = Camera.main;
        playerControls = new PlayerActions();
    }
    private void OnEnable()
    {
        Debug.Log("ENable");

        playerControls.Enable();
    }
    private void OnDisable()
    {
        Debug.Log("Disable");

        playerControls.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        //Passes the contex(Information regarding the touch) to the function
        playerControls.Touch.PrimaryContact.started += contex => StartPrimaryTouch(contex);
        playerControls.Touch.PrimaryContact.canceled += contex => EndPrimaryTouch(contex);

    }

    //Function called when primary touch starts
    private void StartPrimaryTouch(InputAction.CallbackContext context)
    {
        Debug.Log("heloooStart");
        if (OnStartTouch != null)
        {
            Vector3 Touchposition = playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
            Vector3 screenToWorldPosition = Utilities.ScreenToWorld(mainCamera, Touchposition);
            OnStartTouch(screenToWorldPosition, (float)context.startTime);

        }

    }
    private void EndPrimaryTouch(InputAction.CallbackContext context)
    {
        Debug.Log("heloooEnd");

        if (OnEndTouch != null)
        {
            Vector3 Touchposition = playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
            Vector3 screenToWorldPosition = Utilities.ScreenToWorld(mainCamera, Touchposition);
            OnEndTouch(screenToWorldPosition, (float)context.time);

        }
    }

    public Vector2 PrimaryPosition()
    {
        Vector3 Touchposition = playerControls.Touch.PrimaryPosition.ReadValue<Vector2>();
        Vector3 screenToWorldPosition = Utilities.ScreenToWorld(mainCamera, Touchposition);

        return screenToWorldPosition;
    }
}
