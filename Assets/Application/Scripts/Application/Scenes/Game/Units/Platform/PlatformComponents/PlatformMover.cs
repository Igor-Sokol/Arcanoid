using Application.Scripts.Application.Scenes.Game.PlayingField.PlayerInputs;
using Application.Scripts.Library.InitializeManager.Contracts;
using Application.Scripts.Library.ScreenInfo;
using UnityEngine;

namespace Application.Scripts.Application.Scenes.Game.Units.Platform.PlatformComponents
{
    public class PlatformMover : MonoBehaviour, IInitializing
    {
        private float _direction;
        private Vector2 _targetPosition;
        private Vector2 _availablePositionRange;
        
        [SerializeField] private ScreenInfo screenInfo;
        [SerializeField] private PlatformSize platformSize;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Rigidbody2D rigidbody2d;
        [SerializeField] private float speed;

        public void Initialize()
        {
            UpdateAvailablePosition(platformSize.Size);
        }
        
        private void OnEnable()
        {
            playerInput.OnStartTouching += OnStartTouching;
            playerInput.OnTouching += OnTouching;
            playerInput.OnStopTouching += OnStopTouching;
            platformSize.OnSizeChanged += UpdateAvailablePosition;
        }

        private void OnDisable()
        {
            playerInput.OnStartTouching -= OnStartTouching;
            playerInput.OnTouching -= OnTouching;
            playerInput.OnStopTouching -= OnStopTouching;
            platformSize.OnSizeChanged -= UpdateAvailablePosition;
        }

        private void FixedUpdate()
        {
            _direction = Mathf.Clamp(_targetPosition.x - rigidbody2d.position.x, -1, 1);

            Vector2 rigidbody2dPosition = rigidbody2d.position;
            Vector2 newPosition = new Vector2(rigidbody2dPosition.x + _direction * speed * Time.fixedDeltaTime,
                rigidbody2dPosition.y);

            newPosition = new Vector2(Mathf.Clamp(newPosition.x, _availablePositionRange.x, _availablePositionRange.y),
                newPosition.y);

            rigidbody2d.MovePosition(newPosition);
        }

        private void UpdateAvailablePosition(Vector2 platformNewSize)
        {
            Vector2 availablePosition = new Vector2(screenInfo.ScreenLeftBottom.x, screenInfo.ScreenRightUpper.x);
            float halfNewSize = platformNewSize.x / 2f;
            availablePosition += new Vector2(halfNewSize, -halfNewSize);

            _availablePositionRange = availablePosition;
        }
        
        private void OnStartTouching(Vector2 position)
        {
            _targetPosition = position;
        }
        
        private void OnTouching(Vector2 position)
        {
            _targetPosition = position;
        }

        private void OnStopTouching(Vector2 position)
        {
            _targetPosition = rigidbody2d.position + new Vector2(_direction, 0);
        }
    }
}
