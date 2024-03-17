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
        if (transform.position.y <= minHeightToFall && virtualCamera != null)
        {
            // fall log
            Debug.Log("fall");
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;
            // respawn delay
            ResetPosition();
        }
    }

    // fall sonrasý reset fonksiyonunun çaðýrýlmasý
    public void ResetPosition()
    {
        gameObject.GetComponent<PlayerMovementController>().enabled = false;
        transform.position = startingPosition; // start pos
        Invoke("ResetCamera", 0.2f);
    }
    public void ResetCamera()
    {
        virtualCamera.Follow = gameObject.transform;
        virtualCamera.LookAt = gameObject.transform;
        gameObject.GetComponent<PlayerMovementController>().enabled = true;
    }
}
