using System;
using Animancer.Editor;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using LWShootDemo;
using UnityEngine;
using UnityGameFramework.Runtime;
using Random = UnityEngine.Random;

namespace GameMain
{
    public class CoinPickUp : EntityLogicBase
    {
        private float _speed;
        private TrailRenderer _trailRenderer;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            var data = userData as CoinPickUpData;
            _speed = data.Speed;
            Showing().Forget();
        }

        private async UniTask Showing()
        {
            _trailRenderer.enabled = false;
            float randomx = Random.Range(1.5f, 2f);
            float randomy = Random.Range(1.5f, 2f);
            randomy = Random.Range(0, 100) > 50 ? -randomy : randomy;
            randomx = Random.Range(0, 100) > 50 ? -randomx : randomx;

            var pos = CachedTransform.position + new Vector3(randomx, randomy, 0);
            float duration = Random.Range(0.3f, 0.4f);
            var tween = transform.DOMove(pos, duration).SetEase(Ease.OutSine);

            while (tween.IsPlaying())
            {
                await UniTask.Yield(); // 异步等待一帧
            }

            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            _trailRenderer.enabled = true;

            _speed = _initSpeed;
            DOTween.To(() => _speed, (x) => _speed = x, _finalSpeed, 3f).SetEase(Ease.InSine);
            var player = GameManager.Instance.Player;

            while (Vector3.Distance(player.transform.position, CachedTransform.position) >= 0.1f)
            {
                var dir = (player.transform.position - CachedTransform.position).normalized;
                CachedTransform.position += dir * _speed;
                await UniTask.Yield(); // 异步等待一帧
            }

            GameEntry.Entity.HideEntity(Entity);
        }

        private float _initSpeed = 0;

        private float _finalSpeed = 5;
    }
}