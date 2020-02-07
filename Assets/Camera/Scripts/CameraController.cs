using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private Camera camera;
    private const float mapSizeOrthographicSize = 40f;
    private const float minOrthographicSize = 10f;

    void Awake()
    {
        camera = GetComponentInChildren<Camera>();
        float ratio = (float)camera.pixelWidth / camera.pixelHeight;
        
        float orthographicSize = mapSizeOrthographicSize / ratio / 2;
        camera.orthographicSize = Mathf.Clamp(orthographicSize, minOrthographicSize, Mathf.Infinity);
    }

}
