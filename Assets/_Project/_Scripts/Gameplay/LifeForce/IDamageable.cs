using UnityEngine;

public interface IDamageable 
{
    void Damage(float amount, Transform source = null);
}

public interface IHeal
{
    void AddHealth(float amount);
}

