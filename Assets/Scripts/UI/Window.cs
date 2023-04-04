using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [SerializeField] public bool _isAnimated = true;

    [ConditionalHide("_isAnimated", true)] 
    [SerializeField] private BaseAnimation _animation;
    
    public bool IsActive => gameObject.activeSelf;

    protected abstract void OnOpen(ViewParam viewParam);
    protected abstract void OnClose();

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
        _animation.Hide();
        await _animation.DoShow();
    }

    private async UniTask PlayCloseAnimation()
    {
        _animation.DoHide();
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
