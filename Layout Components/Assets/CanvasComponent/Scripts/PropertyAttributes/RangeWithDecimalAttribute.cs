using System;
using UnityEngine;

namespace Core.Scripts.UI
{
    public class RangeWithDecimalAttribute : PropertyAttribute
    {
        public float Min;
        public float Max;
        public int Digit;

        public RangeWithDecimalAttribute(float min, float max,int digit)
        {
            this.Min = min;
            this.Max = max;
            this.Digit = digit;

        }
    }
}