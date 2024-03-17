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

        Debug.DrawRay(ray.origin, ray.direction * _laserLenght, Color.red);//raycast görselleþtirme

        if (Physics.Raycast(ray, out RaycastHit hit, _laserLenght))
        {
            if (hit.transform.gameObject.tag == "Player")//raycast karakteri bulduysa
            {
                Debug.Log("Playeri vurdu");
                ReturnBase();
            }
            else//bulamadýysa
                Debug.Log("Iþýn bir nesneye çarptý: " + hit.collider.gameObject.name);
        }
    }

    private void ReturnBase()
    {
        _player.GetComponent<PlayerFall>().ResetPosition();

    }
}
