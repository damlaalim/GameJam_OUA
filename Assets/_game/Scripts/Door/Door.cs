using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Door : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _interactiveText;
    public float Rotation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _interactiveText.gameObject.SetActive(true);
            other.GetComponent<PlayerMovementController>().CanInteractive = true;
            other.GetComponent<PlayerMovementController>().door = this.gameObject;
            other.GetComponent<PlayerMovementController>().DoorRotation = Rotation;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _interactiveText.gameObject.SetActive(false);
            other.GetComponent<PlayerMovementController>().CanInteractive = false;
        }
    }
}
