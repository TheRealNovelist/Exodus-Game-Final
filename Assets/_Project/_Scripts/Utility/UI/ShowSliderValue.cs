using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Display number value next to slider in percentage.
//Code adapt from @Nitin43320280
public class ShowSliderValue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numTxt;
    // Start is called before the first frame update
    void Start()
    {
        numTxt = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateValue(float v)
    {
        numTxt.text = Mathf.RoundToInt(v * 100) + "%";
    }
}
