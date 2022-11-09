using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMakingTesting : MonoBehaviour
{
    private GridBuilding grid;

    public int width;
    public int height;
    public GameObject baseCubePrefab;
    public CubeInGrid[,] allCubes; 
    
    private void Start()
    {
       // cos teer dungf trong tuonwg lai ddeer debug
       // grid = new GridBuilding(width, height, 10f, new Vector3(0, 0, 0));
        allCubes = new CubeInGrid[width, height]; 
        SetUpCube();
    }

    void SetUpCube()
    {
        for (int row = 0; row < width ; row++){
            for ( int col = 0; col < height; col++) 
            {
                // instantiates the tile prefab at coordinates row and col
                //  Instantiate() contrus an Object , so this  'casts' it instead as GameObject
                // A Tile is 512*512 and 512 Pixels per unit, and so is exacly 1 unit squared
                GameObject cube = Instantiate(baseCubePrefab, new Vector3(row, 0, col), Quaternion.identity) as GameObject;

                // Set the tile name to it's coordinates
                cube.name = "Cube ( " + row + ", " + col +")"; 
                // store the tilePrefabs Tile script at the appropriate position in the array
                allCubes[row, col] = cube.GetComponent<CubeInGrid>();

                // To keep things tidy, parent the tiles to the pieces object in the Hierarchy
                cube.transform.parent = GameObject.Find("Cubes").transform;

                /*Call the Init method on the tile and pass it a reference and pass it row and col(which become Tile.xIndex and 
                Tile.yIndex and pass it a reference to the board which becomes Tile.boardscript;*/
                allCubes[row, col].Init(row, 0, col,  this);
            }
        }
    }
}
