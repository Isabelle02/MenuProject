using UnityEngine;
using UnityEngine.UI;

public class StarElement : MonoBehaviour
{
    [SerializeField] private Image _starImage;
    [SerializeField] private Sprite _starSprite;
    [SerializeField] private Sprite _starEmptySprite;

    public void SetReceived(bool isReceived)
    {
        _starImage.sprite = isReceived ? _starSprite : _starEmptySprite;
    }
}