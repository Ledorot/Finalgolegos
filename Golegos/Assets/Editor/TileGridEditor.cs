using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileGrid))]
public class TileGridEditor : Editor {

    //Grid that contains all the tiles
    private TileGrid grid;
    //Vector that updates the grid size whenever the update grid button is pressed
    private Vector2 editorGridSize = new Vector2(0f, 0f);
    //Float that updates the tile size whenever the update grid button is pressed
    private float editorTileSize = 0f;
    //List of tiles that were spawned in the scene
    private List<Transform> tiles = new List<Transform>();

    void OnEnable() {
        grid = target as TileGrid;
    }

    public override void OnInspectorGUI() {
        EditorGUILayout.BeginVertical();

        //Allow the fields to be edited
        grid.gridSize = EditorGUILayout.Vector2Field("Grid Size", grid.gridSize);
        grid.tileSize = EditorGUILayout.FloatField("Tile Size", grid.tileSize);
        grid.tile = EditorGUILayout.ObjectField("Tile", grid.tile, typeof(Transform), true) as Transform;

        //Button that updates the grid tiles
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(EditorWindow.focusedWindow.position.width / 2f - 100f);
        if (GUILayout.Button("Update Grid", GUILayout.Width(200f))) {
            UpdateGrid();
        }
        EditorGUILayout.EndHorizontal();

        //Button that deletes the grid tiles
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(EditorWindow.focusedWindow.position.width / 2f - 100f);
        if (GUILayout.Button("Delete tiles", GUILayout.Width(200f))) {
            DeleteTiles();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    private void UpdateGrid() {

        //Update the editor values
        editorGridSize.x = grid.gridSize.x;
        editorGridSize.y = grid.gridSize.y;
        editorTileSize = grid.tileSize;
        DeleteTiles();

        //Spawn all the tiles on the scene
        if (grid.tile && grid.gridSize.x > 0 && grid.gridSize.y > 0) {

            Vector3 newPos = grid.transform.position;
            float xPos = newPos.x;
            SpriteRenderer sprite = grid.tile.GetComponent<SpriteRenderer>();
            if (!sprite) {
                Debug.LogError("Tile doesn't have SpriteRenderer component");
            }
            //If the sprite doesn't ocupy the desired space, scale it so that it does
            if (sprite.bounds.size.x != grid.tileSize) {
                grid.tile.localScale *= (grid.tileSize / sprite.bounds.size.x);
            }
            for (int j = 0; j < grid.gridSize.y; j++) {
                for (int i = 0; i < grid.gridSize.x; i++) {
                    Transform newTile = Instantiate(grid.tile, newPos, grid.transform.rotation, grid.transform);
                    if (newTile == null) {
                        Debug.LogError("Not instantiated");
                    }
                    newTile.name = "Tile_" + ((i + 1) + j * grid.gridSize.x);
                    tiles.Add(newTile);
                    newPos.x += grid.tileSize;
                }
                newPos.x = xPos;
                newPos.y += grid.tileSize;
            }
        }
    }

    private void DeleteTiles() {
        foreach(Transform trans in tiles) {
            DestroyImmediate(trans.gameObject);
        }
        tiles.Clear();
    }

}