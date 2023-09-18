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

        public float popHeight = 1.0f; // 弹出高度
        public float popDuration = 0.5f; // 弹出动画的持续时间
        public float floatAmount = 0.1f; // 浮动的高度
        public float floatDuration = 1.0f; // 浮动动画的持续时间

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
            sequence.Play();
            // sequence.Append(transform.DOLocalMoveY(transform.localPosition.y + floatAmount, floatDuration).SetEase(Ease.OutQuad));
            // sequence.Append(transform.DOLocalMoveY(transform.localPosition.y - floatAmount, floatDuration).SetEase(Ease.OutQuad));
            // sequence.SetLoops(-1, DG.Tweening.LoopType.Yoyo); // 上下浮动循环
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            var player = other.GetComponent<Player>();
            if (player != null && Input.GetKeyDown(KeyCode.F))
            {
                player.PickItem(_itemProp);
                Destroy(gameObject);
            }
        }
    }
}