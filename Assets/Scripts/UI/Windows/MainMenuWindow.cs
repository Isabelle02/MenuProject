using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : Window
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;

    public override void OnOpen(ViewParam viewParam)
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _optionsButton.onClick.AddListener(OnOptionsButtonCLick);
    }

    private void OnPlayButtonClick()
    {
        WindowManager.Open<LevelsWindow>();
    }

    private void OnOptionsButtonCLick()
    {
        WindowManager.Open<OptionsWindow>();
    }

    public override void OnClose()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _optionsButton.onClick.RemoveListener(OnOptionsButtonCLick);
    }
}