using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameMain.Item
{
    public class ItemInteract : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer _model;

        private ItemProp _itemProp;
        public ItemProp ItemProp => _itemProp;
        private Collider2D _collider2D;

        public Collider2D Collider2D
        {
            get
            {
                if (_collider2D == null)
                {
                    _collider2D = GetComponent<Collider2D>();
                }

                return _collider2D;
            }
        }

        public float popHeight = 1.0f; // 弹出高度
        public float popDuration = 0.5f; // 弹出动画的持续时间
        private bool _canPick = false;

        private Sequence sequence;
        [Button]
        public void Setup(ItemProp itemProp, Vector3 initPos)
        {
            transform.position = initPos;
            _itemProp = itemProp;
            _model.sprite = itemProp.Model;
            
            // 随机方向
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;

            // 计算目标位置
            Vector3 targetPosition = initPos + randomDirection * popHeight;

            sequence.Kill();
            // 使用DOTween创建弹出动画
            sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(targetPosition, popDuration));
            sequence.onComplete += OnAnimationComplete;
            sequence.Play();
            // sequence.Append(transform.DOLocalMoveY(transform.localPosition.y + floatAmount, floatDuration).SetEase(Ease.OutQuad));
            // sequence.Append(transform.DOLocalMoveY(transform.localPosition.y - floatAmount, floatDuration).SetEase(Ease.OutQuad));
            // sequence.SetLoops(-1, DG.Tweening.LoopType.Yoyo); // 上下浮动循环
        }

        private void OnAnimationComplete()
        {
            _canPick = true;
        }

        int? tipFormId = null;


        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_canPick)
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

            if (tipFormId == null)
            {
                tipFormId = GameEntry.UI.OpenUIForm(UIFormId.ItemTip, this);
            }

            var player = other.GetComponent<Player>();
            if (player != null && Input.GetKeyDown(KeyCode.F))
            {
                player.PickItem(_itemProp);
                Destroy(gameObject);
            }
        }
    }
}