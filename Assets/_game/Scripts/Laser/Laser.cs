using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject rayOriginObject;

    // Update is called once per frame
    void Update()
    {
        if (rayOriginObject != null)
        {
            // Raycast için baþlangýç noktasý olarak belirlediðimiz objenin pozisyon ve rotasyonu üzerinden bir ýþýn oluþtur
            Ray ray = new Ray(rayOriginObject.transform.position, rayOriginObject.transform.forward);

            // Sonsuz mesafeye kadar ýþýn gönder
            float rayDistance = 10f;

            // Iþýný görselleþtirme (isteðe baðlý)
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.black);

            // Raycast ile iþlem yap
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                Debug.Log("Iþýn bir nesneye çarptý: " + hit.collider.gameObject.name);
            }
        }
    }
}
