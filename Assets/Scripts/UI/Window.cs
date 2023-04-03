using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Element;
using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [SerializeField] public bool _isAnimated = true;
    
    [ConditionalHide("_isAnimated", true)]
    [SerializeField] private Transform _animationPanel;
    
    [ConditionalHide("_isAnimated", true)]
    [SerializeField] private FadeGroup _fadeGroup;
    
    public bool IsActive => gameObject.activeSelf;
    
    public abstract void OnOpen(ViewParam viewParam);
    public abstract void OnClose();

    public async UniTask Open(ViewParam viewParam)
    {
        gameObject.SetActive(true);

        if (_isAnimated)
        {
            await PlayOpenAnimation();
        }
        
        OnOpen(viewParam);
    }

    private async UniTask PlayOpenAnimation()
    {
        _animationPanel.localScale = Vector3.zero;
        _fadeGroup.SetAlpha(0f);

        _fadeGroup.Fade(1f, 0.3f);
        await _animationPanel.DOScale(Vector3.one, 0.2f).AsyncWaitForCompletion();
        await _animationPanel.ScaleChangeAnimation();
    }

    private async UniTask PlayCloseAnimation()
    {
        _fadeGroup.Fade(0f, 0.3f);
        await _animationPanel.ScaleChangeAnimation();
        await _animationPanel.DOScale(Vector3.zero, 0.2f).AsyncWaitForCompletion();
    }
    
    public async UniTask Close()
    {
        if (_isAnimated)
        {
            await PlayCloseAnimation();
        }

        gameObject.SetActive(false);
        OnClose();
    }
    
    public abstract class ViewParam
    {
    }
}
