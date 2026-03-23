using System;
using UnityEngine;

namespace WorldOfDreams
{
    [Serializable]
    public struct ResourceData
    {
        public float Current;
        public float Max;

        public ResourceData(float current, float max)
        {
            Current = Mathf.Clamp(current, 0, max);
            Max = max;
        }
    }
}