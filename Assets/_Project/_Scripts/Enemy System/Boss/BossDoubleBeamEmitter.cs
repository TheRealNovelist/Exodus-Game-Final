using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class BossDoubleBeamEmitter : MonoBehaviour
{
    [Header("Weapons")] 
    [SerializeField] private BossBeamEmitter rightWeapon;
    [SerializeField] private BossBeamEmitter leftWeapon;
    
    [SerializeField] private int spinAttackTurns = 3;
    [SerializeField] private float spinRate = 1f;

    [SerializeField] private float weaponRotateTime = 2f;

    float currentAngle = 0;
    
    public IEnumerator RotateWeaponHorizontal()
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.Insert(0, rightWeapon.transform.DOLocalRotate(new Vector3(0, 90f, 0), weaponRotateTime));
        sequence.Insert(0, leftWeapon.transform.DOLocalRotate(new Vector3(0, -90f, 0), weaponRotateTime));

        sequence.Play();

        yield return sequence.WaitForCompletion();
    }

    public IEnumerator RotateWeaponFront()
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.Insert(0, rightWeapon.transform.DOLocalRotate(new Vector3(0, 0, 0), weaponRotateTime));
        sequence.Insert(0, leftWeapon.transform.DOLocalRotate(new Vector3(0, 0, 0), weaponRotateTime));

        sequence.Play();

        yield return sequence.WaitForCompletion();
    }

    public void StartSpinAttack()
    {
        currentAngle = 0;
    }
    
    public bool SpinAttack()
    {
        float totalAngle = spinAttackTurns * 360f;
        
        if (currentAngle < totalAngle)
        {
            transform.Rotate(0, spinRate, 0);

            leftWeapon.Attack();
            rightWeapon.Attack();

            currentAngle += spinRate;
            return false;
        }
        
        return true;
    }

    public void EndSpinAttack()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        
        leftWeapon.StopAttack();
        rightWeapon.StopAttack();
    }
}
