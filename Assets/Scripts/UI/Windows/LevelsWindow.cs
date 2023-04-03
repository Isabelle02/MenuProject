using UnityEngine;
using UnityEngine.UI;

public class LevelsWindow : Window
{
    [SerializeField] private Button _homeButton;
    [SerializeField] private LevelButton[] _levelButtons;

    private int _lastLevelInScreen;

    public override void OnOpen(ViewParam viewParam)
    {
        _homeButton.onClick.AddListener(OnHomeButtonClick);
        
        var currentLevel = LevelManager.CurrentLevelIndex;

        var startLevel = currentLevel % 9 + 9 * currentLevel / 9;
        _lastLevelInScreen = startLevel + 9;

        for (var i = startLevel; i < startLevel + 9; i++)
        {
            _levelButtons[i].Init(i + 1);
            _levelButtons[i].Clicked += OnLevelClicked;
        }
        
        UpdateButtons();
    }

    private void OnHomeButtonClick()
    {
        WindowManager.Open<MainMenuWindow>();
    }

    private void OnLevelClicked()
    {
        LevelManager.CompleteLevel();
        
        UpdateButtons();

        if (LevelManager.PassedLevelsCount - 1 == _lastLevelInScreen)
        {
            _lastLevelInScreen += 9;

            foreach (var button in _levelButtons)
                button.Init(button.LevelNumber + 9);
            
            UpdateButtons();
        }
    }

    private void UpdateButtons()
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

    public override void OnClose()
    {
        _homeButton.onClick.RemoveListener(OnHomeButtonClick);
        
        foreach (var button in _levelButtons)
        {
            button.Clicked -= OnLevelClicked;
        }
    }
}