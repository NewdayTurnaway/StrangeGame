using Gameplay.Input;
using Gameplay.Mechanics.Timer;
using Scriptables;
using Services;
using System;
using UnityEngine;

namespace Gameplay.Enemy
{
    public sealed class EnemyMovement : IDisposable
    {
        private const float RAYCAST_OFFSET = 0.2f;

        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;
        private readonly Updater _updater;
        private readonly Timer _timer;
        private readonly EnemyInput _input;
        private readonly EnemyConfig _enemyConfig;

        private float _verticalInput;
        private float _horizontalInput;

        private Vector3 _moveDirection;
        private bool _isGrounded;
        private bool _readyToJump;

        public EnemyMovement(Updater updater, TimerFactory timerFactory, EnemyView enemyView, EnemyInput input, EnemyConfig enemyConfig)
        {
            _transform = enemyView.Orientation;
            _rigidbody = enemyView.Rigidbody;
            _updater = updater;
            _input = input;
            _enemyConfig = enemyConfig;

            _timer = timerFactory.Create(_enemyConfig.JumpCooldown);
            _timer.OnExpire += ResetJump;
            _timer.Start();

            _rigidbody.freezeRotation = true;

            _input.VerticalAxisInput += OnVerticalAxisChange;
            _input.HorizontalAxisInput += OnHorizontalAxisChange;
            _input.JumpInput += OnJumpInputChange;

            _updater.SubscribeToUpdate(OnUpdate);
            _updater.SubscribeToFixedUpdate(Move);
        }

        public void Dispose()
        {
            _timer.OnExpire -= ResetJump;
            _timer.Dispose();

            _input.VerticalAxisInput -= OnVerticalAxisChange;
            _input.HorizontalAxisInput -= OnHorizontalAxisChange;
            _input.JumpInput -= OnJumpInputChange;

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
            if (_transform == null) return;

            GroundCheck();
            SpeedControl();
        }

        private void ResetJump()
        {
            _readyToJump = true;
        }

        private void Move()
        {
            if (_transform == null) return;

            _moveDirection = _transform.forward * _verticalInput + _transform.right * _horizontalInput;
            
            if (_isGrounded)
            {
                _rigidbody.AddForce(_enemyConfig.MoveSpeed * _enemyConfig.SpeedMiltiplier * _moveDirection.normalized, ForceMode.Force); 
            }
            else
            {
                _rigidbody.AddForce(_enemyConfig.MoveSpeed * _enemyConfig.SpeedMiltiplier * _enemyConfig.AirMiltiplier * _moveDirection.normalized, ForceMode.Force); 
            }
        }

        private void GroundCheck()
        {
            _isGrounded = Physics.Raycast(_transform.position,Vector3.down, _enemyConfig.EnemyHeight * 0.5f + RAYCAST_OFFSET, _enemyConfig.GroundLayer);

            if (_isGrounded)
            {
                _rigidbody.drag = _enemyConfig.GroundDrag;
            }
            else
            {
                _rigidbody.drag = 0;
            }
        }

        private void SpeedControl()
        {
            var flatVelocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            var sqrtMoveSpeed = _enemyConfig.MoveSpeed * _enemyConfig.MoveSpeed;
            
            if (flatVelocity.sqrMagnitude > sqrtMoveSpeed)
            {
                var limitedVelocity = flatVelocity.normalized * _enemyConfig.MoveSpeed;
                _rigidbody.velocity = new Vector3(limitedVelocity.x, _rigidbody.velocity.y, limitedVelocity.z);
            }
        }

        private void Jump()
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(_enemyConfig.JumpForce * _transform.up, ForceMode.Impulse);
        }
    }
}