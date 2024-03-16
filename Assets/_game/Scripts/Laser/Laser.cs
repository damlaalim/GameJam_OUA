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
            // Raycast i�in ba�lang�� noktas� olarak belirledi�imiz objenin pozisyon ve rotasyonu �zerinden bir ���n olu�tur
            Ray ray = new Ray(rayOriginObject.transform.position, rayOriginObject.transform.forward);

            // Sonsuz mesafeye kadar ���n g�nder
            float rayDistance = 10f;

            // I��n� g�rselle�tirme (iste�e ba�l�)
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.black);

            // Raycast ile i�lem yap
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                Debug.Log("I��n bir nesneye �arpt�: " + hit.collider.gameObject.name);
            }
        }
    }
}
