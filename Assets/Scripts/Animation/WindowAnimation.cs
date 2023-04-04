using Cysharp.Threading.Tasks;
using DG.Tweening;
using Element;
using UnityEngine;

public class WindowAnimation : BaseAnimation
{
    [SerializeField] private FadeGroup _fadeGroup;
    [SerializeField] private Transform _scaleContainer;
    
    public override void Show()
    {
        _scaleContainer.localScale = Vector3.one;
        _fadeGroup.SetAlpha(1f);
    }

    public override async UniTask DoShow()
    {
        _fadeGroup.Fade(1f, 0.3f);
        await _scaleContainer.DOScale(Vector3.one, 0.2f).AsyncWaitForCompletion();
        await _scaleContainer.ScaleChangeAnimation();
    }

    public override void Hide()
    {
        _scaleContainer.localScale = Vector3.zero;
        _fadeGroup.SetAlpha(0f);
    }

    public override async UniTask DoHide()
    {
        _fadeGroup.Fade(0f, 0.3f);
        await _scaleContainer.ScaleChangeAnimation();
        await _scaleContainer.DOScale(Vector3.zero, 0.2f).AsyncWaitForCompletion();
    }
}