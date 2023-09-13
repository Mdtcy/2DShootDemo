using System.Collections;
using BerserkPixel.Tilemap_Generator;
using Com.LuisPedroFonseca.ProCamera2D;
using Cysharp.Threading.Tasks;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameMain.Scripts.Utility;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;
using UnityGameFramework.Runtime;

namespace GameMain
{
    public class ProcedureMain : ProcedureBase
    {
        public Entity Player => GameEntry.Entity.GetEntity(_playerEntityId);
        // todo 优化
        public Tilemap GroundTileMap => GameObject.Find("Ground").GetComponent<Tilemap>();
        
        private int _playerEntityId;

        private ProCamera2D _proCamera2D;
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            Log.Debug("Enter ProcedureMain.");
            
            _levelTileGenerators = Object.FindObjectsOfType<LevelTileGenerator>();
            _levelObjectGenerators = Object.FindObjectsOfType<LevelObjectGenerator>();
            _mapObjectPlacer = Object.FindObjectOfType<MapObjectPlacer>();
            GenerateLevel().Forget();
            GameEntry.FeedBack.Init();
            // GameEntry.Projectile.Init();

            // 获取TileMap上一个随机没有碰撞体的位置
            var pos = TilemapUtility.FindPositionWithoutCollider(GroundTileMap, 3, ~0, 1000);
            Assert.IsTrue(pos!=null);
            
            _playerEntityId = GameEntry.Entity.GenerateSerialId();
            GameEntry.Entity.ShowPlayer(new PlayerData(_playerEntityId, 10300000)
            {
                Position = pos.Value,
                Rotation = Quaternion.identity,
                Scale = Vector3.one,
                PropID = 10200000,
            });
            // GameEntry.Entity.ShowEnemy(new EnemyData(GameEntry.Entity.GenerateSerialId(), 10300001)
            // {
            //     Position = new Vector3(0, 3, 0),
            //     Rotation = Quaternion.identity,
            //     Scale = Vector3.one,
            //     PropID = 10200001,
            // });
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        }

        private void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            var ne = (ShowEntitySuccessEventArgs) e;
            if (ne.EntityLogicType == typeof(Player))
            {
                _proCamera2D = Camera.main.GetComponent<ProCamera2D>();
                _proCamera2D.AddCameraTarget(ne.Entity.Logic.gameObject.transform);
            }
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // todo 这里优化一下 忽视在敌人附近的情况 layerMask
                var pos = TilemapUtility.FindPositionWithoutColliderNearPosition(GroundTileMap,
                    Player.transform.position,
                    15,3, 
                    ~0,
                    1000);

                if (pos == null)
                {
                    Debug.Log("没有找到合适的位置,不生成敌人");
                    return;
                }
                
                GameEntry.Entity.ShowEnemy(new EnemyData(GameEntry.Entity.GenerateSerialId(), 10300001)
                {
                    Position = pos.Value,
                    Rotation = Quaternion.identity,
                    Scale = Vector3.one,
                    PropID = 10200001,
                });
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                GenerateLevel().Forget();
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                var tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
                var pos = TilemapUtility.FindPositionWithoutCollider(tilemap, 3, ~0, 1000);
                Assert.IsTrue(pos!=null);
                Player.transform.position = pos.Value;
            }
        }
        
        private MapObjectPlacer _mapObjectPlacer;
        private LevelTileGenerator[] _levelTileGenerators;
        private LevelObjectGenerator[] _levelObjectGenerators;

        private async UniTask GenerateLevel()
        {
            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);


            if (_mapObjectPlacer != null) _mapObjectPlacer.ClearObjects();

            if (_levelTileGenerators != null && _levelTileGenerators.Length > 0)
                foreach (var levelTileGenerator in _levelTileGenerators)
                {
                    foreach (var layer in levelTileGenerator.GetActiveLayers()) layer.SetRandomSeed();
                    levelTileGenerator.GenerateLayers();
                }
            else if (_levelObjectGenerators != null)
                foreach (var levelObjectGenerator in _levelObjectGenerators)
                {
                    foreach (var layer in levelObjectGenerator.GetActiveLayers()) layer.SetRandomSeed();
                    levelObjectGenerator.GenerateLayers();
                }
            
            if (_mapObjectPlacer != null) _mapObjectPlacer.PlaceObjects();
        }
    }
}