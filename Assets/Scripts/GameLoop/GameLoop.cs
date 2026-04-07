using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;
using DG.Tweening;

namespace GameLoop
{
    [BurstCompile]
    public class GameLoop : MonoBehaviour
    {
        private const int MinMatches = 3;
        private const int StepOffset = 1;

        [SerializeField] private int _width = 8;
        [SerializeField] private int _height = 8;
        [SerializeField] private BallFabric _fabric;

        private Ball[,] _grid;
        private int _stepsLeft = 3;

        public event Action<int> StepsChanged;

        private void OnEnable() =>
            DOTween.Clear(true);

        private void Start()
        {
            InitializeGrid();
            ShowTutorial();
        }

        private void OnDestroy()
        {
            DOTween.Clear(true);

            foreach (Ball ball in _grid)
            {
                if (ball != null)
                {
                    ball.BallPressed -= OnBallClicked;
                }
            }
        }

        public void OnBallClicked(Ball clickedBall)
        {
            if (_stepsLeft <= 0)
            {
                return;
            }

            List<Ball> matches = FindMatches(clickedBall);

            if (matches.Count >= MinMatches)
            {
                ProcessMatches(matches);
            }
            else
            {
                _grid[GetGridX(clickedBall), GetGridY(clickedBall)] = null;
                clickedBall.Explode();
                _stepsLeft--;
            }

            ShiftBallsDown();
            StepsChanged?.Invoke(_stepsLeft);
        }

        private void InitializeGrid()
        {
            _grid = new Ball[_width, _height];

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    SpawnBall(x, y);
                }
            }
        }

        private void SpawnBall(int x, int y)
        {
            Ball ball = _fabric.Create();
            ball.transform.position = new Vector3(x, y, 0);
            _grid[x, y] = ball;

            ball.BallPressed += OnBallClicked;
        }

        private List<Ball> FindMatches(Ball startBall)
        {
            List<Ball> matches = new List<Ball>();
            Queue<Ball> queueBall = new Queue<Ball>();
            HashSet<Ball> visitedBall = new HashSet<Ball>();

            queueBall.Enqueue(startBall);
            visitedBall.Add(startBall);

            while (queueBall.Count > 0)
            {
                Ball current = queueBall.Dequeue();
                matches.Add(current);

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }

                        int newX = GetGridX(current) + x;
                        int newY = GetGridY(current) + y;

                        if (IsValidPosition(newX, newY))
                        {
                            Ball neighbor = _grid[newX, newY];

                            if (neighbor != null && !visitedBall.Contains(neighbor) &&
                                neighbor.ColorId == startBall.ColorId)
                            {
                                queueBall.Enqueue(neighbor);
                                visitedBall.Add(neighbor);
                            }
                        }
                    }
                }
            }

            return matches;
        }

        private int GetGridX(Ball ball)
        {
            Vector3 pos = ball.transform.position;

            return Mathf.RoundToInt(pos.x);
        }

        private int GetGridY(Ball ball)
        {
            Vector3 pos = ball.transform.position;

            return Mathf.RoundToInt(pos.y);
        }

        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < _width && y >= 0 && y < _height && _grid[x, y] != null;
        }

        private void ProcessMatches(List<Ball> matches)
        {
            _stepsLeft += matches.Count - StepOffset;

            foreach (Ball ball in matches)
            {
                _grid[GetGridX(ball), GetGridY(ball)] = null;
                ball.Explode();
            }
        }

        private void ShiftBallsDown()
        {
            List<int> coloumWithoutBall = new List<int>();

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (_grid[i, j] == null)
                    {
                        coloumWithoutBall.Add(i);
                    }
                }
            }

            foreach (int item in coloumWithoutBall)
            {
                List<Ball> columnBalls = new List<Ball>();

                for (int i = 0; i < _height; i++)
                {
                    for (int y = 0; y < _height; y++)
                    {
                        if (_grid[item, y] != null)
                        {
                            columnBalls.Add(_grid[item, y]);
                            _grid[item, y] = null;
                        }
                    }
                }

                int newY = 0;

                foreach (Ball ball in columnBalls)
                {
                    Vector3 newPosition = new Vector3(item, newY, 0);
                    ball.MoveTo(newPosition);

                    _grid[item, newY] = ball;
                    newY++;
                }

                for (int y = newY; y < _height; y++)
                {
                    SpawnBall(item, y);
                }
            }
        }

        private void ShowTutorial()
        {
            for (int x = 0; x < _height; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (FindMatches(_grid[x, y]).Count > MinMatches)
                    {
                        _grid[x, y].ShowTutorial();

                        return;
                    }
                }
            }
        }
    }
}