using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Element
{
    public class FadeGroup : MonoBehaviour
    {
        [SerializeField] private Image[] _images;
        [SerializeField] private Text[] _texts;
        [SerializeField] private FadeGroup[] _fadeGroups;

        public void SetAlpha(float alpha)
        {
            foreach (var image in _images)
            {
                image.SetAlpha(alpha);
            }

            foreach (var text in _texts)
            {
                text.SetAlpha(alpha);
            }

            foreach (var fadeGroup in _fadeGroups)
            {
                fadeGroup.SetAlpha(alpha);
            }
        }

        public async UniTask Fade(float alpha, float duration)
        {
            var tasks = new List<UniTask>();
            
            foreach (var image in _images)
            {
                tasks.Add(DOTween.To(() => image.color.a, x => image.SetAlpha(x), alpha, duration).AsyncWaitForCompletion().AsUniTask());
            }

            foreach (var text in _texts)
            {
                tasks.Add(DOTween.To(() => text.color.a, x => text.SetAlpha(x), alpha, duration).AsyncWaitForCompletion().AsUniTask());
            }

            foreach (var fadeGroup in _fadeGroups)
            {
                tasks.Add(Fade(alpha, duration));
            }

            await UniTask.WhenAll(tasks);
        }

        [ContextMenu("Rebuild")]
        public void Rebuild()
        {
            var images = new List<Image>();
            images.AddRange(gameObject.GetComponents<Image>());
            images.AddRange(gameObject.GetComponentsInChildren<Image>());
            
            var texts = new List<Text>();
            texts.AddRange(gameObject.GetComponents<Text>());
            texts.AddRange(gameObject.GetComponentsInChildren<Text>());

            _images = images.ToArray();
            _texts = texts.ToArray();
        }
    }
}