using LWShootDemo.Entities;
using UnityEngine;

namespace DefaultNamespace.GameMain.Scripts.HpBar
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private ProgressBar _progressBar;

        [SerializeField] private OldEntity _oldEntity;
        
        private void OnEnable()
        {
            _oldEntity.ActOnHpChanged += OnHpChanged;
            _progressBar.UpdateProgressImmeadiatly(_oldEntity.MaxHp, _oldEntity.MaxHp);
        }

        private void OnDisable()
        {
            _oldEntity.ActOnHpChanged -= OnHpChanged;
        }

        private void OnHpChanged(int hp)
        {
            _progressBar.UpdateProgress(hp, _oldEntity.MaxHp);
        }
    }
}