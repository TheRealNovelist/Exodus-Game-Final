using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInGrid : MonoBehaviour
{
    public int xIndex; // the x location of the Tile
    public int yIndex; // the y location of the Tiles
    public int zIndex; //the z location of the Tiles

    private MapMakingTesting mapTestScript;
    private TurretManager _turretManager;
    

    // Start is called before the first frame update
    void Start()
    {
       mapTestScript = GameObject.Find("MapGridMaking").GetComponent<MapMakingTesting>();
       _turretManager = GameObject.Find("TurretManager").GetComponent<TurretManager>();
    }

    // Sets the xIndex, yIndex, and boardScript variables to the ones passed in
    //we are passing in a boardScript in case later we want more than one boardScript in our game
    //Call by Board.SetupTiles when the level starts
    public void  Init(int x, int y, int z, MapMakingTesting map)
    {
        xIndex = x; //store the x-location of the cubes
        yIndex = y; //store the y-location of the cubes
        zIndex = z; //store the z-location of the cubes
        mapTestScript = map;

    }
    //A cube has been clicked on
    private void OnMouseDown()
    {
        if (_turretManager != null)
        {
            //make turret at the location of the mouse
            _turretManager.ClickCube(this);
            
        }
    }
    
    //A tile has been entered by the mouse
    private void OnMouseOver()
    {
        if (_turretManager != null)
        {
            //make transpanrent turret
            _turretManager.CubeMouseIsOn(this);
        }
        
    }
    
    
}
