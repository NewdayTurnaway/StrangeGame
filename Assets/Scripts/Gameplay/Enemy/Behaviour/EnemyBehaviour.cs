using Gameplay.Input;
using Gameplay.Level;
using Gameplay.Mechanics.Timer;
using Scriptables;
using Services;
using System;
using UnityEngine;

namespace Gameplay.Enemy
{
    public abstract class EnemyBehaviour : IDisposable
    {
        protected Updater Updater;
        protected Timer Timer;
        protected EnemyView View;
        protected EnemyInput Input;
        protected LevelPartView LevelPartView;
        protected EnemyConfig EnemyConfig;

        public EnemyBehaviour(
            Updater updater,
            TimerFactory timerFactory,
            EnemyView view,
            EnemyInput enemyInput,
            LevelPartView levelPartView,
            EnemyConfig enemyConfig
            )
        {
            Updater = updater;
            Timer = timerFactory.Create(enemyConfig.RandomDirectionCooldown);
            View = view;
            Input = enemyInput;
            LevelPartView = levelPartView;
            EnemyConfig = enemyConfig;

            Updater.SubscribeToUpdate(OnUpdate);
        }

        protected abstract void OnUpdate();

        public virtual void Dispose()
        {
            Updater.UnsubscribeFromUpdate(OnUpdate);
            Timer.Dispose();
        }
    }
}