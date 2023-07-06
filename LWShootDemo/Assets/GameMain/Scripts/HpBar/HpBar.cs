using System;
using GameMain;
using LWShootDemo.Entities;
using UnityEngine;

namespace DefaultNamespace.GameMain.Scripts.HpBar
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private ProgressBar _progressBar;

        // [SerializeField] private OldEntity _oldEntity;
        
        private void OnEnable()
        {
            // _oldEntity.ActOnHpChanged += OnHpChanged;
            // _progressBar.UpdateProgressImmeadiatly(_oldEntity.MaxHp, _oldEntity.MaxHp);
        }

        private void OnDisable()
        {
            // _oldEntity.ActOnHpChanged -= OnHpChanged;
        }

        // private Character _character;
        // public void Init(Character character)
        // {
        //     _character = character;
        //     _progressBar.UpdateProgressImmeadiatly(_character.MaxHp, _character.MaxHp);
        // }
        //
        // private void OnHpChanged(int hp)
        // {
        //     _progressBar.UpdateProgress(hp, _character.MaxHp);
        // }
        
        public void UpdateProgress(int hp, int maxHp)
        {
            _progressBar.UpdateProgress(hp, maxHp);
        }

        public void UpdateImmeadiatly(int hp, int maxHp)
        {
            _progressBar.UpdateProgressImmeadiatly(hp, maxHp);
        }
    }
}