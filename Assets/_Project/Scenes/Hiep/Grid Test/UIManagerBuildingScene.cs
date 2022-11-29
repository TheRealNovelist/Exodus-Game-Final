using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManagerBuildingScene : MonoBehaviour
{
    [SerializeField] private List<PlacedObjectTypeSO> placeObjectTypeSOList = null;
    
    [SerializeField] private TMP_Text turret1PriceText;
    [SerializeField] private TMP_Text turret2PriceText;
    [SerializeField] private TMP_Text turret3PriceText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turret1PriceText.SetText("Money " + placeObjectTypeSOList[0].price.ToString());
        turret2PriceText.SetText("Money " + placeObjectTypeSOList[1].price.ToString());
        turret3PriceText.SetText("Money " + placeObjectTypeSOList[2].price.ToString());
    }
}
