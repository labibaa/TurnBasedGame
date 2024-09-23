using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    
    float moveSpeed = 10f;
    
    float rotationSpeed = 100f;
    float zoomAmount = 1f;
    float zoomSpeed = 5f;
    const float MIN_FOLLOW_Y = 0f;
    const float MAX_FOLLOW_Y = 6f;
    private Vector3 targetFollowOffset;
    private CinemachineTransposer cinemachineTransposer;

    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    void Update()
    {
        //if (GridSystem.instance.IsGridOn)
        //{
            //HandleMovement();
            HandleRotation();
            HandleZoom();
        //}
    }

    private void HandleMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;

        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        float minZoomDistance = -10f; // Set the minimum distance (closer zoom limit)
        float maxZoomDistance = -5f;  // Set the maximum distance (farther zoom limit)

        if (Input.mouseScrollDelta.y > 0)
        {
            // Zoom in: Reduce the y value if above MIN_FOLLOW_Y, otherwise adjust only z
            if (targetFollowOffset.y > MIN_FOLLOW_Y)
            {
                targetFollowOffset.y -= zoomAmount;
                targetFollowOffset.z += zoomAmount;
            }
            else
            {
                // Only move along the z-axis once MIN_FOLLOW_Y is reached
                inputMoveDir.z = +50f;
            }
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            // Zoom out: Increase the y value if below MAX_FOLLOW_Y, otherwise adjust only z
            if (targetFollowOffset.y < MAX_FOLLOW_Y)
            {
                targetFollowOffset.y += zoomAmount;
                targetFollowOffset.z -= zoomAmount;
            }
            else
            {
                // Only move along the z-axis once MAX_FOLLOW_Y is reached
                inputMoveDir.z = -50f;
            }
        }

        // Smoothly interpolate the moveVector to the target direction
        Vector3 moveVector = transform.forward * inputMoveDir.z;
        Vector3 smoothMoveVector = Vector3.Lerp(Vector3.zero, moveVector, Time.deltaTime * zoomSpeed);  // Smooth the movement

        // Apply the movement to the camera position
        transform.position += smoothMoveVector * moveSpeed * Time.deltaTime;

        // Clamp the y value to prevent it from exceeding bounds
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y, MAX_FOLLOW_Y);

        // Clamp the z-axis for zoom limits
        targetFollowOffset.z = Mathf.Clamp(targetFollowOffset.z, minZoomDistance, maxZoomDistance);

        // Update the camera's follow offset smoothly
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }




}
