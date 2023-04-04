using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : SoundButton
{
    [SerializeField] private GameObject _locked;
    [SerializeField] private GameObject _unlocked;
    [SerializeField] private GameObject _passed;
    [SerializeField] private GameObject _comingSoon;
    [SerializeField] private Text _levelNumberText;
    [SerializeField] private HorizontalLayoutGroup _starsLayout;

    private readonly List<StarElement> _stars = new();

    private Dictionary<LevelState, GameObject> _states;

    private bool _isComingSoon;

    public event Action Clicked;
    
    public int LevelNumber { get; private set; }

    private bool IsComingSoon
    {
        get => _isComingSoon;
        set
        {
            _isComingSoon = value;
            _comingSoon.SetActive(value);
           
            foreach (var s in _states)
                s.Value.SetActive(!value);

            _levelNumberText.gameObject.SetActive(!value);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        
        _states = new Dictionary<LevelState, GameObject>
        {
            [LevelState.Locked] = _locked,
            [LevelState.Unlocked] = _unlocked, 
            [LevelState.Passed] = _passed
        };
        
        onClick.AddListener(OnClick);
    }

    public void Init(int levelNumber)
    {
        IsComingSoon = LevelManager.LevelsCount < levelNumber;

        LevelNumber = levelNumber;
        _levelNumberText.text = LevelNumber.ToString();
    }

    private void OnClick()
    {
        if (IsComingSoon)
            return;
        
        LevelManager.CurrentLevelIndex = LevelNumber - 1;
        
        Clicked?.Invoke();
    }

    public void SetState(LevelState state)
    {
        if (IsComingSoon)
            return;
        
        foreach (var s in _states)
            s.Value.SetActive(s.Key == state);

        _levelNumberText.gameObject.SetActive(state is LevelState.Unlocked or LevelState.Passed);
        
        if (LevelManager.PassedLevelsCount < LevelNumber)
            return;

        foreach (var star in _stars) 
            Pool.Release(star);

        _stars.Clear();

        for (var i = 0; i < 3; i++)
        {
            var star = Pool.Get<StarElement>(_starsLayout.transform);
            star.SetReceived(i < LevelManager.Rewards[LevelNumber - 1]);
            _stars.Add(star);
        }
    }
}

public enum LevelState
{
    Locked,
    Unlocked,
    Passed
}