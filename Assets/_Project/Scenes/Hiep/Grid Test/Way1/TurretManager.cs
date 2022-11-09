using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    [SerializeField] private MapMakingTesting _mapMakingTesting; //reference to class to take value in MapFridMaking object

    //name a array of turrets(by name of class Turrets)
    public Turrets[,] allTurretsArray; 
    
    //make 2 one dimention array to hold 2 type of turret
    //one for player to put on cube, one is transparent to show when mouse is over the cube
    public GameObject turretPrefabNormal;
    public GameObject turretPrefabTransparent;
    private GameObject currentPlaceableObject;
    
    private CubeInGrid _cubeMouseIsOn; //cube that player mouse in hovering above
    private CubeInGrid _cubeBeenClick; //cube that player clicked on
    
    private void Start()
    {
        //create array of turrets with width and height the same as mapMaking
        allTurretsArray = new Turrets[_mapMakingTesting.width, _mapMakingTesting.height];
        
    }
    
    //place a turret at the x, y and z value passed in
    //add that turret to the allTurretArray
    //call when player clicked on cube
    public void PlaceTurret(Turrets turret, int x, int z, int y = 0)
    {
        //safety program check
        if (turret == null)
        {
            Debug.Log("TurretManger : Invalid Turret");
        }
        
        //move the turret passed in into the position passed in
        turret.transform.position = new Vector3(x, y, z);
        //set the rotation back to 0
        turret.transform.rotation = Quaternion.identity;
        
        //call CetCoor() in Turrets
        turret.SetCoor(x, y, z);
        
        //the function IsWithinBounds() to check if value passed in is valid
        if (IsWithinBounds(x, z) == true)
        {
            Debug.Log("Is in bound");
            //assign the turret to the correct place in the allGamePieces array
            allTurretsArray[x, y] = turret;
        }
    }
    
    private Turrets MakeTurret(GameObject prefab, int x, int z)
    {
        if (prefab != null && IsWithinBounds(x, z) == true)
        {
            // Initialise the turret to give it access to the TurretManager class
            prefab.GetComponent<Turrets>().Init(this);
            allTurretsArray[x, z] = prefab.GetComponent<Turrets>();
            //if it is valid, place it at the row and col passed in
            PlaceTurret(prefab.GetComponent<Turrets>(), x,z);


            // Set the turret name to it's coordinates
            prefab.name = "turret name's ( " + x + ", " + z + ")";


            // To keep things tidy, parent the turrets to the turrets object in the Hierarchy
            prefab.transform.parent = GameObject.Find("Turrets").transform;

            //return the turret to the function calling this
            return prefab.GetComponent<Turrets>();
        }

        return null;
    }
    
    // Sets the clickedTile variable to the tile passed in
    //Called by class Tile.OnMouseDown() when a tile is clicked
    public void ClickCube(CubeInGrid cube)
    {
        //tiles that can be clicked are always = to null
        if (_cubeBeenClick == null)
        {
            // set clicked t the tile passed in by ***
            _cubeBeenClick = cube;
            Debug.Log("Clicked cube" + cube.name);
            if (currentPlaceableObject == null)
            {
                currentPlaceableObject = Instantiate(turretPrefabNormal);
            }
            else
            {
                Destroy(currentPlaceableObject);
            }
        }
    }
    
   //get value of cube that mouse of player is hover above
    public void CubeMouseIsOn(CubeInGrid cube)
    {
        //set the target tile to the tile passed in
        if (_cubeBeenClick == null)
        {
            _cubeMouseIsOn = cube;
            Debug.Log("Hovering over cube" + cube.name);
            turretPrefabTransparent.transform.position = cube.transform.position;
            turretPrefabTransparent.transform.rotation = Quaternion.identity;

        }
        
    }
    
    //check if value passed in is within the map we set
    public bool IsWithinBounds(int x, int z)
    {
        //checks to make sure x is between 0 and the width-1 and y is within 0 and height-1
        //return true or false
        if (x >= 0 && x < _mapMakingTesting.width && z >= 0 && z < _mapMakingTesting.height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
