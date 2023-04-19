using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;
    public CoinManager CoinManager;
    public CursorAndCam CursorAndCam;
    public Shop Shop;
    public Inventory Inventory;
    public PlayerCam PlayerCamera;
    public PlayerMovement Player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
