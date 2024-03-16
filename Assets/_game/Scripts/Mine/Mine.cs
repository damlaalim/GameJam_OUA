using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject Explosion;
    bool starttimer;
    float timer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerFall>().ResetCamera();
            other.GetComponent<PlayerFall>().ResetPosition();
            Explosion.SetActive(true);
            starttimer = true;
            //can azalsýn
        }
    }
    private void Update()
    {
        if (starttimer)
        {
            timer += Time.deltaTime;
            if(timer >= 1.5f)
            {
                Explosion.SetActive(false);
                starttimer = false;
                timer = 0;
            }
        }
    }
}
