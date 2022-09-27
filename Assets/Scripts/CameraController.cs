using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffset;
    private float zoomSpeed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 inputMoveDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDirection.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDirection.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDirection.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDirection.x = +1f;
        }

        float moveSpeed = 10f;
        Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;

        Vector3 rotationVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            rotationVector.y = +1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotationVector.y = -1f;
        }

        float rotationSpeed = 100f;
        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;

        
        Vector3 followOffset = cinemachineTransposer.m_FollowOffset;
        float zoomAmount = 1f;
        if (Input.GetKey(KeyCode.F))
        {
            targetFollowOffset.y -= zoomAmount;
        }

        if (Input.GetKey(KeyCode.T))
        {
            targetFollowOffset.y += zoomAmount;
        }

        targetFollowOffset.y = Mathf.Clamp(followOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        Mathf.Lerp(cinemachineTransposer.m_FollowOffset.y, followOffset.y, 3f);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(
            cinemachineTransposer.m_FollowOffset,
            targetFollowOffset,
            Time.deltaTime * zoomSpeed
        );
    }
}
