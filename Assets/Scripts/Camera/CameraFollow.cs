using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 0.125f;

    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Clamp the camera position
        float clampedX = Mathf.Clamp(smoothedPosition.x, minPosition.x, maxPosition.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minPosition.y, maxPosition.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

}
