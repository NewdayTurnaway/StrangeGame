using System;
using Gameplay.Unit;
using Services;
using UnityEngine;

namespace Gameplay.Input
{
    public sealed class PlayerInput : IUnitMovementInput, IDisposable
    {
        private readonly Updater _updater;

        private const string MOUSE_X = "Mouse X";
        private const string MOUSE_Y = "Mouse Y";
        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";

        private const KeyCode PAUSE = KeyCode.Escape;
        
        private const KeyCode JUMP = KeyCode.Space;

        private const KeyCode FIRST_ATTACK = KeyCode.Mouse0;
        private const KeyCode SECOND_ATTACK = KeyCode.Mouse1;
        private const KeyCode FIRST_ABILITY = KeyCode.Q;
        private const KeyCode SECOND_ABILITY = KeyCode.E;

        private bool _blockPauseInput;

        public bool IsPause { get; set; }

        public event Action<bool> PauseInput = _ => { };

        public event Action<float> MouseXAxisInput = _ => { };
        public event Action<float> MouseYAxisInput = _ => { };

        public event Action<float> VerticalAxisInput = _ => { };
        public event Action<float> HorizontalAxisInput = _ => { };

        public event Action<bool> JumpInput = _ => { };

        public event Action<bool> FirstAttackInput = _ => { };
        public event Action<bool> SecondAttackInput = _ => { };
        public event Action<bool> FirstAbilityInput = _ => { };
        public event Action<bool> SecondAbilityInput = _ => { };

        public PlayerInput(Updater updater)
        {
            _updater = updater;
            
            _updater.SubscribeToUpdate(CheckPauseInput);
            _updater.SubscribeToUpdate(CheckMouseXAxisInput);
            _updater.SubscribeToUpdate(CheckMouseYAxisInput);
            _updater.SubscribeToUpdate(CheckVerticaAxislInput);
            _updater.SubscribeToUpdate(CheckHorizontalAxisInput);
            _updater.SubscribeToUpdate(CheckJumpInput);
            _updater.SubscribeToUpdate(CheckFirstAttackInput);
            _updater.SubscribeToUpdate(CheckSecondAttackInput);
            _updater.SubscribeToUpdate(CheckFirstAbilityInput);
            _updater.SubscribeToUpdate(CheckSecondAbilityInput);
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(CheckPauseInput);
            _updater.UnsubscribeFromUpdate(CheckMouseXAxisInput);
            _updater.UnsubscribeFromUpdate(CheckMouseYAxisInput);
            _updater.UnsubscribeFromUpdate(CheckVerticaAxislInput);
            _updater.UnsubscribeFromUpdate(CheckHorizontalAxisInput);
            _updater.UnsubscribeFromUpdate(CheckJumpInput);
            _updater.UnsubscribeFromUpdate(CheckFirstAttackInput);
            _updater.UnsubscribeFromUpdate(CheckSecondAttackInput);
            _updater.UnsubscribeFromUpdate(CheckFirstAbilityInput);
            _updater.UnsubscribeFromUpdate(CheckSecondAbilityInput);
        }

        public void SetActivePauseButton(bool active)
        {
            _blockPauseInput = !active;
        }

        private void CheckPauseInput()
        {
            if (_blockPauseInput) return;

            var value = UnityEngine.Input.GetKeyDown(PAUSE);
            if (value)
            {
                IsPause = !IsPause;
            }
            PauseInput(value);
        }

        private void CheckMouseXAxisInput()
        {
            if (IsPause) return;
            var inputValue = UnityEngine.Input.GetAxis(MOUSE_X);
            MouseXAxisInput(inputValue);
        }
        
        private void CheckMouseYAxisInput()
        {
            if (IsPause) return;
            var inputValue = UnityEngine.Input.GetAxis(MOUSE_Y);
            MouseYAxisInput(inputValue);
        }

        private void CheckVerticaAxislInput()
        {
            if (IsPause) return;
            var inputValue = UnityEngine.Input.GetAxisRaw(VERTICAL);
            VerticalAxisInput(inputValue);
        }

        private void CheckHorizontalAxisInput()
        {
            if (IsPause) return;
            var inputValue = UnityEngine.Input.GetAxisRaw(HORIZONTAL);
            HorizontalAxisInput(inputValue);
        }

        private void CheckJumpInput()
        {
            if (IsPause) return;
            var value = UnityEngine.Input.GetKey(JUMP);
            JumpInput(value);
        }
        
        private void CheckFirstAttackInput()
        {
            if (IsPause) return;
            var value = UnityEngine.Input.GetKey(FIRST_ATTACK);
            FirstAttackInput(value);
        }
        
        private void CheckSecondAttackInput()
        {
            if (IsPause) return;
            var value = UnityEngine.Input.GetKey(SECOND_ATTACK);
            SecondAttackInput(value);
        }
        
        private void CheckFirstAbilityInput()
        {
            if (IsPause) return;
            var value = UnityEngine.Input.GetKeyDown(FIRST_ABILITY);
            FirstAbilityInput(value);
        }
        
        private void CheckSecondAbilityInput()
        {
            if (IsPause) return;
            var value = UnityEngine.Input.GetKeyDown(SECOND_ABILITY);
            SecondAbilityInput(value);
        }
    }
}