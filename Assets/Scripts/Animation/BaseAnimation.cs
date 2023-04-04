using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseAnimation : MonoBehaviour
{
    public abstract void Show();
    public abstract UniTask DoShow();
    public abstract void Hide();
    public abstract UniTask DoHide();
}