using System;
using System.Collections.Generic;
using System.Linq;
using BerserkPixel.Tilemap_Generator.Attributes;
using BerserkPixel.Tilemap_Generator.Destructible;
using BerserkPixel.Tilemap_Generator.SO;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BerserkPixel.Tilemap_Generator
{
    public abstract class LevelGenerator : MonoBehaviour
    {
        public delegate void OnMapChangeDelegate(MapArray map);

        public event OnMapChangeDelegate OnMapChange;

        [Space] 
        [Header("Tilemap")] 
        [SerializeField] protected Tilemap tilemap;

        [Tooltip("Check to clear the map before putting new tiles on it")]
        [SerializeField] private bool _clearOnGenerate = true;

        [Note("Be aware that the priority of the layers goes from top to bottom.\n" +
              "Every map algorithm must have the same dimensions")]
        [Tooltip("The list of layers that will be used to generate the tilemap")]
        [SerializeField] protected List<MapLayer> layers;

        private MapArray _generatedMap;
        private Dictionary<int, MapLayer> _cachedAllActiveDictionary;

        private void OnValidate()
        {
            var totalLayers = GetActiveLayers();
            if (totalLayers == null || totalLayers.Count <= 0) return;

            try
            {
                _cachedAllActiveDictionary = totalLayers.ToDictionary(layer => layer.MapConfig.GetHashCode());
            }
            catch (ArgumentException)
            {
                return;
            }
            
            if (totalLayers.Count > 1)
            {
                for (var i = 0; i < totalLayers.Count; i++)
                {
                    if (i == 0)
                    {
                        totalLayers[i].IsTheOnlyActive = true;
                        totalLayers[i].IsAdditive = false;
                    }
                    else
                    {
                        totalLayers[i].IsTheOnlyActive = false;
                    }
                }
            }
            else
            {
                totalLayers[0].IsTheOnlyActive = true;
                totalLayers[0].IsAdditive = false;
            }
            
            foreach (var mapLayer in _cachedAllActiveDictionary)
            {
                mapLayer.Value.MapConfig.OnMapChange += HandleMapConfigChange;
            }
            
            _generatedMap = _cachedAllActiveDictionary.Values.ToList().GetTotalMap();
            OnMapChange?.Invoke(_generatedMap);
        }

        private void OnDestroy()
        {
            foreach (var mapLayer in _cachedAllActiveDictionary)
            {
                mapLayer.Value.MapConfig.OnMapChange -= HandleMapConfigChange;
            }
        }

        private void HandleMapConfigChange(MapConfigSO changedMap)
        {
            _generatedMap = _cachedAllActiveDictionary.Values.ToList().GetTotalMap();
            OnMapChange?.Invoke(_generatedMap);
        }

        public abstract List<MapLayer> GetActiveLayers();

        /// <summary>
        ///     Renders the MapArray into a Tilemap.
        ///     <param name="terrainMap">The MapArray to use for rendering</param>
        /// </summary>
        protected abstract void RenderMap(MapArray terrainMap);

        /// <summary>
        ///     If selected in the inspector, this method attaches a TilemapCollider2D and a
        ///     Rigidbody2D properly to the Tilemap
        /// </summary>
        protected abstract void GenerateColliders();

        /// <summary>
        ///     Clears the tilemap and it's colliders
        /// </summary>
        protected abstract void ClearMap();

        public void GenerateLayers()
        {
            var totalLayers = GetActiveLayers().GetTotalMap();

            if (_clearOnGenerate)
                ClearMap();
            
            RenderMap(totalLayers);

            GenerateColliders();

            foreach (var shadow in FindObjectsOfType<TilemapShadow>()) shadow.UpdateShadows();
            
            foreach (var additive in FindObjectsOfType<MapAdditive>()) additive.AddTiles();

            foreach (var destructibleBg in FindObjectsOfType<DestructibleTilemapBackground>())
                destructibleBg.UpdateBackground();
        }

        public void DoClearMap()
        {
            ClearMap();

            foreach (var shadow in FindObjectsOfType<TilemapShadow>()) shadow.ClearShadows();
            
            foreach (var additive in FindObjectsOfType<MapAdditive>()) additive.Clear();

            foreach (var destructibleBg in FindObjectsOfType<DestructibleTilemapBackground>())
                destructibleBg.ClearBackground();
        }
    }
}