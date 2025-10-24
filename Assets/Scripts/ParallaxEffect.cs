using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera mainCamera;
    public Transform followTarget;

    Vector2 startingPosition;
    float startingZ;
    Vector2 camMoveSinceStart => (Vector2)mainCamera.transform.position - startingPosition;
    float zDistanceFromTarget => transform.position.z - followTarget.position.z;
    float clippingPosition => mainCamera.transform.position.z + (zDistanceFromTarget > 0 ? mainCamera.farClipPlane : mainCamera.nearClipPlane);

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
