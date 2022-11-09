using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this one is no good
public class MakeCameraRecoil : MonoBehaviour
{
   //Rotations
   private Vector3 currentRotation;
   private Vector3 targetRotation;
   
   //Hipfire Recoil
   [SerializeField] private float recoilX;
   [SerializeField] private float recoilY;
   [SerializeField] private float recoilZ;
   
   //Settings
   [SerializeField] private float snappingness;
   [SerializeField] private float returnSpeed;
   
    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappingness * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
}
