using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMode :  Laser
{
    private bool _isLocked = true;
    [SerializeField] private float _lockTimer;
    [SerializeField] private float _laserLockTime;

    private void Update()
    {
        float distance = Vector3.Distance(_player.transform.position, this.transform.position);
        if (distance <= 10f)
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
            if (_lockTimer >= _laserLockTime)
            {
                _lockTimer = 0;
                _isLocked = false;
            }
            //raycastý kapat
        }
        else
        {
            //konumunda dur
            _lockTimer += Time.deltaTime;
            laserColor.startColor = Color.red;
            laserColor.endColor = Color.red;
            if (_lockTimer >= 1f)
            {
                _lockTimer = 0;
                _isLocked = true;
            }
            //raycastý aç
            Raycast();
        }
    }
}
