using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject _box;
    [SerializeField] private Transform _startPoint;

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
            Debug.Log("Timer 0land� laser kapand�");
            timer = 0;
            _isActive = false;
            _box.SetActive(false);
        }

        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);


        float rayDistance = _laserLenght;

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.black);//raycast g�rselle�tirme

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.GetComponent<PlayerMovementController>() != null)//raycast karakteri bulduysa
            {
                Debug.Log("Playera �arpt�");
                hit.collider.GetComponent<PlayerMovementController>().enabled = false;
                hit.collider.GetComponent<PlayerMovementController>().transform.position = Vector3.zero;
                hit.collider.GetComponent<PlayerMovementController>().enabled = true;
            }
            else//bulamad�ysa
                Debug.Log("I��n bir nesneye �arpt�: " + hit.collider.gameObject.name);
        }
    }
    private void LaserDisable()
    {
        timer += Time.deltaTime;
        if (timer >= _closedLaserDuration)
        {
            Debug.Log("Timer 0land� laser a��ld�");
            timer = 0;
            _isActive = true;
            _box.SetActive(true);
        }
    }
}
