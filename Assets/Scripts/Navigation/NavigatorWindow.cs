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
                    button.OnActivate += OnChangeScene;
                }
                else
                {
                    button.OnActivate -= OnChangeScene;
                }
            }
        }

        private void OnChangeScene(string nameNewScene) =>
            SceneManager.LoadScene(nameNewScene);
    }
}
