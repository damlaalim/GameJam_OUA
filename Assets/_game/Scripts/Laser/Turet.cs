using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turet : MonoBehaviour
{
    public bool CanLook;
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        if (CanLook)
        {
            transform.LookAt(Player.position);
        }
        else
        {

        }
    }
}
