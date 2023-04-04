using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundToggle : Toggle
{
    [SerializeField] private Sound _clickSound;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        SoundManager.PlayOneShot(_clickSound);
    }
}