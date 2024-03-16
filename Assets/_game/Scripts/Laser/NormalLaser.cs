using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLaser : Laser
{
    [SerializeField] private float _closedLaserDuration;//Lazerin kapal� kalma s�resi
    [SerializeField] private float _onLaserDuration;//Lazerin a��k kalma s�resi

    [SerializeField] private float timer;

    [SerializeField] private bool _isActive;//a��k veya kapal� olma durumu
    private void Start()
    {
        _laserLenght = _box.gameObject.transform.localScale.z;
    }
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
        Raycast();
        if (timer >= _onLaserDuration)//s�re a��k kalma s�resine denk geldi ise
        {
            Debug.Log("Timer 0land� laser kapand�");
            timer = 0;// zamanlay�c�y� s�f�rl�yoruz
            _isActive = false;//lazeri kapat�yoruz
            _box.SetActive(false);//g�rseli kapat�yoruz
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
