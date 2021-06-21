using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public Transform subject;

    private Vector2 startPosition;

    private float startZ;
    Vector2 travel => (Vector2)cam.transform.position - startPosition;
    private float distanceFromSubject => transform.position.z - subject.position.z;

    private float clippingPlane =>
        (cam.transform.position.z + (distanceFromSubject > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(distanceFromSubject/clippingPlane);
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }
    
    void Update()
    {
        Vector2 newPos = startPosition + travel * 5.4f;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}