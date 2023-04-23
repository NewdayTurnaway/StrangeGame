using System;

namespace Gameplay.Unit
{
    public interface IUnitMovementInput
    {
        public event Action<float> MouseXAxisInput;
        public event Action<float> MouseYAxisInput;

        public event Action<float> VerticalAxisInput;
        public event Action<float> HorizontalAxisInput;

        public event Action<bool> JumpInput;

        //public event Action<bool> FirstAttackInput;
        //public event Action<bool> SecondAttackInput;
        public event Action<bool> FirstAbilityInput;
        public event Action<bool> SecondAbilityInput;
    }
}