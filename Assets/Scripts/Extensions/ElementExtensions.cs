using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public static class ElementExtensions
{
    public static UniTask ScaleChangeAnimation(this Transform transform)
    {
        var scale = transform.localScale;

        return DOTween.Sequence()
            .Append(transform.DOScale(scale * 0.8f, 0.07f))
            .Append(transform.DOScale(scale * 1.2f, 0.02f))
            .Append(transform.DOScale(scale, 0.03f))
            .SetEase(Ease.InOutQuad)
            .AsyncWaitForCompletion().AsUniTask();
    }
}