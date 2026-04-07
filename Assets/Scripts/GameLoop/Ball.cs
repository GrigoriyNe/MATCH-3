using System;
using UnityEngine;
using Unity.Burst;
using DG.Tweening;

namespace GameLoop
{
    [BurstCompile]
    public class Ball : MonoBehaviour
    {
        private const int LoopsRotation = 1;

        [SerializeField] private float _durationAnimationMove;
        [SerializeField] private float _durationAnimationsSize;
        [SerializeField] private SpriteRenderer _renderer;

        private bool _isMoved = false;
        private float _defaultScale;
        private float _rotationAngle = 90f;

        private DG.Tweening.Sequence _tutorialSequence;
        public int ColorId { get; private set; }

        public event Action<Ball> BallPressed;

        private void Start()
        {
            _defaultScale = transform.localScale.x;
            ShowSpawnAnimation();
        }

        private void OnMouseDown()
        {
            if (_isMoved)
                return;

            BallPressed?.Invoke(this);
        }

        public void SetColorId(int id) =>
            ColorId = id;

        public void SetColor(Color color) =>
            _renderer.color = color;

        public void Explode()
        {
            _isMoved = true;

            transform.DOScale(0, _durationAnimationMove)
                .OnComplete(Destroyed);
        }

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
            _isMoved = false;
            Destroy(gameObject);
        }

        private void ReadyToClick() =>
            _isMoved = false;
    }
}