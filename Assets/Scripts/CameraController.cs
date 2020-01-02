using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float xMin = 8.55f;
    [SerializeField]
    private float xMax = 41.6f;
    [SerializeField]
    private float zMin = -1.5f;
    [SerializeField]
    private float zMax = 25.8f;



    void LateUpdate()
    {
        float X = Mathf.Clamp(transform.position.x + Input.GetAxis("Horizontal"), xMin, xMax);
        float Z = Mathf.Clamp(transform.position.z + Input.GetAxis("Vertical"), zMin, zMax);
        transform.position = new Vector3(X, transform.position.y, Z);
    }
}
