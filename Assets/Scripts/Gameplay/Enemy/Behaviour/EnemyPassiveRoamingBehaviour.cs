using Gameplay.Input;
using Gameplay.Level;
using Gameplay.Mechanics.Timer;
using Scriptables;
using Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Enemy
{
    public sealed class EnemyPassiveRoamingBehaviour : EnemyBehaviour
    {
        private const byte MaxTries = 10;

        private Vector3 _direction;

        public EnemyPassiveRoamingBehaviour(
            Updater updater,
            TimerFactory timerFactory,
            EnemyView view,
            EnemyInput enemyInput,
            LevelPartView levelPartView,
            EnemyConfig enemyConfig) : base(updater, timerFactory, view, enemyInput, levelPartView, enemyConfig)
        {
            Input.MoveForward(0);
            Input.MoveSideways(0);
            View.Rigidbody.velocity = Vector3.zero;

            Timer.OnExpire += GetNewDirection;
            GetNewDirection();
        }

        public override void Dispose()
        {
            Timer.OnExpire -= GetNewDirection;
            Input.MoveForward(0);
            Input.MoveSideways(0);
            base.Dispose();
        }

        private void GetNewDirection()
        {
            if (View == null)
            {
                Timer.Dispose();
                return;
            }

            var randomVector = Random.insideUnitCircle.normalized * EnemyConfig.DirectionLenght;
            var position = View.transform.position;
            var point = new Vector3(position.x + randomVector.x, position.y, position.z + randomVector.y);

            var count = 0;

            while (count < MaxTries)
            {
                if (LevelPartView.LevelPartCollider.bounds.Contains(point))
                {
                    _direction = point;
                    break;
                }
                count++;
            }

            if(_direction != point)
            {
                _direction = LevelPartView.transform.position;
            }
            Timer.Start();
        }

        protected override void OnUpdate()
        {
            if (View == null)
            {
                Dispose();
                return;
            }

            //if (Vector3Approximately(_direction, View.transform.position, 1.5f))
            //{
            //    GetNewDirection();
            //    return;
            //}
            View.Orientation.LookAt(_direction);
            Input.MoveForward(1);
        }

        private bool EqualsApproximately(float a, float b, float precision) => Mathf.Abs(b - a) <= precision;

        private bool Vector3Approximately(Vector3 original, Vector3 other, float precision) =>
            EqualsApproximately(original.x, other.x, precision) &&
            EqualsApproximately(original.y, other.y, precision);
    }
}