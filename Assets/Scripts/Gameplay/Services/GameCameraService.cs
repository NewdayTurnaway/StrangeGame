using System;
using Gameplay.Input;
using Gameplay.Player;
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
        private readonly PlayerFactory _playerFactory;
        private readonly CameraConfig _cameraConfig;
        private readonly Transform _cameraTransform;

        private Player.Player _player;
        private Transform _headTransform;
        private Transform _orientationTransform;

        private float _xRotation;
        private float _yRotation;

        public GameCameraService(
            Updater updater, 
            PlayerInput playerInput, 
            PlayerFactory playerFactory,
            CameraConfig cameraConfig, 
            Camera mainCamera)
        {
            _updater = updater;
            _playerInput = playerInput;
            _playerFactory = playerFactory;
            _cameraConfig = cameraConfig;
            _cameraTransform = mainCamera.transform;

            _playerInput.MouseXAxisInput += OnMouseXChange;
            _playerInput.MouseYAxisInput += OnMouseYChange;

            _playerFactory.PlayerCreated += OnPlayerCreated;
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

            _playerFactory.PlayerCreated -= OnPlayerCreated;
            
            if(_player != null)
            {
                _player.PlayerDestroyed -= OnPlayerDestroyed;
            }

            _updater.UnsubscribeFromUpdate(RotateCamera);
            _updater.UnsubscribeFromUpdate(FollowPlayer);
        }

        private void OnPlayerCreated(Player.Player player)
        {
            _player = player;
            _headTransform = _player.PlayerView.Head;
            _orientationTransform = _player.PlayerView.Orientation;

            _player.PlayerDestroyed += OnPlayerDestroyed;
            _updater.SubscribeToUpdate(RotateCamera);
            _updater.SubscribeToUpdate(FollowPlayer);
        }

        private void OnPlayerDestroyed()
        {
            Dispose();
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
            _orientationTransform.rotation = Quaternion.Euler(0, _yRotation, 0);
            _headTransform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        }

        private void FollowPlayer()
        {
            _cameraTransform.position = _headTransform.position;
        }
    }
}