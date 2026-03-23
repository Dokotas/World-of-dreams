using System.Collections.Generic;
using UnityEngine;

namespace WorldOfDreams
{
    public sealed class AnimationControlService
    {
        private readonly List<Animator> _animators = new List<Animator>();
        private readonly Dictionary<Animator, float> _originalSpeeds = new Dictionary<Animator, float>();
        private bool _isPaused = false;

        public void Register(Animator animator)
        {
            if (!_animators.Contains(animator))
            {
                _animators.Add(animator);
                _originalSpeeds[animator] = animator.speed;
            }
        }

        public void Pause()
        {
            if (_isPaused) return;

            foreach (var animator in _animators)
            {
                if (animator != null)
                {
                    _originalSpeeds[animator] = animator.speed;
                    animator.speed = 0f;
                }
            }
            _isPaused = true;
        }

        public void Resume()
        {
            if (!_isPaused) return;

            foreach (var animator in _animators)
            {
                if (animator != null && _originalSpeeds.TryGetValue(animator, out float speed))
                {
                    animator.speed = speed;
                }
            }
            _isPaused = false;
        }
    }
}
