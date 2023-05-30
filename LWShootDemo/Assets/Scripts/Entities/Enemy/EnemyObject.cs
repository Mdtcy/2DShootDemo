using GameFramework;
using GameFramework.ObjectPool;
using LWShootDemo.Entities.Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Entities.Enemy
{
    public class EnemyObject : ObjectBase
    {
        private static Scene _poolScene;
        private static Scene PoolScene
        {
            get
            {
                if (!_poolScene.isLoaded)
                {
                    _poolScene = SceneManager.CreateScene("EnemyPool");
                }
        
                return _poolScene;
            }
        }
        
        public static EnemyObject Create(object target)
        {
            EnemyObject enemyObject = ReferencePool.Acquire<EnemyObject>();
            enemyObject.Initialize(target);
            return enemyObject;
        }

        protected internal override void Release(bool isShutdown)
        {
            GameObject enemy = (GameObject)Target;
            if (enemy == null)
            {
                return;
            }
        
            Object.Destroy(enemy.gameObject);
        }

        protected internal override void OnSpawn()
        {
            base.OnSpawn();
            GameObject enemy = (GameObject)Target;
            GameObject gameObject = enemy.gameObject;
            enemy.GetComponent<EnemyController>().OnSpawn();
            gameObject.SetActive(true);
            SceneManager.MoveGameObjectToScene(gameObject, PoolScene);
        }

        protected internal override void OnUnspawn()
        {
            base.OnUnspawn();
            GameObject enemy = (GameObject)Target;
            enemy.GetComponent<EnemyController>().OnDespawn();
            enemy.SetActive(false);
        }
    }
}