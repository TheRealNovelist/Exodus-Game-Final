using UnityEngine;

public static class Extension
{
    public static void RotateTowards(this Transform transform, Transform target, float turnSpeed = 1f, bool freezeX = false, bool freezeY = false, bool freezeZ = false)
    {
        Quaternion tmpRotation = transform.localRotation;
        Vector3 targetPointTurret = (target.transform.position - transform.position).normalized;
        Quaternion targetRotationTurret = Quaternion.LookRotation(targetPointTurret, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationTurret, turnSpeed);

        float localX = freezeX ? tmpRotation.eulerAngles.x : transform.localRotation.eulerAngles.x;
        float localY = freezeY ? tmpRotation.eulerAngles.y : transform.localRotation.eulerAngles.y;
        float localZ = freezeZ ? tmpRotation.eulerAngles.z : transform.localRotation.eulerAngles.z;
        
        transform.localRotation = Quaternion.Euler(localX, localY, localZ);
    }
}