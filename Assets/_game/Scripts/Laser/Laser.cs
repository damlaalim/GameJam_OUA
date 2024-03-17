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

        Debug.DrawRay(ray.origin, ray.direction * _laserLenght, Color.red);//raycast g�rselle�tirme

        if (Physics.Raycast(ray, out RaycastHit hit, _laserLenght))
        {
            if (hit.transform.gameObject.tag == "Player")//raycast karakteri bulduysa
            {
                Debug.Log("Playeri vurdu");
                ReturnBase();
            }
            else//bulamad�ysa
                Debug.Log("I��n bir nesneye �arpt�: " + hit.collider.gameObject.name);
        }
    }

    private void ReturnBase()
    {
        _player.GetComponent<PlayerFall>().ResetPosition();

    }
}
