using UnityEngine.Assertions;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class SceneBlackBoardComponent : GameFrameworkComponent
    {
        public Player Player { get; set; }

        public Inventory Inventory
        {
            get
            {
                Assert.IsNotNull(Player, "Player is null");
                return Player.Inventory;
            }
        }
    }
}