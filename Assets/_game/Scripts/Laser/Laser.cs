using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _laserLenght;
    // Update is called once per frame
    void Update()
    {
        // Raycast için baþlangýç noktasý olarak belirlediðimiz objenin pozisyon ve rotasyonu üzerinden bir ýþýn oluþtur
        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);

        // Sonsuz mesafeye kadar ýþýn gönder
        float rayDistance = _laserLenght;

        // Iþýný görselleþtirme (isteðe baðlý)
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.black);

        // Raycast ile iþlem yap
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.GetComponent<PlayerMovementController>() != null)
                Debug.Log("Playera Çarptý");
            else
                Debug.Log("Iþýn bir nesneye çarptý: " + hit.collider.gameObject.name);
        }
    }
}
