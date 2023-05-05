using UnityEngine;

public interface IDamageable 
{
    public void Damage(float amount, Transform source = null);
}

public interface IHeal
{
    public void AddHealth(float amount);
}

