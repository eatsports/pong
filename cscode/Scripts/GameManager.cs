using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pong
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameObject ball;
        [SerializeField] private TextMeshProUGUI player1ScoreText;
        [SerializeField] private TextMeshProUGUI player2ScoreText;
        private int _player1Score;
        private int _player2Score;
        private const int MaxScore = 5;

        private void Start()
        {
            _player1Score = 0;
            _player2Score = 0;

            player1ScoreText.text = _player1Score.ToString();
            player2ScoreText.text = _player2Score.ToString();
            SpawnBall();
        }

        public void SpawnBall()
        {
            Instantiate(ball);
        }

        public void AddScore(int playerId)
        {
            if (playerId == (int) Players.Player1)
            {
                _player1Score++;
                player1ScoreText.text = _player1Score.ToString();
            }
            else
            {
                _player2Score++;
                player2ScoreText.text = _player2Score.ToString();
            }

            if (_player1Score == MaxScore || _player2Score == MaxScore)
            {
                SceneManager.LoadScene(2);
            }

            Invoke("SpawnBall", 2f );
        }

        public HitPoint GetHitPosition(GameObject activeBall, GameObject bat)
        {
            var ballPosition = activeBall.transform.position.y;
            var batPosition = bat.transform.position.y;

            if (ballPosition > batPosition)
            {
                return HitPoint.Top;
            }

            if (ballPosition < batPosition)
            {
                return HitPoint.Bottom;
            }

            return HitPoint.Middle;
        }
    }
}