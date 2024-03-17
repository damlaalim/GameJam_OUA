using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAmmo : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * 8f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerFall>().ResetPosition();
        }
    }
}
