using UnityEngine;


namespace EventBus      ///
                        ///  TODO
                        /// 
{
    public class StartLevelEvent : EventType
    {
        public int _levelIndex { get; private set; }



        public StartLevelEvent(int index)
        {
            _levelIndex = index;

            LoadingManager.Instance.LoadScene("GameScene");
        }
    }
}