using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] protected GameObject _player;
    [SerializeField] protected GameObject _box;//görsel
    [SerializeField] private Transform _startPoint;//karakterin spawnlanacaðý nokta

    [SerializeField] protected float _laserLenght;//lazerin boyu
    
    public void Raycast()
    {
        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * _laserLenght, Color.cyan);//raycast görselleþtirme

        if (Physics.Raycast(ray, out RaycastHit hit, _laserLenght))
        {
            if (hit.collider.TryGetComponent<PlayerMovementController>(out var _player))//raycast karakteri bulduysa
            {
                Debug.Log("Playera Çarptý");
                _player.enabled = false;
                _player.transform.position = Vector3.zero;
                _player.enabled = true;
            }
            else//bulamadýysa
                Debug.Log("Iþýn bir nesneye çarptý: " + hit.collider.gameObject.name);
        }
    }
}
