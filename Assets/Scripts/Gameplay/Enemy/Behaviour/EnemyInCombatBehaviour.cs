using Gameplay.Input;
using Gameplay.Level;
using Gameplay.Mechanics.Timer;
using Gameplay.Player;
using Scriptables;
using Services;
using UnityEngine;

namespace Gameplay.Enemy
{
    public sealed class EnemyInCombatBehaviour : EnemyBehaviour
    {
        private readonly PlayerView _playerView;

        private float _sqrtDistance;

        private float _lastVerticalInput;
        private float _lastHorizontalInput;

        public EnemyInCombatBehaviour(
            Updater updater,
            TimerFactory timerFactory,
            EnemyView view,
            EnemyInput enemyInput,
            LevelPartView levelPartView,
            EnemyConfig enemyConfig,
            PlayerView playerView) : base(updater, timerFactory, view, enemyInput, levelPartView, enemyConfig)
        {
            _playerView = playerView;

            Timer.SetMaxValue(EnemyConfig.OptionalCooldown);

            View.Rigidbody.velocity = Vector3.zero;
            Timer.OnExpire += SetSideMove;
            Timer.Start();
        }

        public override void Dispose()
        {
            Timer.OnExpire -= SetSideMove;
            Input.MoveForward(0);
            Input.MoveSideways(0);
            base.Dispose();
        }

        protected override void OnUpdate()
        {
            if (View == null) return;
            if (_playerView == null)
            {
                Dispose();
                return;
            }

            View.Head.LookAt(_playerView.Orientation);
            var newPosition = _playerView.Orientation.position;
            newPosition.y = View.Orientation.position.y;
            View.Orientation.LookAt(newPosition);
            
            Jump();
            Move();
            UseAbility();
        }

        private void UseAbility()
        {
            var newCheckDistance = EnemyConfig.StopDistance * 1.5;

            if (_sqrtDistance <= newCheckDistance * newCheckDistance)
            {
                Input.SecondAbility();
            }
        }

        private void SetSideMove()
        {
            if(View == null)
            {
                Timer.Dispose();
                return;
            }

            var newCheckDistance = EnemyConfig.StopDistance * 1.5;

            if (_sqrtDistance >= newCheckDistance * newCheckDistance)
            {
                Input.MoveSideways(-_lastHorizontalInput * 2);
                _lastHorizontalInput = -_lastHorizontalInput;
                Timer.Start();
                return;
            }

            if (Random.Range(0, 2) == 0)
            {
                Input.MoveSideways(-1);
                _lastHorizontalInput = -1;
            }
            else
            {
                Input.MoveSideways(1);
                _lastHorizontalInput = 1;
            }

            Timer.Start();
        }

        private void Jump()
        {
            var checkHeight = View.Orientation.position.y + EnemyConfig.EnemyHeight;
            if (_playerView.Orientation.position.y > checkHeight)
            {
                Input.Jump();
            }
        }

        private void Move()
        {
            _sqrtDistance = (_playerView.Orientation.position - View.Orientation.position).sqrMagnitude;

            if (_sqrtDistance < EnemyConfig.StopDistance * EnemyConfig.StopDistance)
            {
                Input.MoveForward(-1);
                _lastVerticalInput = -1;
            }
            else if (_sqrtDistance > EnemyConfig.StopDistance * EnemyConfig.StopDistance)
            {
                Input.MoveForward(1);
                _lastVerticalInput = 1;
            }
            
            if(EqualsApproximately(_sqrtDistance, 0, 1))
            {
                Input.MoveForward(-_lastVerticalInput * 2);
                _lastVerticalInput = -_lastVerticalInput;
                View.Rigidbody.velocity = Vector3.zero;
            }
        }

        private bool EqualsApproximately(float a, float b, float precision) => Mathf.Abs(b - a) <= precision;
    }
}