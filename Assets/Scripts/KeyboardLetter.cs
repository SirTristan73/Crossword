using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardLetter : MonoBehaviour
{
    [SerializeField] private Button _letterButton;

    [SerializeField] private TMP_Text _text;

    [SerializeField] private char _char;



    private void OnEnable()
    {
        _letterButton.onClick.AddListener(OnKeyboardButtonPressed);
        
        _text.text = _char.ToString();
    }


    private void OnDisable()
    {
        _letterButton.onClick.RemoveListener(OnKeyboardButtonPressed);
    }


    private void OnKeyboardButtonPressed()
    {

    }

}
