using UnityEngine;
using UnityEngine.UI;



namespace EventBus
{
    public class StartLevel : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private int _thisLevelIndex;


        private void OnEnable()
        {
            _button.onClick.AddListener(StartLevelAction);
        }


        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }


        private void StartLevelAction()
        {
            EventBus.Trigger(new StartLevelEvent(_thisLevelIndex));
        }
    }
}