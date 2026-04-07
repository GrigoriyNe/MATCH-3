using UnityEngine;
using UnityEngine.SceneManagement;

namespace Navigator
{
    public class NavigatorWindow : MonoBehaviour
    {
        [SerializeField] private NavigatorButton[] _navigatorButtons;

        private void OnEnable() =>
            ChangeSubscribeToButtons(true);

        private void OnDisable() =>
            ChangeSubscribeToButtons(false);

        private void ChangeSubscribeToButtons(bool isSubscribe)
        {

            foreach (NavigatorButton button in _navigatorButtons)
            {
                if (isSubscribe)
                {
                    button.Activated += OnChangeScene;
                }
                else
                {
                    button.Activated -= OnChangeScene;
                }
            }
        }

        private void OnChangeScene(string nameNewScene) =>
            SceneManager.LoadScene(nameNewScene);
    }
}
