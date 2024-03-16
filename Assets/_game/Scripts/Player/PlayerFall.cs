using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
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

            // respawn delay
            Invoke("ResetPosition", 0.5f);
        }
    }

    // fall sonrasý reset fonksiyonunun çaðýrýlmasý
    private void ResetPosition()
    {
        transform.position = startingPosition; // start pos
    }
}
