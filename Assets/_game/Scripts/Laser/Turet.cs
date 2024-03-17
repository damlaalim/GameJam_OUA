using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Turet : MonoBehaviour
{
    public bool CanLook;
    public Transform Player;
    FieldOfView _fieldOfView;
    [SerializeField] private float _followSpeed;
    private void Start()
    {
        _fieldOfView = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanLook && _fieldOfView.canSeePlayer)
        {
            Vector3 playerdirecion = Player.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(playerdirecion);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _followSpeed * Time.deltaTime);
        }
        if (!_fieldOfView.canSeePlayer)
        {
            Quaternion target = Quaternion.Euler(Vector3.zero);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, 2f * Time.deltaTime);
        }
    }
}
