using UnityEngine;
using UnityEngine.UI;

public class LevelsWindow : Window
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private LevelButton[] _levelButtons;
    [SerializeField] private Button _upperButton;
    [SerializeField] private Button _bottomButton;
    [SerializeField] private LevelsAnimation _upLevelsAnimation;
    [SerializeField] private LevelsAnimation _downLevelsAnimation;
    [SerializeField] private GameObject[] _controlElements;

    private int _lastLevelInScreen;

    protected override void OnOpen(ViewParam viewParam)
    {
        _homeButton.onClick.AddListener(OnHomeButtonClick);
        _upperButton.onClick.AddListener(OnUpperButtonClick);
        _bottomButton.onClick.AddListener(OnBottomButtonClick);
        
        var currentLevel = LevelManager.PassedLevelsCount + 1;

        var startLevel = 8 * Mathf.FloorToInt(currentLevel / 9f) + 1;
        _lastLevelInScreen = startLevel + 8;

        for (var i = startLevel; i <= _lastLevelInScreen; i++)
        {
            _levelButtons[i - startLevel].Init(i);
            _levelButtons[i - startLevel].Clicked += OnLevelClicked;
        }
        
        UpdateLevelButtons();
        
        UpdateLevelsControlButtons();
    }

    private async void OnUpperButtonClick()
    {
        foreach (var controlElement in _controlElements) 
            controlElement.SetActive(false);
        
        await _upLevelsAnimation.Play(UpdateInfo);
        
        foreach (var controlElement in _controlElements) 
            controlElement.SetActive(true);
        
        UpdateLevelsControlButtons();

        void UpdateInfo()
        {
            _lastLevelInScreen += 8;

            foreach (var button in _levelButtons)
                button.Init(button.LevelNumber + 8);
            
            UpdateLevelButtons();
        }
    }

    private async void OnBottomButtonClick()
    {
        foreach (var controlElement in _controlElements) 
            controlElement.SetActive(false);
        
        await _downLevelsAnimation.Play(UpdateInfo);
        
        foreach (var controlElement in _controlElements) 
            controlElement.SetActive(true);
        
        UpdateLevelsControlButtons();

        void UpdateInfo()
        {
            _lastLevelInScreen -= 8;

            foreach (var button in _levelButtons)
                button.Init(button.LevelNumber - 8);
            
            UpdateLevelButtons();
        }
    }

    private void OnHomeButtonClick()
    {
        WindowManager.Open<MainMenuWindow>();
    }

    private void OnLevelClicked()
    {
        LevelManager.CompleteLevel();
        
        UpdateLevelButtons();
        
        UpdateLevelsControlButtons();
    }

    private void UpdateLevelButtons()
    {
        foreach (var button in _levelButtons)
        {
            if (button.LevelNumber == LevelManager.PassedLevelsCount + 1)
                button.SetState(LevelState.Unlocked);
            
            if (button.LevelNumber <= LevelManager.PassedLevelsCount)
                button.SetState(LevelState.Passed);
            
            if (button.LevelNumber > LevelManager.PassedLevelsCount + 1)
                button.SetState(LevelState.Locked);
        }
    }

    private void UpdateLevelsControlButtons()
    {
        _upperButton.gameObject.SetActive(LevelManager.PassedLevelsCount >= _lastLevelInScreen && _lastLevelInScreen < LevelManager.LevelsCount);
        _bottomButton.gameObject.SetActive(_lastLevelInScreen > 9);
    }

    protected override void OnClose()
    {
        _homeButton.onClick.RemoveListener(OnHomeButtonClick);
        _upperButton.onClick.RemoveListener(OnUpperButtonClick);
        _bottomButton.onClick.RemoveListener(OnBottomButtonClick);
        
        foreach (var button in _levelButtons)
        {
            button.Clicked -= OnLevelClicked;
        }
    }
}