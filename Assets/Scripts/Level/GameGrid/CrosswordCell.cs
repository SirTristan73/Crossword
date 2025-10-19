using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


namespace EventBus
{
    public class CrosswordCell : MonoBehaviour
    {
        public int _thisCellIndex { get; private set; }

        [SerializeField] private Button _button;

        [SerializeField] private TMP_Text _text;

        private CrosswordCellState _myState;
        
        [SerializeField] private Image _background;

        [Header("Sprites from Atlas")]
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private Sprite _correctSprite;
        [SerializeField] private Sprite _disabledSprite;





        public void Init(int index)
        {
            _thisCellIndex = index;

            _button.onClick.RemoveAllListeners();

            _button.onClick.AddListener(OnClick);
        }
        

        public void SetState(CrosswordCellState state)
        {
            switch (state)
            {
                case CrosswordCellState.Default:

                    _background.sprite = _defaultSprite;

                    _button.interactable = true;

                    break;

                case CrosswordCellState.Correct:
                    _background.sprite = _correctSprite;

                    _button.interactable = false;

                    break;

                case CrosswordCellState.Disabled:

                    _background.sprite = _disabledSprite;

                    _button.interactable = false;

                    break;
            }

            _myState = state;
        }
        

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }


        public void SetLetter(char c)
        {
            _text.text = c.ToString();
        }


        private void OnClick()
        {
            EventBus.Trigger(new CrosswordCellClick(_thisCellIndex));
        }
    }
}