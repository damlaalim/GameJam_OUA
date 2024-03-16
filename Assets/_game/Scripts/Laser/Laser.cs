using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _laserLenght;
    // Update is called once per frame
    void Update()
    {
        // Raycast i�in ba�lang�� noktas� olarak belirledi�imiz objenin pozisyon ve rotasyonu �zerinden bir ���n olu�tur
        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);

        // Sonsuz mesafeye kadar ���n g�nder
        float rayDistance = _laserLenght;

        // I��n� g�rselle�tirme (iste�e ba�l�)
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.black);

        // Raycast ile i�lem yap
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.GetComponent<PlayerMovementController>() != null)
                Debug.Log("Playera �arpt�");
            else
                Debug.Log("I��n bir nesneye �arpt�: " + hit.collider.gameObject.name);
        }
    }
}
