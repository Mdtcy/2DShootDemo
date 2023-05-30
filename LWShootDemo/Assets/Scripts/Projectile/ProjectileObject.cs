using GameFramework;
using GameFramework.ObjectPool;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LWShootDemo.Weapons
{
    public class ProjectileObject : ObjectBase
    {
        private static Scene _poolScene;
        private static Scene PoolScene
        {
            get
            {
                if (!_poolScene.isLoaded)
                {
                    _poolScene = SceneManager.CreateScene("ProjectilePool");
                }
        
                return _poolScene;
            }
        }
        
        public static ProjectileObject Create(object target)
        {
            ProjectileObject projectileObject = ReferencePool.Acquire<ProjectileObject>();
            projectileObject.Initialize(target);
            return projectileObject;
        }

        protected internal override void Release(bool isShutdown)
        {
            Projectile projectile = (Projectile)Target;
            if (projectile == null)
            {
                return;
            }
        
            Object.Destroy(projectile.gameObject);
        }

        protected internal override void OnSpawn()
        {
            base.OnSpawn();
            Projectile projectile = (Projectile)Target;
            projectile.gameObject.SetActive(true);
            SceneManager.MoveGameObjectToScene(projectile.gameObject, PoolScene);
        }

        protected internal override void OnUnspawn()
        {
            base.OnUnspawn();
            Projectile projectile = (Projectile)Target;
            projectile.gameObject.SetActive(false);
        }
    }
}