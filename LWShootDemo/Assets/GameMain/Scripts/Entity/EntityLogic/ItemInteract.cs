using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameMain.Item
{
    public class ItemInteract : EntityLogicBase
    {
        private SpriteRenderer _model;
        private ItemProp _itemProp;
        public ItemProp ItemProp => _itemProp;
        private Collider2D _collider2D;
        public Collider2D Collider2D => _collider2D;

        public float _popHeight = 1.0f; // 弹出高度
        public float _popDuration = 0.5f; // 弹出动画的持续时间
        private bool _canPick = false;
        private bool _hasPick = false;

        private Player _player;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            _model = GetComponentInChildren<SpriteRenderer>();
            _collider2D = GetComponent<Collider2D>();
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            var data = userData as ItemInteractData;
            _popHeight = data.PopUpHeight;
            _popDuration = data.PopUpDuration;
            _itemProp = data.ItemProp;
            _canPick = false;
            _player = null;
            _hasPick = false;
            
            _model.sprite = _itemProp.Model;
            // 随机方向
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;

            // 计算目标位置
            Vector3 targetPosition = CachedTransform.position + randomDirection * _popHeight;
            
            // 使用DOTween创建弹出动画
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(targetPosition, _popDuration));
            sequence.onComplete += OnAnimationComplete;
            sequence.Play();
        }

        private void OnAnimationComplete()
        {
            _canPick = true;
        }

        int? tipFormId = null;
        private bool Interacting => tipFormId != null;
        private void OnTriggerExit2D(Collider2D other)
        {
            var player = other.GetComponent<Player>();
            if (player == null || player != _player)
            {
                return;
            }

            if (tipFormId != null)
            {   
                GameEntry.UI.CloseUIForm(tipFormId.Value);
                tipFormId = null;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!_canPick)
            {
                return;
            }
            _player = other.GetComponent<Player>();
            if (_player == null)
            {
                return;
            }

            if (tipFormId == null)
            {
                tipFormId = GameEntry.UI.OpenUIForm(UIFormId.ItemTip, this);
            }
        }

        private void Update()
        {
            if (_hasPick)
            {
                return;
            }

            if (_player == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _player.PickItem(_itemProp);
                GameEntry.Entity.HideEntity(this);
                _hasPick = true;
            }
        }
    }
}