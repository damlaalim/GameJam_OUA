using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMode :  Laser
{
    private bool _isLocked = true;
    [SerializeField] float shootdelay;
    [SerializeField] private float _lockTimer;
    [SerializeField] private float _laserLockTime;

    private Turet _turet;
    private FieldOfView _fieldOfView;
    private void Start()
    {
        _turet = GetComponentInParent<Turet>();
        _fieldOfView = GetComponentInParent<FieldOfView>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(_player.transform.position, this.transform.position);
        if (_fieldOfView.canSeePlayer)
        {
            _box.SetActive(true);
            LaserLock(distance/5);
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
            _turet.CanLook = true;
            _lockTimer += Time.deltaTime;
            laserColor.gameObject.transform.localScale = new Vector3(1f, 1f, distance);
            laserColor.startColor = Color.white;
            laserColor.endColor = Color.white;
            _laserLenght = distance * 5;
            if (_lockTimer >= _laserLockTime)
            {
                _lockTimer = 0;
                _isLocked = false;
            }
            //raycastý kapat
        }
        else
        {
            _turet.CanLook = false;
            _lockTimer += Time.deltaTime;
            shootdelay += Time.deltaTime;
            //konumunda dur
            //raycastý aç
            if (shootdelay >= 0.7f)
            {
                _laserLenght = distance * 5;
                Raycast();
                shootdelay = 0;
                laserColor.startColor = Color.red;
                laserColor.endColor = Color.red;

            }
            if(_lockTimer >= 1.5f)
            {
                _isLocked = true;
                _lockTimer = 0;
                shootdelay = 0;
            }
        }
    }

}
