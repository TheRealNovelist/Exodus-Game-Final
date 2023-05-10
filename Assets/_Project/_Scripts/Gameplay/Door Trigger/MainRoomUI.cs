using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainRoomUI : MonoBehaviour
{
    [SerializeField] private GameObject waveWarning;
    [SerializeField] private TextMeshProUGUI waveStatText;
    
    public void ToggleStats(bool show)
    {
        waveStatText.gameObject.SetActive(show);
    }

    public void ToggleWarning(bool show)
    {
        waveWarning.SetActive(show);
    }

    public void WarningActivate(bool show)
    {
        if(!waveWarning.gameObject.activeSelf) {return;}
        
        //PLACE HOLDER
        waveWarning.GetComponent<Image>().enabled = show;
    }

    public void UpdateWaveStats(ESpawnerSystem spawnerSystem)
    {
        waveStatText.text = $"{spawnerSystem.Spawned}/{spawnerSystem.TotalToSpawn}";
    }
    
    public void UpdateWaveStats()
    {
        waveStatText.text = $"";
    }
    
}
