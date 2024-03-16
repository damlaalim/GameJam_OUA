using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NormalLaserMovement : MonoBehaviour
{
    public Transform MovePoint;

    private void Start()
    {
        transform.DOMove(MovePoint.position, 2f).SetLoops(-1, LoopType.Yoyo);
    }
    // Update is called once per frame
    void Update()
    {
        //transform.DOMove(MovePoint.position, 2f).SetLoops(-1, LoopType.Yoyo);
    }
}
