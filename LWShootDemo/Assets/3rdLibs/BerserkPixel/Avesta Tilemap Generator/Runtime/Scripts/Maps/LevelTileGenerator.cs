using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BerserkPixel.Tilemap_Generator
{
    [AddComponentMenu("Avesta/Tilemaps/Level Tile Generator")]
    public class LevelTileGenerator : LevelGenerator
    {
        [Space] 
        [Header("Type of Tile to use")] 
        [Tooltip("Add your Tile here")] 
        [SerializeField] private TileBase tile;

        [Header("Collider Generation")] 
        [SerializeField] private bool generateColliders = true;

        public override List<MapLayer> GetActiveLayers()
        {
            if (layers == null || layers.Count <= 0) return new List<MapLayer>(1);

            return layers.FindAll(layer => layer.active);
        }

        protected override void GenerateColliders()
        {
            if (!generateColliders) return;

            var tilemapCollider2D = tilemap.gameObject.GetComponent<TilemapCollider2D>();
            if (tilemapCollider2D != null)
            {
                tilemapCollider2D.ProcessTilemapChanges();
                return;
            }

            var tilemapCollider = tilemap.gameObject.AddComponent<TilemapCollider2D>();
            tilemap.gameObject.AddComponent<CompositeCollider2D>();

            var rb = tilemap.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;

            tilemapCollider.usedByComposite = true;
        }

        protected override void RenderMap(MapArray terrainMap)
        {
            var positions = terrainMap.GetPositions();
            var tileArray = terrainMap.GetTiles(tile);

            tilemap.SetTiles(positions, tileArray);
        }

        protected override void ClearMap()
        {
            if (tilemap != null)
            {
                tilemap.ClearAllTiles();
                tilemap.RefreshAllTiles();
            }

            if (!generateColliders) return;

            if (tilemap.gameObject.TryGetComponent(out TilemapCollider2D tilemapCollider2D))
                DestroyImmediate(tilemapCollider2D);

            if (tilemap.gameObject.TryGetComponent(out CompositeCollider2D compositeCollider2D))
                DestroyImmediate(compositeCollider2D);

            if (tilemap.gameObject.TryGetComponent(out Rigidbody2D rb)) DestroyImmediate(rb);
        }
    }
}