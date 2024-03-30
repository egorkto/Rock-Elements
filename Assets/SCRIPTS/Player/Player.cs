using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action<Player> Died;

    public Health Health => _health;

    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Died += () => Died?.Invoke(this);
    }

    private void OnDisable()
    {
        _health.Died -= () => Died?.Invoke(this);
    }
}