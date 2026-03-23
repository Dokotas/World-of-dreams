using UnityEngine;
using VContainer;

namespace WorldOfDreams
{
    public sealed class PlayerAnimatorRegistrator : MonoBehaviour
    {
        private AnimationControlService _animationService;

        [Inject]
        private void Construct(AnimationControlService animationService)
        {
            _animationService = animationService;
        }

        private void Start()
        {
            var animators = GetComponentsInChildren<Animator>(true);

            foreach (var animator in animators)
            {
                _animationService.Register(animator);
            }
        }
    }
}