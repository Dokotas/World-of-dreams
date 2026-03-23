using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace WorldOfDreams
{
    public sealed class AnimationUIHandler : MonoBehaviour
    {
        private AnimationControlService _animationService;

        [Header("UI References")]
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;

        [Inject]
        private void Construct(AnimationControlService animationService)
        {
            _animationService = animationService;
        }

        private void Start()
        {
            _pauseButton.onClick.AddListener(_animationService.Pause);
            _resumeButton.onClick.AddListener(_animationService.Resume);
        }

        private void OnDestroy()
        {
            if (_pauseButton != null) _pauseButton.onClick.RemoveListener(_animationService.Pause);
            if (_resumeButton != null) _resumeButton.onClick.RemoveListener(_animationService.Resume);
        }
    }
}