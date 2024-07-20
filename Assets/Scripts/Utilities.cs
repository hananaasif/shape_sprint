using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    //Converts from screen to world coordinates
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        //Because we don't need the distance to the object, we just need to check for touch input
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
}
