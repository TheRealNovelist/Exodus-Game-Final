using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;

public class GridXZ<TGridObject> {

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs {
        public int x;
        public int z;
    }

    private const int sortingOrderDefault = 500; 

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;

    public GridXZ(int width, int height, float cellSize, Vector3 originPosition, Func<GridXZ<TGridObject>, int, int, TGridObject> createGridObject) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int z = 0; z < gridArray.GetLength(1); z++) {
                gridArray[x, z] = createGridObject(this, x, z);
            }
        }

        CreateLine();
        
        
        bool showDebug = false;
        if (showDebug) 
        {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int z = 0; z < gridArray.GetLength(1); z++) {
                    debugTextArray[x, z] = CreateWorldText(gridArray[x, z]?.ToString(), null, GetWorldPosition(x, z) + new Vector3(cellSize, 0, cellSize) * .5f, 5, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
                   Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
                    
                    
                }
            }
            
            
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) => { debugTextArray[eventArgs.x, eventArgs.z].text = gridArray[eventArgs.x, eventArgs.z]?.ToString();
                
                
            };
        }
    }

    public int GetWidth() {
        return width;
    }

    public int GetHeight() {
        return height;
    }

    public float GetCellSize() {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int z) {
        return new Vector3(x, 0, z) * cellSize + originPosition;
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z) {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }

    public void SetGridObject(int x, int z, TGridObject value) {
        if (x >= 0 && z >= 0 && x < width && z < height) {
            gridArray[x, z] = value;
            TriggerGridObjectChanged(x, z);
        }
    }

    public void TriggerGridObjectChanged(int x, int z) {
        OnGridObjectChanged?.Invoke(this, new OnGridObjectChangedEventArgs { x = x, z = z });
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value) {
        GetXZ(worldPosition, out int x, out int z);
        SetGridObject(x, z, value);
    }

    public TGridObject GetGridObject(int x, int z) {
        if (x >= 0 && z >= 0 && x < width && z < height) {
            return gridArray[x, z];
        } 
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition) {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetGridObject(x, z);
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition) {
        return new Vector2Int(
            Mathf.Clamp(gridPosition.x, 0, width - 1),
            Mathf.Clamp(gridPosition.y, 0, height - 1)
        );
    }
    
    
    // Create Text in the World
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
    }

    public void CreateLine()
    {
        // Set the width and height of the grid
        int width = 10;
        int height = 10;

        // Set the spacing between the lines
        float space = 2.0f;

        // Create a new material
        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));

        // Set the color of the material
        lineMaterial.color = Color.red;

        // Draw the horizontal lines
        for (int i = 0; i <= height; i++)
        {
            // Create a new empty game object
            GameObject gridLines = new GameObject();

            // Add a Line Renderer to the game object
            LineRenderer lr = gridLines.AddComponent<LineRenderer>();

            // Set the material of the Line Renderer
            lr.material = lineMaterial;

            // Set the width of the line
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;

            // Set the number of points in the line
            lr.positionCount = 2;

            // Set the positions of the line
            lr.SetPosition(0, new Vector3(0, 0, i * space));
            lr.SetPosition(1, new Vector3(width * space, 0, i * space));
        }

        // Draw the vertical lines
        for (int i = 0; i <= width; i++)
        {
            // Create a new empty game object
            GameObject gridLines = new GameObject();

            // Add a Line Renderer to the game object
            LineRenderer lr = gridLines.AddComponent<LineRenderer>();

            // Set the material of the Line Renderer
            lr.material = lineMaterial;

            // Set the width of the line
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;

            // Set the number of points in the line
            lr.positionCount = 2;

            // Set the positions of the line
            lr.SetPosition(0, new Vector3(i * space, 0, 0));
            lr.SetPosition(1, new Vector3(i * space, 0 , height * space));
        }
    }
}
