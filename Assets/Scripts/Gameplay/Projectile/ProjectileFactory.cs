using Gameplay.Mechanics.Timer;
using Zenject;

namespace Gameplay.Projectile
{
    public class ProjectileFactory : PlaceholderFactory<ProjectileSpawnParams, Projectile>
    {
        private readonly ProjectileViewFactory _projectileViewFactory;
        private readonly TimerFactory _timerFactory;

        public ProjectileFactory(ProjectileViewFactory projectileViewFactory, TimerFactory timerFactory)
        {
            _projectileViewFactory = projectileViewFactory;
            _timerFactory = timerFactory;
        }

        public override Projectile Create(ProjectileSpawnParams spawnParams)
        {
            var projectileView = _projectileViewFactory.Create(spawnParams);
            var projectileInfo = spawnParams.ProjectileInfo;
            var timer = _timerFactory.Create(projectileInfo.LifeTime);
            var projectile = new Projectile(projectileView, timer, projectileInfo);
            return projectile;
        }
    }
}