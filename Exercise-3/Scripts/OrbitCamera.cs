using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    public float rotSpeed = .5f;
    private float rotY;
    private Vector3 offset;
    public float cameraHeight;
    void Start()
    {
        rotY = transform.eulerAngles.y;
        offset = target.position - transform.position;
    }
    void LateUpdate()
    {
        rotY -= Input.GetAxis("Horizontal") * rotSpeed;
        Quaternion rotation = Quaternion.Euler(0, rotY, 0);
        transform.position = target.position - (rotation * offset * cameraHeight);
        transform.LookAt(target);
    }
}