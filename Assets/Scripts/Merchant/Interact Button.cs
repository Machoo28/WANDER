using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    //Detection Point
    public Transform detectionPoint;
    //Detection Radius
    private const float detectionRadius = 0.2f;
    //Detection Layer
    public LayerMask detectionLayer;

    void Update()
    {
        if (DetectObject())
        {
            if (InteractInput())
            {
                Debug.Log("Interact");
            }
        }
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {
        bool isDetected = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        return isDetected;
    }

}
