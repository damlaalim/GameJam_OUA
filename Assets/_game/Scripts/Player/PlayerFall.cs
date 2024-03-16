using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerFall : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public float minHeightToFall = -10f; // fall trashold
    public Vector3 startingPosition; 

    private void Start()
    {
        startingPosition = transform.position; 
    }

    private void Update()
    {
        if (transform.position.y <= minHeightToFall)
        {
            // fall log
            Debug.Log("fall");
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
            // respawn delay
            Invoke("ResetPosition", 0.5f);
        }
    }

    // fall sonrasý reset fonksiyonunun çaðýrýlmasý
    private void ResetPosition()
    {
        transform.position = startingPosition; // start pos
        Invoke("ResetCamera", 0.2f);
    }
    private void ResetCamera()
    {
        virtualCamera.Follow = gameObject.transform;
        virtualCamera.LookAt = gameObject.transform;
    }
}
