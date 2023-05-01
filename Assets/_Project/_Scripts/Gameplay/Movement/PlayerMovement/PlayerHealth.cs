using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IHeal
{
    [SerializeField] float maxHealth;
    private float _playerHealth;
    
    private void Awake()
    {
        PlayerFullHealth();
        RespawnPlayer.OnPlayerFinishedRespawn += PlayerFullHealth;
    }
    
    public void Damage(float amount)
    {
        _playerHealth -= amount;

        if (_playerHealth <= 0f)
        {
            Debug.Log("Player Died!");
            RespawnPlayer.OnPlayerStartRespawn?.Invoke();
            return;
        }
        
        Debug.Log("Player Health: " + _playerHealth);
    }

    public void AddHealth(float amount)
    {
        _playerHealth += amount;
        if (_playerHealth > maxHealth)
            _playerHealth = maxHealth;
    }

    private void PlayerFullHealth()
    {
        _playerHealth = maxHealth;
    }
}
