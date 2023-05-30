/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月11日
 * @modify date 2023年3月11日
 * @desc [敌人控制器]
 */

#pragma warning disable 0649
using System.Collections;
using Damages;
using Events;
using LWShootDemo.Difficulty;
using LWShootDemo.Enemy;
using LWShootDemo.Explosions;
using LWShootDemo.Pool;
using LWShootDemo.Sound;
using LWShootDemo.TimeStop;
using UnityEngine;
using Zenject;

namespace LWShootDemo.Entities.Enemy
{
    /// <summary>
    /// 敌人控制器
    /// </summary>
    [RequireComponent(typeof(Entity))]
    public class EnemyController : PoolObject
    {
        #region FIELDS

        [SerializeField]
        private Entity entity;

        // 正常时候的材质
        [SerializeField]
        private Material matNormal;

        // 闪烁时候的材质
        [SerializeField]
        private Material matFlash;

        // 被击退时的力大小
        [SerializeField]
        private float knockBackForce = 1;

        [SerializeField]
        private SpriteRenderer spModel;

        // 移动速度
        [SerializeField]
        private float moveSpeed;

        // * local
        private Transform            player;
        private SoundManager         soundManager;
        private Coroutine            flashCoroutine;
        private TimeStopManager      timeStopManager;
        private ExplosionManager     explosionManager;
        private SimpleUnitySpawnPool deathEffectPool;
        private DifficultyManager    difficultyManager;

        private SimpleUnitySpawnPool pool;

        #endregion

        #region PROPERTIES

        public SimpleUnitySpawnPool Pool   => pool;
        public bool                 IsDead => entity.IsDead;

        #endregion

        #region PUBLIC METHODS

        public void EnemyUpdate()
        {
            if (player == null)
            {
                return;
            }

            LookAtTarget();
            ChaseTarget();
        }

        // todo 待优化 有时间再改
        public void Setup(SimpleUnitySpawnPool enemyConfigEnemyPool)
        {
            pool = enemyConfigEnemyPool;
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            player           = GameManager.Instance.Player;
            soundManager     = GameManager.Instance.SoundManager;
            timeStopManager  = GameManager.Instance.TimeStopManager;
            explosionManager = GameManager.Instance.explosionManager;
            deathEffectPool  = GameManager.Instance.EnemyDeathEffectPool;
            difficultyManager = GameManager.Instance.DifficultyManager;

            // entity.ActOnHurt  += OnHurt;
            entity.ActOnDeath += OnDeath;
        }

        private void OnDestroy()
        {
            // entity.ActOnHurt  -= OnHurt;
            entity.ActOnDeath -= OnDeath;
        }

        public override void OnSpawn()
        {
            base.OnSpawn();

            entity.Init();
        }

        public override void OnDespawn()
        {
            base.OnDespawn();
            spModel.material = matNormal;
            entity.Reset();
        }

        // 死亡
        private void OnDeath()
        {
            explosionManager.CreateExplosion(transform.position);
            CreateDeathEffect();
            // Debug.Log(gameObject.name);
            EnemyDeathEvent.Trigger();
        }

        // 死亡效果
        private void CreateDeathEffect()
        {
            var deathEffect = deathEffectPool.Get();
            var deathEffectTransform = deathEffect.transform;
            deathEffectTransform.position   = transform.position;
            deathEffectTransform.rotation   = Quaternion.identity;
            deathEffectTransform.localScale = transform.localScale;
            deathEffect.GetComponent<EnemyDeathEffect>().Init();
        }

        // 受击
        private void OnHurt(DamageInfo damageInfo)
        {
            // 被击退
            entity.ApplyKnowBack(0.2f, damageInfo.Direction * knockBackForce);

            // todo 效果不明显 删除 替换为其他效果
            // // 暴击时弹出暴击字样
            // popupManager.Create(transform.position, damageInfo.IsCrit? PopupType.CriticalDamage : PopupType.NormalDamage);

            // 音效
            soundManager.PlaySfx(SoundType.Hit);

            // 时间停止
            timeStopManager.StopTime(0f, 0.02f);

            // 闪烁
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
            }
            flashCoroutine = StartCoroutine(IFlash(0.05f));
        }

        // 看向Player
        private void LookAtTarget()
        {
            Transform target         = player.transform;
            Vector2   targetPosition = target.position - transform.position;
            float    lookAngle      = Vector2.Angle(Vector2.up, targetPosition);

            if (targetPosition.x > 0)
            {
                lookAngle = 360 - lookAngle;
            }

            entity.TryRotate(lookAngle);
        }

        // 追踪Player
        private void ChaseTarget()
        {
            var direction = player.position - transform.position;
            entity.TryMove(direction, moveSpeed * difficultyManager.GetCurrentDifficulty().EnemySpeedNum);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // 撞到Player造成一点伤害
            var target = other.collider.GetComponent<Entity>();
            if (target!=null && target.Side == Side.Player)
            {
                // var damageInfo = new DamageInfo(1, entity.transform.position - transform.position, false);
                var dir = entity.transform.position - transform.position;
                // var damageInfo = new DamageInfo(this.entity, target, 1, dir,0,null);
                _damageManager.DoDamage(entity, target, 1, dir,0,new DamageInfoTag[]{});
                // target.TakeDamage(damageInfo);
            }
        }

        [Inject]
        private IDamageManager _damageManager;

        // 闪光
        private IEnumerator IFlash(float duration)
        {
            spModel.material = matFlash;

            yield return new WaitForSecondsRealtime(duration);
            spModel.material = matNormal;
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649