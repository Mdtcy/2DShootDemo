using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameMain
{
    public class CoinPickUp : EntityLogicBase
    {
        public float _speed; // 金币飞行速度
        public Transform _playerTransform; // 玩家的 Transform
        private Player _player;
        private TrailRenderer _trailRenderer;
        private Tween _moveTween;
        private Sequence _sequence;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        // private float currentSpeed;
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            var data = userData as CoinPickUpData;
            _speed = data.Speed;
            _player = GameEntry.SceneBlackBoard.Player;
            _playerTransform = _player.transform;
            FlyToPlayer().Forget();
        }

        private async UniTask FlyToPlayer()
        {
            _trailRenderer.enabled = false;
            float _popHeight = 1;
            float _popDuration = 0.3f;
            // 随机方向
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;

            // 计算目标位置
            Vector3 targetPosition = CachedTransform.position + randomDirection * _popHeight;
            
            // 使用DOTween创建弹出动画
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOMove(targetPosition, _popDuration));
            _sequence.Play();

            await _sequence.AsyncWaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(Random.Range(1f, 2f)));
            // _trailRenderer.enabled = true;
            GameEntry.Entity.AttachEntity(Entity.Id, _player.Entity.Id);

            float duration = Vector3.Distance(CachedTransform.position, _playerTransform.position) / _speed;
            _moveTween = CachedTransform
                .DOLocalMove(Vector3.zero, duration)
                .SetEase(Ease.InOutSine)
                .OnComplete(OnCoinFlyComplete);
        }
        

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            _trailRenderer.enabled = false;
            _moveTween?.Kill();
        }

        private float _minModifier = 11;
        private float _maxModifier = 13;

        private void OnCoinFlyComplete()
        {
            GameEntry.Entity.HideEntity(this);
        }
    }
    
}