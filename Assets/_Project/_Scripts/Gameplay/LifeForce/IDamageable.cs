using UnityEngine;

public interface IDamageable
{
    Transform transform { get; }

    void Damage(float amount, Transform source = null);
}

public interface IHeal
{
    void AddHealth(float amount);
}

