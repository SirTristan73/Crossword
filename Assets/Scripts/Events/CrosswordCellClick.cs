using UnityEngine;


namespace EventBus
{
    public class CrosswordCellClick : EventType
    {
        public int _clickedCellIndex { get; private set; }


        public CrosswordCellClick(int index)
        {
            _clickedCellIndex = index;    
        }
    }
}