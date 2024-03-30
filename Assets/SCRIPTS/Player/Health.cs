using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event Action Died;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    [SerializeField] private int _maxHealth;

    private int _currentHealth;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if(_currentHealth < 0)
            Died?.Invoke();
    }

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
}