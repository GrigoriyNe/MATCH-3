using System;
using UnityEngine;
using DG.Tweening;

namespace GameLoop
{
    public class Ball : MonoBehaviour
    {
        private const int LoopsRotation = 1;

        [SerializeField] private float _durationAnimationMove = 0.4f;
        [SerializeField] private float _durationAnimationsSize = 0.8f;
        [SerializeField] private SpriteRenderer _renderer;

        private bool _isMoved = false;
        private float _defaultScale;
        private float _rotationAngle = 90f;

        private DG.Tweening.Sequence _tutorialSequence;
        public int ColorId { get; private set; }

        public event Action<Ball> OnBallPressed;

        private void Start()
        {
            _defaultScale = transform.localScale.x;
            ShowSpawnAnimation();
        }

        private void OnMouseDown()
        {
            if (_isMoved)
                return;

            OnBallPressed?.Invoke(this);
        }

        public void SetColorId(int id) =>
            ColorId = id;

        public void SetColor(Color color) =>
            _renderer.color = color;

        public void Explode() =>
            transform.DOScale(0, _durationAnimationMove)
                .OnComplete(Destroyed);

        public void ShowTutorial()
        {
            Vector3 rotateOnTutoial = new Vector3(0, _rotationAngle, 0);

            _tutorialSequence = DOTween.Sequence();
            _tutorialSequence.Append(transform.DORotate(rotateOnTutoial, LoopsRotation).SetLoops
                (int.MaxValue, LoopType.Yoyo));

            _tutorialSequence.Play();
        }

        public void MoveTo(Vector3 target)
        {
            _isMoved = true;
            
            transform.DOMove(target, _durationAnimationMove)
                .OnComplete(ReadyToClick);
        }

        private void ShowSpawnAnimation()
        {
            _isMoved = true;

            transform.DOScale(0, 0);
            transform.DOScale(_defaultScale, _durationAnimationsSize)
                .OnComplete(ReadyToClick);
        }

        private void Destroyed()
        {
            _tutorialSequence?.Kill();
            Destroy(gameObject);
        }

        private void ReadyToClick() =>
            _isMoved = false;
    }
}