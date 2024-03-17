using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float timer;
    [SerializeField] float attackTime;
    public GameObject Ammo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= attackTime)
        {
            timer = 0;
            var obj = Instantiate(Ammo, this.transform.position,Quaternion.identity);
        }
    }


}
