using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMode :  Laser
{
    private bool _isLocked = true;
    [SerializeField] float shootdelay;
    [SerializeField] private float _lockTimer;
    [SerializeField] private float _laserLockTime;
    private FieldOfView _fieldOfView;
    private ParticleSystem _particleSystem;
    private void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        _particleSystem = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(_player.transform.position, this.transform.position);
        if (_fieldOfView.canSeePlayer)
        {
            _box.SetActive(true);
            LaserLock(distance);
        }
        else
        {
            _box.SetActive(false);
        }
    }
    private void LaserLock(float distance)
    {
        LineRenderer laserColor = GetComponentInChildren<LineRenderer>();

        if (_isLocked)
        {
            transform.LookAt(_player.transform);
            _lockTimer += Time.deltaTime;
            laserColor.gameObject.transform.localScale = new Vector3(1f, 1f, distance);
            laserColor.startColor = Color.white;
            laserColor.endColor = Color.white;
            _laserLenght = distance;
            if (_lockTimer >= _laserLockTime)
            {
                _lockTimer = 0;
                _isLocked = false;
            }
            //raycastý kapat
        }
        else
        {
            _lockTimer += Time.deltaTime;
            shootdelay += Time.deltaTime;
            //konumunda dur
            //raycastý aç
            if (shootdelay >= 0.7f)
            {
                Raycast();
                shootdelay = 0;
                laserColor.startColor = Color.red;
                laserColor.endColor = Color.red;
            }
            if(_lockTimer >= 2f)
            {
                _isLocked = true;
                _lockTimer = 0;
                shootdelay = 0;
            }
        }
    }

}
