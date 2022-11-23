using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IHeal
{
    [SerializeField] float maxHealth;
    
    public float _playerHealth;

    private void Awake()
    {
        _playerHealth = maxHealth;
    }
    
    public void Damage(float amount)
    {
        if (_playerHealth <= 0f)
        {
            Debug.Log("Player Died!");
            return;
        }
        
        _playerHealth -= amount;
        Debug.Log("Player Health: " + _playerHealth);
    }

    public void AddHealth(float amount)
    {
        _playerHealth += amount;
        if (_playerHealth > maxHealth)
            _playerHealth = maxHealth;
    }
}
