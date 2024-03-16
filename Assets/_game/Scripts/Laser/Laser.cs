using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject Box;

    [SerializeField] private float _laserLenght;//lazerin boyu
    [SerializeField] private float _closedLaserDuration;//
    [SerializeField] private float _onLaserDuration;
    [SerializeField] private float timer;
    [SerializeField] private bool _isActive;
    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            LaserActive();
        }
        else
        {
            LaserDisable();
        }
        
        
    }
    private void LaserActive()
    {
        timer += Time.deltaTime;
        if (timer >= _onLaserDuration)
        {
            Debug.Log("Timer 0landý laser kapandý");
            timer = 0;
            _isActive = false;
            Box.SetActive(false);
        }

        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);


        float rayDistance = _laserLenght;

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.black);//raycast görselleþtirme

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.GetComponent<PlayerMovementController>() != null)//raycast karakteri bulduysa
                Debug.Log("Playera Çarptý");
            else//bulamadýysa
                Debug.Log("Iþýn bir nesneye çarptý: " + hit.collider.gameObject.name);
        }
    }
    private void LaserDisable()
    {
        timer += Time.deltaTime;
        if (timer >= _closedLaserDuration)
        {
            Debug.Log("Timer 0landý laser açýldý");
            timer = 0;
            _isActive = true;
            Box.SetActive(true);
        }
    }
}
