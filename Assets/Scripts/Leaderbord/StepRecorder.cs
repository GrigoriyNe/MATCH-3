using System;
using UnityEngine;
using GameLoop;

namespace Leaderboard
{
    public class StepRecorder : MonoBehaviour
    {
        private const string Current = "current";

        [SerializeField] private CSVWriter _csvWriter;
        [SerializeField] private GameLoop.GameLoop _game;
        [SerializeField] private StepsView _stepView;
        [SerializeField] private StepsView _pointsView;

        private int _currentStep = 0;
        private int _lastRecord;

        public int CurrentStep => _currentStep;

        private void Awake()
        {
            _game.StepsChanged += ChangeValue;
            _lastRecord = Int32.Parse(PlayerPrefs.GetString(Current));
        }

        private void OnDestroy()
        {
            _game.StepsChanged -= ChangeValue;
        }

        public void ChangeValue(int value)
        {
            _stepView.ChangeValue(value);

            if (_currentStep < value)
            {
                _currentStep = value;
                _pointsView.ChangeValue(_currentStep);

                if (_currentStep < _lastRecord)
                {
                    return;
                }

                _csvWriter.SetNewRecord(value);
            }
        }
    }
}