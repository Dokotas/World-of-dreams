using R3;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WorldOfDreams
{
    public class ResourceBarView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI label;

        private IDisposable _subscription;

        public void Bind(ReadOnlyReactiveProperty<ResourceData> stream)
        {
            _subscription?.Dispose();

            _subscription = stream.Subscribe(data =>
            {
                image.fillAmount = data.Current / data.Max;
                label.text = $"{Mathf.CeilToInt(data.Current)}/{Mathf.CeilToInt(data.Max)}";
            });
        }

        private void OnDestroy()
        {
            _subscription?.Dispose();
        }
    }
}