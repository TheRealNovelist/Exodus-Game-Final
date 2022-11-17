using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickUp2 : MonoBehaviour
{
    [SerializeField] private Transform shotgun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    //This function will put the gun we pass in to the first position, SiblingIndex = 0
    public void PutToTheTop(Transform gun)
    {
        gun.transform.SetSiblingIndex(0);
    }
    //This function will put the gun we pass in to the second position, SiblingIndex = 1
    public void PutAtSecondPlace(Transform gun)
    {
        gun.transform.SetSiblingIndex(1);
    }
}
