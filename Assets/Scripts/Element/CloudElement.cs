using DG.Tweening;
using UnityEngine;

public class CloudElement : MonoBehaviour
{
    [SerializeField, Range(1f, 5f)] private float _distanceX;
    [SerializeField] private float _duration = 3f;

    private Tween _soaringAnim;

    private void OnEnable()
    {
        var directions = new[] {-1, 1};
        var direction = directions[Random.Range(0, 2)];

        var currentPosX = transform.localPosition.x;

        _soaringAnim = DOTween.Sequence()
            .Append(transform.DOLocalMoveX(currentPosX + direction * _distanceX, _duration))
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        _soaringAnim?.Kill();
    }
}