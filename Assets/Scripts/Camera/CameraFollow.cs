using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothSpeed = 8f;

    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;
    [SerializeField] private Vector2 minPosition2;
    [SerializeField] private Vector2 maxPosition2;

    private Vector2 minPos;
    private Vector2 maxPos;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        minPosition2.y = minPosition.y;
        maxPosition2.y = maxPosition.y;

        minPos = minPosition;
        maxPos = maxPosition;
    }

    private void FixedUpdate()
    {
        if (target.position.x > maxPos.x + 20f)
        {
            minPos.x = minPosition2.x;
            maxPos.x = maxPosition2.x;
        }

        if (target.position.x < minPos.x - 20f)
        {
            minPos.x = minPosition.x;
            maxPos.x = maxPosition.x;
        }

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Clamp the camera position
        float clampedX = Mathf.Clamp(smoothedPosition.x, minPos.x, maxPos.x);
        float clampedY = Mathf.Clamp(smoothedPosition.y, minPos.y, maxPos.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

}
