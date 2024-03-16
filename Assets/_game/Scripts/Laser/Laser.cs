using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] protected GameObject _player;
    [SerializeField] protected GameObject _box;//g�rsel
    [SerializeField] private Transform _startPoint;//karakterin spawnlanaca�� nokta

    [SerializeField] protected float _laserLenght;//lazerin boyu
    
    public void Raycast()
    {
        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * _laserLenght, Color.cyan);//raycast g�rselle�tirme

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
}
