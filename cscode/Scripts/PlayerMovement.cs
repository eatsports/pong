using UnityEngine;

namespace Pong
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private int speed;
        [SerializeField] private bool isPlayer1;
        private Rigidbody2D _rb;
        private float _timer;
        public Boundary boundary;

        private void Start()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float verticalMovement;
            if (isPlayer1)
            {
                verticalMovement = Input.GetAxisRaw("Vertical");
            }
            else
            {
                verticalMovement = Input.GetAxisRaw("Vertical1");
            }

            var movement = new Vector2(0f, verticalMovement);
            _rb.velocity = movement * speed;
            var position = _rb.position;
            _rb.position = new Vector2(position.x, Mathf.Clamp(position.y, boundary.yMin, boundary.yMax));
        }
    }
}