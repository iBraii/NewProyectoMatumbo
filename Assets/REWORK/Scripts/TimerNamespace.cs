using UnityEngine;

namespace Utilities
{
    public class OurTimer
    {
        public static float timer;
        public static bool TimerCount(float maxTime)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime) { timer = 0; return true; } 
            else return false;
        }
    }
}
