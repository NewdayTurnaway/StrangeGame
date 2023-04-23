using Gameplay.Unit;
using System;

namespace Gameplay.Input
{
    public sealed class EnemyInput : IUnitMovementInput
    {
        public event Action<float> MouseXAxisInput = _ => { };
        public event Action<float> MouseYAxisInput = _ => { };

        public event Action<float> VerticalAxisInput = _ => { };
        public event Action<float> HorizontalAxisInput = _ => { };

        public event Action<bool> JumpInput = _ => { };

        public event Action<bool> FirstAttackInput = _ => { };
        public event Action<bool> SecondAttackInput = _ => { };
        public event Action<bool> FirstAbilityInput = _ => { };
        public event Action<bool> SecondAbilityInput = _ => { };

        public void LookVertically(float verticalInput)
        {
            MouseYAxisInput(verticalInput);
        }
        
        public void LookHorizontally(float horizontalInput)
        {
            MouseXAxisInput(horizontalInput);
        }
        
        public void MoveForward(float verticalInput)
        {
            VerticalAxisInput(verticalInput);
        }
        
        public void MoveSideways(float horizontalInput)
        {
            VerticalAxisInput(horizontalInput);
        }

        public void Jump()
        {
            JumpInput(true);
        }

        public void FirstAttack()
        {
            FirstAttackInput(true);
        }

        public void SecondAttack()
        {
            SecondAttackInput(true);
        }

        public void FirstAbility()
        {
            FirstAbilityInput(true);
        }

        public void SecondAbility()
        {
            SecondAbilityInput(true);
        }
    }
}