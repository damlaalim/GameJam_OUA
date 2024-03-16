using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject _box;//g�rsel
    [SerializeField] private Transform _startPoint;//karakterin spawnlanaca�� nokta

    [SerializeField] private float _laserLenght;//lazerin boyu
    [SerializeField] private float _closedLaserDuration;//Lazerin kapal� kalma s�resi
    [SerializeField] private float _onLaserDuration;//Lazerin a��k kalma s�resi
    [SerializeField] private float timer;
    [SerializeField] private bool _isActive;//a��k veya kapal� olma durumu
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
        timer += Time.deltaTime;//zamanlay�c�da ge�en s�reyi tutuyoruz 
        if (timer >= _onLaserDuration)//s�re a��k kalma s�resine denk geldi ise
        {
            Debug.Log("Timer 0land� laser kapand�");
            timer = 0;// zamanlay�c�y� s�f�rl�yoruz
            _isActive = false;//lazeri kapat�yoruz
            _box.SetActive(false);//g�rseli kapat�yoruz
        }

        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * _laserLenght, Color.black);//raycast g�rselle�tirme

        if (Physics.Raycast(ray, out RaycastHit hit, _laserLenght))
        {
            if (hit.collider.TryGetComponent<PlayerMovementController>(out var _player))//raycast karakteri bulduysa
            {
                Debug.Log("Playera �arpt�");
                _player.enabled = false;
                _player.transform.position = Vector3.zero;
                _player.enabled = true;
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
