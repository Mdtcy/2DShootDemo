using System;
using GameMain;
using LWShootDemo.Entities;
using UnityEngine;

namespace DefaultNamespace.GameMain.Scripts.HpBar
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private ProgressBar _progressBar;

        [SerializeField] private CanvasGroup _canvasGroup;

        public void UpdateProgress(int hp, int maxHp)
        {
            _progressBar.UpdateProgress(hp, maxHp);
        }

        public void UpdateImmeadiatly(int hp, int maxHp)
        {
            _progressBar.UpdateProgressImmeadiatly(hp, maxHp);
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
        }
    }
}