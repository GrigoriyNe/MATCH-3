using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    private const string GameScene = "Game"; 
    private const string Menu = "MainScreen"; 

    [SerializeField] private Button _offerExit;
    [SerializeField] private Button _exitAccept;
    [SerializeField] private Button _exitNotAccept;
    [SerializeField] private Image _back;

    private void OnEnable()
    {
        _offerExit.onClick.AddListener(OnClickOfferExit);
        _exitAccept.onClick.AddListener(OnClickExit);
        _exitNotAccept.onClick.AddListener(OnClickNoExit);
    }

    private void OnDisable()
    {
        _offerExit.onClick.RemoveListener(OnClickOfferExit);
        _exitAccept.onClick.RemoveListener(OnClickExit);
        _exitNotAccept.onClick.RemoveListener(OnClickNoExit);
    }

    private void OnClickOfferExit()
    {
        _back.gameObject.SetActive(true);
        _exitAccept.gameObject.SetActive(true);
        _exitNotAccept.gameObject.SetActive(true);
    }

    private void OnClickNoExit()
    {
        _back.gameObject.SetActive(false);
        _exitAccept.gameObject.SetActive(false);
        _exitNotAccept.gameObject.SetActive(false);
    }

    private void OnClickExit()
    {
        if (SceneManager.GetActiveScene().name != GameScene)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(Menu);
        }
    }
}
