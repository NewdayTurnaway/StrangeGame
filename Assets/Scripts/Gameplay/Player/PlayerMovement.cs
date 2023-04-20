using Gameplay.Input;
using Gameplay.Mechanics.Timer;
using Scriptables;
using Services;
using System;
using UnityEngine;

namespace Gameplay.Player
{
    public sealed class PlayerMovement : IDisposable
    {
        private const float RAYCAST_OFFSET = 0.2f;

        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly Updater _updater;
        private readonly Timer _timer;
        private readonly PlayerInput _playerInput;
        private readonly PlayerConfig _playerConfig;

        private float _verticalInput;
        private float _horizontalInput;

        private Vector3 _moveDirection;
        private bool _isGrounded;
        private bool _readyToJump;

        public PlayerMovement(Updater updater, TimerFactory timerFactory, PlayerView playerView, PlayerInput playerInput, PlayerConfig playerConfig)
        {
            _transform = playerView.Orientation;
            _rigidbody = playerView.Rigidbody;
            _updater = updater;
            _playerInput = playerInput;
            _playerConfig = playerConfig;

            _timer = timerFactory.Create(_playerConfig.JumpCooldown);

            _rigidbody.freezeRotation = true;

            _playerInput.VerticalAxisInput += OnVerticalAxisChange;
            _playerInput.HorizontalAxisInput += OnHorizontalAxisChange;
            _playerInput.JumpInput += OnJumpInputChange;

            _updater.SubscribeToUpdate(OnUpdate);
            _updater.SubscribeToFixedUpdate(Move);
        }

        public void Dispose()
        {
            _timer.Dispose();

            _playerInput.VerticalAxisInput -= OnVerticalAxisChange;
            _playerInput.HorizontalAxisInput -= OnHorizontalAxisChange;
            _playerInput.JumpInput -= OnJumpInputChange;

            _updater.UnsubscribeFromUpdate(OnUpdate);
            _updater.UnsubscribeFromFixedUpdate(Move);
        }


        private void OnVerticalAxisChange(float verticalInput)
        {
            _verticalInput = verticalInput;
        }

        private void OnHorizontalAxisChange(float horizontalInput)
        {
            _horizontalInput = horizontalInput;
        }
        
        private void OnJumpInputChange(bool jumplInput)
        {
            if (jumplInput && _readyToJump && _isGrounded)
            {
                _readyToJump = false;
                Jump();

                _timer.Start();
            }
        }

        private void OnUpdate()
        {
            GroundCheck();
            SpeedControl();

            if (_readyToJump)
            {
                return;
            }

            if (_timer.IsExpired)
            {
                _readyToJump = true;
            }
        }

        private void Move()
        {
            _moveDirection = _transform.forward * _verticalInput + _transform.right * _horizontalInput;
            
            if (_isGrounded)
            {
                _rigidbody.AddForce(_playerConfig.MoveSpeed * _playerConfig.SpeedMiltiplier * _moveDirection.normalized, ForceMode.Force); 
            }
            else
            {
                _rigidbody.AddForce(_playerConfig.MoveSpeed * _playerConfig.SpeedMiltiplier * _playerConfig.AirMiltiplier * _moveDirection.normalized, ForceMode.Force); 
            }
        }

        private void GroundCheck()
        {
            _isGrounded = Physics.Raycast(_transform.position,Vector3.down, _playerConfig.PlayerHeight * 0.5f + RAYCAST_OFFSET, _playerConfig.GroundLayer);

            if (_isGrounded)
            {
                _rigidbody.drag = _playerConfig.GroundDrag;
            }
            else
            {
                _rigidbody.drag = 0;
            }
        }

        private void SpeedControl()
        {
            var flatVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            var sqrtMoveSpeed = _playerConfig.MoveSpeed * _playerConfig.MoveSpeed;
            
            if (flatVelocity.sqrMagnitude > sqrtMoveSpeed)
            {
                var limitedVelocity = flatVelocity.normalized * _playerConfig.MoveSpeed;
                _rigidbody.velocity = new Vector3(limitedVelocity.x, _rigidbody.velocity.y, limitedVelocity.z);
            }
        }

        private void Jump()
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(_playerConfig.JumpForce * _transform.up, ForceMode.Impulse);
        }
    }
}