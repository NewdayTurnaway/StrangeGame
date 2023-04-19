using System;
using Gameplay.Input;
using Scriptables;
using Services;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public sealed class GameCameraService : IInitializable, IDisposable
    {
        private const int DEFAULT_ANGLE = 90;

        private readonly Updater _updater;
        private readonly PlayerInput _playerInput;
        private readonly CameraConfig _cameraConfig;
        private readonly Transform _cameraTransform;

        private float _xRotation;
        private float _yRotation;

        public GameCameraService(Updater updater, PlayerInput playerInput, CameraConfig cameraConfig, Camera mainCamera)
        {
            _updater = updater;
            _playerInput = playerInput;
            _cameraConfig = cameraConfig;
            _cameraTransform = mainCamera.transform;

            _playerInput.MouseXAxisInput += OnMouseXChange;
            _playerInput.MouseYAxisInput += OnMouseYChange;
         
            _updater.SubscribeToUpdate(RotateCamera);
            _updater.SubscribeToUpdate(FollowPlayer);
        }

        public void Initialize()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Dispose()
        {
            _playerInput.MouseXAxisInput -= OnMouseXChange;
            _playerInput.MouseYAxisInput -= OnMouseYChange;
            
            _updater.UnsubscribeFromUpdate(RotateCamera);
            _updater.UnsubscribeFromUpdate(FollowPlayer);
        }

        private void OnMouseXChange(float mouseX)
        {
            _yRotation += mouseX * Time.deltaTime * _cameraConfig.SensitivityX;
        }

        private void OnMouseYChange(float mouseY)
        {
            _xRotation -= mouseY * Time.deltaTime * _cameraConfig.SensitivityY;
            _xRotation = Mathf.Clamp(_xRotation, -90, DEFAULT_ANGLE);
        }

        private void RotateCamera()
        {
            _cameraTransform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
            //_playerTransform.rotation = Quaternion.Euler(0, _yRotation, 0);
        }

        private void FollowPlayer()
        {
            //_headTransform.position = _cameraTransform.position;
        }
    }
}