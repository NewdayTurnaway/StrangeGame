using System;
using Services;
using UnityEngine;

namespace Gameplay.Input
{
    public sealed class PlayerInput : IDisposable
    {
        private readonly Updater _updater;

        private const string MOUSE_X = "Mouse X";
        private const string MOUSE_Y = "Mouse Y";
        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";

        private const KeyCode JUMP = KeyCode.Space;
        //private const KeyCode SIT_DOWN = KeyCode.LeftControl;

        //private const KeyCode ATTACK_1 = KeyCode.Mouse0;
        //private const KeyCode ATTACK_2 = KeyCode.Mouse1;
        //private const KeyCode SKILL_1 = KeyCode.Q;
        //private const KeyCode SKILL_2 = KeyCode.E;


        public event Action<float> MouseXAxisInput = _ => { };
        public event Action<float> MouseYAxisInput = _ => { };

        public event Action<float> VerticalAxisInput = _ => { };
        public event Action<float> HorizontalAxisInput = _ => { };

        public event Action<bool> JumpInput = _ => { };
        //public event Action<bool> SitDownInput = _ => { };

        //public event Action<bool> Attack1Input = _ => { };
        //public event Action<bool> Attack2Input = _ => { };
        //public event Action<bool> Skill1Input = _ => { };
        //public event Action<bool> Skill2Input = _ => { };

        public PlayerInput(Updater updater)
        {
            _updater = updater;
            
            _updater.SubscribeToUpdate(CheckMouseXAxisInput);
            _updater.SubscribeToUpdate(CheckMouseYAxisInput);
            _updater.SubscribeToUpdate(CheckVerticaAxislInput);
            _updater.SubscribeToUpdate(CheckHorizontalAxisInput);
            _updater.SubscribeToUpdate(CheckJumpInput);
        }

        public void Dispose()
        {
            _updater.UnsubscribeFromUpdate(CheckMouseXAxisInput);
            _updater.UnsubscribeFromUpdate(CheckMouseYAxisInput);
            _updater.UnsubscribeFromUpdate(CheckVerticaAxislInput);
            _updater.UnsubscribeFromUpdate(CheckHorizontalAxisInput);
            _updater.UnsubscribeFromUpdate(CheckJumpInput);
        }
        
        private void CheckMouseXAxisInput()
        {
            float inputValue = UnityEngine.Input.GetAxis(MOUSE_X);
            MouseXAxisInput(inputValue);
        }
        
        private void CheckMouseYAxisInput()
        {
            float inputValue = UnityEngine.Input.GetAxis(MOUSE_Y);
            MouseYAxisInput(inputValue);
        }

        private void CheckVerticaAxislInput()
        {
            float inputValue = UnityEngine.Input.GetAxisRaw(VERTICAL);
            VerticalAxisInput(inputValue);
        }

        private void CheckHorizontalAxisInput()
        {
            float inputValue = UnityEngine.Input.GetAxisRaw(HORIZONTAL);
            HorizontalAxisInput(inputValue);
        }

        private void CheckJumpInput()
        {
            bool value = UnityEngine.Input.GetKey(JUMP);
            JumpInput(value);
        }
    }
}