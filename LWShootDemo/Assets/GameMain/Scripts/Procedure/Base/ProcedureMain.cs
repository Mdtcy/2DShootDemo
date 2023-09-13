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
            var tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
            var pos = TilemapUtility.FindPositionWithoutCollider(tilemap, 3, ~0, 1000);
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
            var playerPos = Player.transform.position;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameEntry.Entity.ShowEnemy(new EnemyData(GameEntry.Entity.GenerateSerialId(), 10300001)
                {
                    Position = playerPos + new Vector3(Random.Range(-15f,15f), Random.Range(-15f,15f), 0),
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

            // if (Input.GetMouseButtonDown(0))
            // {
            //     // 获取鼠标的屏幕坐标
            //     Vector3 screenPoint = Input.mousePosition;
            //
            //     // 转换屏幕坐标为世界坐标
            //     Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
            //     worldPoint.z = 0; // 对于2D游戏，您可能希望设置z坐标为0
            //
            //     // 使用世界坐标进行检查
            //     if (TilemapUtility.IsInsideAnyCompositeCollider(worldPoint))
            //     {
            //         Log.Debug("在里面");
            //     }
            //     else
            //     {
            //         Log.Debug("不在里面");
            //     }
            // }
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
            //
            // var tilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
            // var tileMapCollider = tilemap.GetComponent<CompositeCollider2D>();
            // tileMapCollider.geometryType = CompositeCollider2D.GeometryType.Polygons;
        }
    }
}