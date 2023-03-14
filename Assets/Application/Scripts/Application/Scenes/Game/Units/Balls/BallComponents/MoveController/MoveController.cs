using Application.Scripts.Library.Extensions.Vector;
using Application.Scripts.Library.Reusable;
using Application.Scripts.Library.TimeManagers;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Balls.BallComponents.MoveController
{
    public class MoveController : MonoBehaviour, IReusable
    {
        private Vector2 _previousDirection;
        private Vector2 _currentDirection;

        [SerializeField] private Rigidbody2D rigidbody2d;
        [SerializeField] private TimeManager ballTimeManager;
        [SerializeField] private float speed;
        [SerializeField] private float minBounceAngel;
        
        public bool PhysicActive { get => !rigidbody2d.isKinematic; set => rigidbody2d.isKinematic = !value; }
        public Vector2 CurrentDirection => _currentDirection.normalized;
        public Vector2 PreviousDirection => _previousDirection;


        public void PrepareReuse()
        {
            _currentDirection = default;
            _previousDirection = default;
            PhysicActive = false;
        }
        
        public void SetDirection(Vector2 direction)
        {
            direction = direction.normalized;
            rigidbody2d.velocity = direction * speed;
            _currentDirection = direction;
        }

        private void FixedUpdate()
        {
            if (PhysicActive)
            {
                if (rigidbody2d.velocity.sqrMagnitude == 0f)
                {
                    SetDirection(_currentDirection.sqrMagnitude > 0f ? _currentDirection : Random.insideUnitCircle);
                }
            
                rigidbody2d.velocity = rigidbody2d.velocity.normalized * (speed * ballTimeManager.FixedDeltaTime);
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            _previousDirection = _currentDirection;
            _currentDirection = rigidbody2d.velocity.normalized;

            CheckAngel();
        }

        private void CheckAngel()
        {
            Vector2 horizontal = new Vector2((_currentDirection.x < 0) ? -1f : 1f, 0);
            Vector2 vertical = new Vector2(0f, (_currentDirection.y < 0) ? -1f : 1f);

            float horizontalAngel = Vector2.SignedAngle(_currentDirection, horizontal);
            float verticalAngel = Vector2.SignedAngle(_currentDirection, vertical);

            if (Mathf.Abs(horizontalAngel) < minBounceAngel)
            {
                CorrectAngel(horizontalAngel, horizontal);
            }
            else if (Mathf.Abs(verticalAngel) < minBounceAngel)
            {
                CorrectAngel(verticalAngel, vertical);
            }
        }

        private void CorrectAngel(float angel, Vector2 side)
        {
            if (angel == 0)
            {
                angel = -Vector2.SignedAngle(side, _previousDirection);
            }
                
            Vector2 newDirection = side.Rotate(minBounceAngel * (angel > 0 ? 1 : -1));
            SetDirection(newDirection);
        }
    }
}