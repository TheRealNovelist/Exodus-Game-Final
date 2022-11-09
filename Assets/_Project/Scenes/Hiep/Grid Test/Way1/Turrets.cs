using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    public int xIndex; // the current x coordinate of the  turret
   public int yIndex; // the current y coordinate of the turret
   public int zIndex; // the current z coordinate of the turret

   public bool isMoving = false; //Checks whether pieces are moving right now 
   private TurretManager turretManager; // a reference to the TurretManager class

   
   
   
   
   
   //Sets the x and the y index to the arguments passed in
   // Call by PieceManager.PlaceGamePiece() function
   // Called by MoveRoutine when a game piece is moved
   public void SetCoor(int x, int y, int z){
        xIndex = x; // set xIndex to the value passed in by the funtion called
        yIndex = y; // set yIndex to the value passed in by the funtion called
        zIndex = z; 
   }
   
   //call by ******
   
    //Initialises the GamePiece to give it access to the PieceManager class
    //called by PieceManager.FillRandom()
    public void Init(TurretManager tr)
    {
        if (tr != null)
        {
            //set the pieceManager variable to the one passed in to the function
            turretManager = tr; 
        }
        else
        {
            Debug.Log("jdfhbglszjdhfbiloushdfb");
        }
    }
}
