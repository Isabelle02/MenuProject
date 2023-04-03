using UnityEngine;
using UnityEngine.UI;

public class OptionsWindow : Window
{
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _sounndToggle;
    [SerializeField] private Button _closeButton;
    
    public override void OnOpen(ViewParam viewParam)
    {
        _musicToggle.isOn = !SoundManager.IsMusicOn;
        _sounndToggle.isOn = !SoundManager.IsSoundOn;
        
        _closeButton.onClick.AddListener(OnCLoseButtonClick);
        _musicToggle.onValueChanged.AddListener(OnMusicValueChanged);
        _sounndToggle.onValueChanged.AddListener(OnSoundValueChanged);
    }

    private void OnMusicValueChanged(bool isOn)
    {
        SoundManager.IsMusicOn = !isOn;
    }

    private void OnSoundValueChanged(bool isOn)
    {
        SoundManager.IsSoundOn = !isOn;
    }

    private void OnCLoseButtonClick()
    {
        WindowManager.Open<MainMenuWindow>();
    }

    public override void OnClose()
    {
        _closeButton.onClick.RemoveListener(OnCLoseButtonClick);
    }
}