using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//NOTE dont use for now
public class GridBuilding : MonoBehaviour
{
   /*public const int sortingOrderDefault = 5000;//for create text
   
   private int widthOfGrid;
   private int heightOfGrid;
   private float cellSizeOfGrid;
   private int[,] gridArray;
   private Vector3 originPosition; //position we want to create our grid

   private TextMesh[,] debugTextArray; //array for debug
   
   //Create grid with value passed in 
   //Call this function to create grid
   public GridBuilding(int widthOfGrid, int heightOfGrid, float cellSizeOfGrid, Vector3 originPosition)
   {
      this.widthOfGrid = widthOfGrid;
      this.heightOfGrid = heightOfGrid;
      this.cellSizeOfGrid = cellSizeOfGrid;
      this.originPosition = originPosition;
      
      //create new array of grid with value passed in
      gridArray = new int[widthOfGrid, heightOfGrid];
      
      //create a grid of text for debug
      debugTextArray = new TextMesh[widthOfGrid, heightOfGrid];

      
      //print grid out by text
      for (int x = 0; x < gridArray.GetLength(0); x++)
      {
         for (int y = 0; y < gridArray.GetLength(1); y++)
         {
            /*
            debugTextArray[x, y] =  CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSizeOfGrid, cellSizeOfGrid) *0.5f, 20, Color.white,
               TextAnchor.MiddleCenter);
            #1#
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
         }
         Debug.DrawLine(GetWorldPosition(0, heightOfGrid), GetWorldPosition(widthOfGrid, heightOfGrid), Color.white, 100f);
         Debug.DrawLine(GetWorldPosition(widthOfGrid, 0), GetWorldPosition(widthOfGrid, heightOfGrid), Color.white, 100f);
         
      }
      
   }
   
   
   //get position of grid to print text
   private Vector3 GetWorldPosition(int x, int y)
   {
      return new Vector3(x, y) * cellSizeOfGrid + originPosition;
   }
   
   //get  world position of gridcell and output x and y value of gridcell
   private void GetXY(Vector3 worldPosition, out int x, out int y) {
      x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSizeOfGrid);
      y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSizeOfGrid);
   }
   
   //take parameter passed in to change value of grid cell
   public void SetValue(int x, int y, int value) {
      if (x >= 0 && y >= 0 && x < widthOfGrid && y < heightOfGrid) {
         gridArray[x, y] = value;
         debugTextArray[x, y].text = gridArray[x, y].ToString();
      }
   }
// overload of above function
//this one will receive worldposition, use GetXY() to get x and y value of gridcell
//and change value of that cell by value passed in
   public void SetValue(Vector3 worldPosition, int value)
   {
      int x, y;
      GetXY(worldPosition, out x, out y);
      SetValue(x, y, value);
   }
   
   //return value of grid cell by pass in it x and y
   public int GetValue(int x, int y) {
      if (x >= 0 && y >= 0 && x < widthOfGrid && y < heightOfGrid) {
         return gridArray[x, y];
      } else {
         return 0;
      }
   }
   //return value of grid cell by pass in it world position
   public int GetValue(Vector3 worldPosition) {
      int x, y;
      GetXY(worldPosition, out x, out y);
      return GetValue(x, y);
   } */
   
   /*// Create Text in the World
   public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault) {
      if (color == null) color = Color.white;
      return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
   }
        
   // Create Text in the World
   public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder) {
      GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
      Transform transform = gameObject.transform;
      transform.SetParent(parent, false);
      transform.localPosition = localPosition;
      TextMesh textMesh = gameObject.GetComponent<TextMesh>();
      textMesh.anchor = textAnchor;
      textMesh.alignment = textAlignment;
      textMesh.text = text;
      textMesh.fontSize = fontSize;
      textMesh.color = color;
      textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
      return textMesh;
   }*/
}
