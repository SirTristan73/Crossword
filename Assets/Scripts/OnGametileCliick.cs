using UnityEngine;



namespace EventBus
{
    public class OnGametileCliick : EventType
    {
        public int _cellIndex { get; private set; }

        public OnGametileCliick(int index)
        {
            _cellIndex = index;
        }
    }
}