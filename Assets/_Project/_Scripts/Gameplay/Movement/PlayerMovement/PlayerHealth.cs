using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

//TAKE DAMAGE AND HEAL SOUND

public class PlayerHealth : MonoBehaviour, IDamageable, IHeal
{
    [SerializeField] float maxHealth;
    public float _playerHealth;

    [SerializeField] private MMF_Player feedback;
    [SerializeField] private AudioManager _audioManager;

    private void Awake()
    {
        PlayerFullHealth();
        RespawnPlayer.OnPlayerFinishedRespawn += PlayerFullHealth;
    }
    
    public void Damage(float amount, Transform source = null)
    {
        _audioManager.PlayOneShot("PlayerTakeDamage");
        feedback.PlayFeedbacks();
        
        _playerHealth -= amount;

        if (_playerHealth <= 0f)
        {
            Debug.Log("Player Died!");
            _audioManager.PlayOneShot("PlayerRespawn");
            RespawnPlayer.OnPlayerStartRespawn?.Invoke();
            return;
        }
        
//        Debug.Log("Player Health: " + _playerHealth);
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
