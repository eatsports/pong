using UnityEngine;

namespace Pong
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private AudioClip[] ballSounds;

        private Rigidbody2D _rb;
        private AudioSource _audio;

        private bool _inTrigger;
        private bool _topTrigger;
        private bool _bottomTrigger;

        // Start is called before the first frame update
        private void Start()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _audio = gameObject.GetComponent<AudioSource>();
            var movementDirection = new Vector2(Random.Range(0, 2) * 2 - 1, Random.Range(-1, 2));
            _rb.velocity = movementDirection * speed;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("VWall"))
            {
                var velocity = _rb.velocity;
                velocity = new Vector2(velocity.x, -velocity.y);
                _rb.velocity = velocity;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("LWall") || other.CompareTag("RWall"))
            {
                _audio.PlayOneShot(ballSounds[(int) BallAudio.Score]);
                if (other.CompareTag("LWall"))
                {
                    GameManager.Instance.AddScore((int) Players.Player2); 
                }

                if (other.CompareTag("RWall"))
                {
                    GameManager.Instance.AddScore((int) Players.Player1);
                }

                gameObject.GetComponent<Renderer>().enabled = false;
                Destroy(gameObject, 2);
            }
            else
            {
                _audio.PlayOneShot(ballSounds[(int) BallAudio.Bounce]);
                if (other.CompareTag("VWall"))
                {
                    var velocity = _rb.velocity;
                    velocity = new Vector2(velocity.x, -velocity.y);
                    _rb.velocity = velocity;
                }

                if (other.CompareTag("Bat"))
                {
                    var partHit = GameManager.Instance.GetHitPosition(gameObject, other.gameObject);
                    switch (partHit)
                    {
                        case HitPoint.Top:
                        {
                            var direction = _rb.velocity / speed;
                            speed += 0.5f;
                            var movement = new Vector2(-direction.x, 1) * speed;
                            _rb.velocity = movement;
                            break;
                        }
                        case HitPoint.Bottom:
                        {
                            var direction = _rb.velocity / speed;
                            speed += 0.5f;
                            var movement = new Vector2(-direction.x, -1) * speed;
                            _rb.velocity = movement;
                            break;
                        }
                        case HitPoint.Middle:
                        {
                            var direction = _rb.velocity / speed;
                            speed += 0.5f;
                            var movement = new Vector2(-direction.x, 0) * speed;
                            _rb.velocity = movement;
                            break;
                        }
                    }
                }
            }
        }
    }
}