using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerFall : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public float minHeightToFall = -10f; // fall trashold
    public Vector3 startingPosition;
    public bool GoToSpawn;
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
        if (GoToSpawn)
        {
            float distace = Vector3.Distance(this.transform.position, startingPosition);
            ResetPosition();
            if(distace <= 1.2f)
            {
                GoToSpawn = false;
                gameObject.GetComponent<PlayerMovementController>().enabled = true;
            }
        }
    }

    // fall sonrasý reset fonksiyonunun çaðýrýlmasý
    public void ResetPosition()
    {
        GoToSpawn = true;
        gameObject.GetComponent<PlayerMovementController>().enabled = false;
        transform.position = startingPosition; // start pos
        virtualCamera.Follow = gameObject.transform;
        virtualCamera.LookAt = gameObject.transform;
    }

}
