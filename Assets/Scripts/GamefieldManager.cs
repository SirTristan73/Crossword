using UnityEngine;



namespace EventBus
{
    public class GamefieldManager : MonoBehaviour
    {
        [SerializeField] private GameObject _gamefield;
        [SerializeField] private GameTileSO _tile;




        private void OnEnable()
        {
            EventBus.SubscribeToEvent<OnGametileCliick>(SetClickedCell);
        }


        private void OnDisable()
        {
            EventBus.UnsubscribeFromEvent<OnGametileCliick>(SetClickedCell);            
        }


        private void SetClickedCell(OnGametileCliick eventData)
        {
            Debug.Log(eventData._cellIndex);
        }
    }
}