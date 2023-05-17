using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GridBuildingSystem : MonoBehaviour
{
   public static GridBuildingSystem Instance { get; private set; }
   
   public int gridWidth = 5;
   public int gridHeight = 5;
   public float cellSize = 5f;
   public bool showGridLayOut;

    public event EventHandler OnSelectedChanged;
    public event EventHandler OnObjectPlaced;

    private GridXZ<GridObject> grid;
    [SerializeField] private List<PlacedObjectTypeSO> placeObjectTypeSOList = new List<PlacedObjectTypeSO>();
    [SerializeField] private List<ShopButton> _buttons;
    
    private PlacedObjectTypeSO placedObjectTypeSO;
    private PlacedObjectTypeSO.Dir dir;

    [SerializeField] private Camera turretCamera;

    private GameObject lineParent;
    private Shop _shop;


    private void Awake() {
        Instance = this;

        _shop = FindObjectOfType<Shop>();
        if (!_shop)
        {
            Debug.Log("No Shop Found");
            Debug.Break();
        }
    
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, this.transform.position, (GridXZ<GridObject> g, int x, int y) => new GridObject(g, x, y));

      //  placedObjectTypeSO = placeObjectTypeSOList[0];//set placeObject to the first one in the list
        placedObjectTypeSO = null;
        
    }

    private void Start()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            var index = i;
            _buttons[index].SetupButton(placeObjectTypeSOList[index],() => ChangeToTurret(index));
        }

        lineParent = new GameObject();
        
        lineParent.name = "Line Holder";
        lineParent.transform.parent = transform;
        lineParent.transform.localPosition = Vector3.zero;
        CreateLine(gridWidth, gridHeight, cellSize,this.transform.position, lineParent);
    }
    private void Update() 
    { 
        //Place object on place
        if (placedObjectTypeSO != null && Input.GetMouseButton(0)) 
        {
            PlaceObjects();
            DeselectObjectType();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateObjectBeforePlace();
        }

        //Delete object in a place
        if (Input.GetKeyDown(KeyCode.G))
        {
            RemovePlacedDefender();
        }
        
        //Deselect object
        if (Input.GetKeyDown(KeyCode.Alpha0)) { DeselectObjectType(); }

        lineParent.SetActive(showGridLayOut);
        
    }


    public class GridObject {

        private GridXZ<GridObject> grid;
        private int x;
        private int z;
        private PlacedObject placedObject;
        
        public GridObject(GridXZ<GridObject> grid, int x, int z) {
            this.grid = grid;
            this.x = x;
            this.z = z;
            placedObject = null;
        }
        //print out name of object 
        public override string ToString()
        {
            return x + ", " + z + "\n" + placedObject;
        }

        public void SetPlacedObject(PlacedObject placedObject)
        {
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x, z);
        }


        public void ClearPlacedObject()
        {
            placedObject = null;
            grid.TriggerGridObjectChanged(x, z);
        }

        public PlacedObject GetPlacedObject()
        {
            return placedObject;
        }
        //check if we can build
        public bool CanBuild()
        {
            return placedObject == null;
        }
    }

    public void PlaceObjects()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        grid.GetXZ(mousePosition, out int x, out int z);
           
        List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x, z), dir);
            
        //test is it can build
        bool canBuild = true;
        foreach (Vector2Int gridPosition in gridPositionList)
        {
            if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
            {
                //cannot build here
                canBuild = false;
                break;
            }
        }
            
        if (canBuild && CoinManager.Instance.CoinAmount >= placedObjectTypeSO.price)
        {
            Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPossition = grid.GetWorldPosition(x, z) +
                                                 new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

            PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPossition,
                new Vector2Int(x, z), dir, placedObjectTypeSO);


            foreach (Vector2Int gridPosition in gridPositionList)
            {
                grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
            }
            
            //_shop.PurchasedItem?.Invoke(placedObjectTypeSO);
        }
        else
        {
            _shop.shopUI.ToggleShopPanel(true);
        }
        
       // _shopUI.CloseShop();
       
       
    }

    // Rotate placeObject before place it  on grid
    public void RotateObjectBeforePlace()
    {
        dir = PlacedObjectTypeSO.GetNextDir(dir);
    }

    public void ChangeToTurret(int index)
    {
        placedObjectTypeSO = placeObjectTypeSOList[index]; 
        RefreshSelectedObjectType();
    }

    public void DeselectTurret()
    {
        placedObjectTypeSO = null; 
        RefreshSelectedObjectType();
    }
    
    private void RemovePlacedDefender()
    {
        GridObject gridObject = grid.GetGridObject(GetMouseWorldPosition());
        //get the object in the position to delete
        PlacedObject placedObject = gridObject.GetPlacedObject();
        if (placedObject != null)
        {
            //delete value of placeObject on the grid
            List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
                
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
            }
                
            placedObject.DestroySelf(); //delete the prefab
        }
    }

    private void DeselectObjectType() {
        placedObjectTypeSO = null; 
        RefreshSelectedObjectType();
    }

    private void RefreshSelectedObjectType() {
        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }


    public Vector2Int GetGridPosition(Vector3 worldPosition) {
        grid.GetXZ(worldPosition, out int x, out int z);
        return new Vector2Int(x, z);
    }

    public Vector3 GetMouseWorldSnappedPosition() {
        Vector3 mousePosition = GetMouseWorldPosition();
        grid.GetXZ(mousePosition, out int x, out int z);

        if (placedObjectTypeSO != null) 
        {
            Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        } 
        else 
        {
            return mousePosition;
        }
    }

    public Quaternion GetPlacedObjectRotation() 
    {
        if (placedObjectTypeSO != null) 
        {
            return Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0);
        } 
        else 
        {
            return Quaternion.identity;
        }
    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO() {
        return placedObjectTypeSO;
    }

    //return position of mouse by the camera with tag MainCamera
    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = turretCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 9990f))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
    
    //create line for grid and also put them in to a gameobject
    public void CreateLine(int width, int height, float space, Vector3 origin, GameObject lineParent)
    {
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
            lr.SetPosition(0, new Vector3(origin.x, origin.y, origin.z +i * space) );
            lr.SetPosition(1, new Vector3(origin.x + width * space,  origin.y, origin.z +i * space));
            
            gridLines.transform.SetParent(lineParent.transform);
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
            lr.SetPosition(0, new Vector3(i * space + origin.x, origin.y, origin.z));
            lr.SetPosition(1, new Vector3(i * space+ origin.x,  origin.y, height * space +origin.z));
            gridLines.transform.SetParent(lineParent.transform);
        }
    }
}
