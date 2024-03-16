using DG.Tweening;
using UnityEngine;

namespace _game.Scripts.PuzzleElements
{
    public class OrbController : MonoBehaviour
    {
        [SerializeField] private float _maxScale, _minScale, _halfScale, _scaleGrowthTime, _scaleReductionTime, _scaleLoopTime, _waitingTime;
        [SerializeField] private Ease _maxScaleEase;
        
        private void Start()
        {
            transform.localScale = Vector3.one * _minScale;

            var seq = DOTween.Sequence();
             
            seq.Append(transform.DOScale(_maxScale * Vector3.one, _scaleGrowthTime));
            seq.Append(transform.DOScale(_minScale * Vector3.one, _scaleReductionTime).SetEase(_maxScaleEase));
            seq.Append(transform.DOScale(_halfScale * Vector3.one, _scaleLoopTime).SetLoops(3, LoopType.Yoyo));
            seq.Append(transform.DOScale(_minScale * Vector3.one, _scaleReductionTime).SetEase(_maxScaleEase));

            seq.SetLoops(-1);
        }

    }
}