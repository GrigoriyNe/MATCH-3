using UnityEngine;

namespace GameLoop
{
    public class TutorialText : MonoBehaviour
    {
        [SerializeField] private GameLoop _game;
        private void OnEnable() =>
            _game.StepsChanged += OnChange;

        private void OnDisable() =>
            _game.StepsChanged -= OnChange;

        private void OnChange(int value) =>
            gameObject.SetActive(false);
    }
}
