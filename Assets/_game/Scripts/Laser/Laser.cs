using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject _box;//görsel
    [SerializeField] private Transform _startPoint;//karakterin spawnlanacaðý nokta

    [SerializeField] private float _laserLenght;//lazerin boyu
    [SerializeField] private float _closedLaserDuration;//Lazerin kapalý kalma süresi
    [SerializeField] private float _onLaserDuration;//Lazerin açýk kalma süresi
    [SerializeField] private float timer;
    [SerializeField] private bool _isActive;//açýk veya kapalý olma durumu
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
        timer += Time.deltaTime;//zamanlayýcýda geçen süreyi tutuyoruz 
        if (timer >= _onLaserDuration)//süre açýk kalma süresine denk geldi ise
        {
            Debug.Log("Timer 0landý laser kapandý");
            timer = 0;// zamanlayýcýyý sýfýrlýyoruz
            _isActive = false;//lazeri kapatýyoruz
            _box.SetActive(false);//görseli kapatýyoruz
        }

        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * _laserLenght, Color.black);//raycast görselleþtirme

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
