using Gameplay.Unit;
using System;

namespace Gameplay.Input
{
    public sealed class EnemyInput : IUnitMovementInput
    {
        public bool IsPause { get; set; }

        public event Action<float> MouseXAxisInput = _ => { };
        public event Action<float> MouseYAxisInput = _ => { };

        public event Action<float> VerticalAxisInput = _ => { };
        public event Action<float> HorizontalAxisInput = _ => { };

        public event Action<bool> JumpInput = _ => { };

        //public event Action<bool> FirstAttackInput = _ => { };
        //public event Action<bool> SecondAttackInput = _ => { };
        public event Action<bool> FirstAbilityInput = _ => { };
        public event Action<bool> SecondAbilityInput = _ => { };

        public void LookVertically(float verticalInput)
        {
            if (!IsPause) MouseYAxisInput(verticalInput);
        }
        
        public void LookHorizontally(float horizontalInput)
        {
            if (!IsPause) MouseXAxisInput(horizontalInput);
        }
        
        public void MoveForward(float verticalInput)
        {
            if (!IsPause) VerticalAxisInput(verticalInput);
        }
        
        public void MoveSideways(float horizontalInput)
        {
            if (!IsPause) HorizontalAxisInput(horizontalInput);
        }

        public void Jump()
        {
            if (!IsPause) JumpInput(true);
        }

        //public void FirstAttack()
        //{
        //    if (!IsPause) FirstAttackInput(true);
        //}

        //public void SecondAttack()
        //{
        //    if (!IsPause) SecondAttackInput(true);
        //}

        public void FirstAbility()
        {
            if (!IsPause) FirstAbilityInput(true);
        }

        public void SecondAbility()
        {
            if (!IsPause) SecondAbilityInput(true);
        }
    }
}