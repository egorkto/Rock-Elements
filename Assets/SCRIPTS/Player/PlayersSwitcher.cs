using DG.Tweening;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayersSwitcher : MonoBehaviour
{
    public event Action AllPlayersDied;

    [SerializeField] private List<Player> players = new List<Player>();
    [SerializeField] private PlayerPresenter _presenter;
    [SerializeField] private float _swapSpeed;

    private List<Vector3> points = new List<Vector3>();

    private int _currentPlayerIndex;
    private Player _currentPlayer;

    private void OnEnable()
    {
        Player.Died += ExcludePlayer;
    }

    private void OnDisable()
    {
        Player.Died += ExcludePlayer;
    }

    private void ExcludePlayer(Player player)
    {
        AllPlayersDied?.Invoke();
    }

    private void Start()
    {
        _currentPlayer = players[0];
        _presenter.SetHealth(_currentPlayer.Health);
        foreach (Player player in players)
        {
            points.Add(player.transform.localPosition);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            SwitchPlayer();
    }

    private void SwitchPlayer()
    {
        _currentPlayerIndex = (_currentPlayerIndex + 1) % players.Count;
        _currentPlayer = players[_currentPlayerIndex];
        _presenter.SetHealth(_currentPlayer.Health);
        for (int i = 0; i < players.Count; i++)
        {
            players[(_currentPlayerIndex + i) % players.Count].transform.DOLocalMove(points[(i) % points.Count], _swapSpeed, false);
        }
    }

    public void DamageCurrentPlayer(int damage)
    {
        _currentPlayer.Health.TakeDamage(damage);
        _presenter.PresentHealth();
    }
}
