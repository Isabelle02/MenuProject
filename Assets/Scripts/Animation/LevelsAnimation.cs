using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelsAnimation : MonoBehaviour
{
    [SerializeField] private Collider2D _levels;
    [SerializeField] private Collider2D[] _clouds;
    [SerializeField] private bool _isGoingToUpLevels;

    private readonly Dictionary<Collider2D, Vector3> _savedCloudPositions = new();

    public async UniTask Play(Action infoUpdate)
    {
        var mainDirection = _isGoingToUpLevels ? -1 : 1;
        
        var startLevelsPos = _levels.transform.localPosition;

        var levelsSize = Camera.main.WorldToScreenPoint(_levels.bounds.size);
        var s = DOTween.Sequence()
            .Append(_levels.transform.DOLocalMoveY(startLevelsPos.y + mainDirection * levelsSize.y, 1f));

        foreach (var cloud in _clouds)
        {
            var startCloudPos = cloud.transform.localPosition;
            _savedCloudPositions.Add(cloud, startCloudPos);
            var cloudSize = Camera.main.WorldToScreenPoint(cloud.bounds.size);
            s.Join(cloud.transform.DOLocalMoveY(startCloudPos.y + mainDirection * cloudSize.y, 1f));
        }

        s.AppendCallback(() =>
        {
            infoUpdate?.Invoke();
            _levels.transform.SetLocalPosition(startLevelsPos);
        });

        var directions = new [] {-1, 1};
        foreach (var cloud in _clouds)
        {
            var direction = directions[Random.Range(0, 2)];
            s.Insert(1f, cloud.transform.DOLocalMoveX(cloud.transform.localPosition.x + direction * Screen.width * 2, 1f))
                .Insert(2f, cloud.transform.DOLocalMove(_savedCloudPositions[cloud], 1f));
        }

        await s.AsyncWaitForCompletion();
        _savedCloudPositions.Clear();
    }
}