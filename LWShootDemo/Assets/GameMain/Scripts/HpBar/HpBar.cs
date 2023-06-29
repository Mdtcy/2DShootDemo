using LWShootDemo.Entities;
using UnityEngine;

namespace DefaultNamespace.GameMain.Scripts.HpBar
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private ProgressBar _progressBar;

        [SerializeField] private Entity _entity;
        
        private void OnEnable()
        {
            _entity.ActOnHpChanged += OnHpChanged;
            _progressBar.UpdateProgressImmeadiatly(_entity.MaxHp, _entity.MaxHp);
        }

        private void OnDisable()
        {
            _entity.ActOnHpChanged -= OnHpChanged;
        }

        private void OnHpChanged(int hp)
        {
            _progressBar.UpdateProgress(hp, _entity.MaxHp);
        }
    }
}