using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalLaser : Laser
{
    [SerializeField] private float _closedLaserDuration;//Lazerin kapalý kalma süresi
    [SerializeField] private float _onLaserDuration;//Lazerin açýk kalma süresi

    [SerializeField] private float timer;

    [SerializeField] private bool _isActive;//açýk veya kapalý olma durumu
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
        timer += Time.deltaTime;//zamanlayýcýda geçen süreyi tutuyoruz 
        Raycast();
        if (timer >= _onLaserDuration)//süre açýk kalma süresine denk geldi ise
        {
            Debug.Log("Timer 0landý laser kapandý");
            timer = 0;// zamanlayýcýyý sýfýrlýyoruz
            _isActive = false;//lazeri kapatýyoruz
            _box.SetActive(false);//görseli kapatýyoruz
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
            _box.SetActive(true);
        }
    }
}
