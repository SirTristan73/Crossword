using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private string _thisButtonKey;



    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartAction);
    }


    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartAction);
    }


    private void StartAction()
    {
        UIManager.Instance.PushOverlay(UIState.LevelSelection);
    }
}
