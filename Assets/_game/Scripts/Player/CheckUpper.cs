using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUpper : MonoBehaviour
{
    private PlayerMovementController _playerMovementController;

    private void Start()
    {
        _playerMovementController = GetComponentInParent<PlayerMovementController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Ceil" && _playerMovementController.CrouchBool == true)
        {
            Debug.Log("Eðilmeli");
            _playerMovementController.NeedCrouch = true;
            _playerMovementController.gameObject.transform.localScale = new Vector3(1f, 0.5f, 1f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ceil")
        {
            _playerMovementController.NeedCrouch = false;
            _playerMovementController.CrouchBool = false;
            _playerMovementController.gameObject.transform.localScale = Vector3.one;
        }
    }
}
