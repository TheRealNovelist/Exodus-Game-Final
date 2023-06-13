using UnityEngine;

public enum SearchComponentMode
{
    Any,
    IncludeChild,
    IncludeParent
}

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
    
    public static bool TryGetComponent<T>(this GameObject gameObject, SearchComponentMode mode, out T component)
    {
        var tempComp = gameObject.GetComponent<T>();
        if (tempComp != null)
        {
            component = tempComp;
            return true;
        }
        
        if (mode is SearchComponentMode.Any or SearchComponentMode.IncludeParent)
        {
            tempComp = gameObject.GetComponentInParent<T>();
            if (tempComp != null)
            {
                component = tempComp;
                return true;
            }
        }

        if (mode is SearchComponentMode.Any or SearchComponentMode.IncludeChild)
        {
            tempComp = gameObject.GetComponentInChildren<T>();
            if (tempComp != null)
            {
                component = tempComp;
                return true;
            }
        }

        component = default;
        return false;
    }
}