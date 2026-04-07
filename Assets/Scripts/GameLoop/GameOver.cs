using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameLoop
{
    public class GameOver : MonoBehaviour
    {
        private const string Records = "Records";
        private const string Menu = "MainScreen";
        private const string Current = "current";
        private const string IsRecord = "isRecord";
        private const int True = 1;


        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private Button _okButton;
        [SerializeField] private GameLoop _game;
        [SerializeField] private Navigator.ExitGame _exit;
        [SerializeField] private Leaderboard.StepRecorder _record;

        private int _lastRecord;

        private void Awake()
        {
            PlayerPrefs.SetInt("iSRecord", 0);
            _lastRecord = Int32.Parse(PlayerPrefs.GetString(Current));
            _game.StepsChanged += OnMoves;
            _okButton.onClick.AddListener(OpenMenu);
        }

        private void OnDisable()
        {
            _game.StepsChanged -= OnMoves;
            _okButton.onClick.AddListener(OpenMenu);
        }
        private void OpenMenu() =>
            SceneManager.LoadScene(Menu);

        private void OnMoves(int value)
        {
            if (value == 0)
            {
                if (_record.CurrentStep > _lastRecord)
                {
                    PlayerPrefs.SetInt(IsRecord, True);
                    SceneManager.LoadScene(Records);
                }
                else
                {
                    PlayerPrefs.SetInt(IsRecord, 0);
                    _gameOverPanel.gameObject.SetActive(true);
                }
            }
        }
    }
}