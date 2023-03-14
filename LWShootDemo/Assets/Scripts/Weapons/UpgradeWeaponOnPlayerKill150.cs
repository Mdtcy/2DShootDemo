/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2023年3月14日
 * @modify date 2023年3月14日
 * @desc [击杀达到150个敌人后升级武器]
 */

#pragma warning disable 0649
using System;
using DG.Tweening;
using Events;
using LWShootDemo.Entities;
using LWShootDemo.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace LWShootDemo.Weapons
{
    /// <summary>
    /// 击杀达到150个敌人后升级武器 todo 仅用于展示效果
    /// </summary>
    public class UpgradeWeaponOnPlayerKill150 : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private Slider sliderKillCount;

        [SerializeField]
        private Transform upgradeTip;

        [SerializeField]
        private Transform tip;

        // local
        private int             killCount;
        private TimeStopManager timeStopManager;
        private Transform       player;
        private Sequence        sequence;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        private void Start()
        {
            timeStopManager = GameManager.Instance.TimeStopManager;
            player          = GameManager.Instance.Player;

            sliderKillCount.maxValue = 150;
            sliderKillCount.value    = 0;
            EnemyDeathEvent.Register(OnEnemyDeath);
        }

        private void OnEnemyDeath()
        {
            killCount++;
            sliderKillCount.value = killCount;
            sequence.Kill();
            sequence = DOTween.Sequence();
            var scaleTween = sliderKillCount.transform.DOScale(1.5f, 0.1f).SetEase(Ease.OutBack);
            var resetTween = sliderKillCount.transform.DOScale(1f, 0.1f).SetEase(Ease.OutSine);
            sequence.Append(scaleTween);
            sequence.Append(resetTween);
            sequence.Play();

            if (killCount == 150)
            {
                EnemyDeathEvent.Unregister(OnEnemyDeath);

                player.GetComponent<PlayerController>().ChangeWeapon();

                upgradeTip.DOScale(1, 2f)
                          .SetEase(Ease.OutBack)
                          .SetUpdate(true) // 不受TimeScale影响
                          .OnComplete(() =>
                           {
                               sliderKillCount.gameObject.SetActive(false);
                               upgradeTip.gameObject.SetActive(false);
                               tip.gameObject.SetActive(false);
                           });
                timeStopManager.StopTime(0,2);
            }
        }

        private void OnDestroy()
        {
            EnemyDeathEvent.Unregister(OnEnemyDeath);
        }

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649