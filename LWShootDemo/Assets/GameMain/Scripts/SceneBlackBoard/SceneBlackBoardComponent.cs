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

        private EntityTable _entityTable;
        public EntityTable EntityTable
        {
            get
            {
                _entityTable = _entityTable ? _entityTable : GameEntry.TableConfig.Get<EntityTable>();
                return _entityTable;
            }
        }

        #region 指定Entity

        public EntityProp GetCoinPickUpProp()
        {
            return EntityTable.Get(10300011);
        }

        #endregion
    }
}