using Animancer;
using GameMain;
using LWShootDemo.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LWShootDemo.Entities.Player
{
    public class PlayerFsmContext : MonoBehaviour
    {
        public NamedAnimancerComponent AnimancerComponent;

        public Rigidbody2D Rb2D;

        public EnemyDetector EnemyDetector;
        
        public FaceController FaceController;

        public float Speed;

        public Character Character;

        [BoxGroup("Shoot")]
        public float FireRate;

        [BoxGroup("Shoot")] 
        public Weapon Weapon;
        
        // [BoxGroup("Shoot")]
        // public float TimeToShoot;
    }
}