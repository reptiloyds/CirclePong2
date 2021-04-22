using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EventAggregator : MonoBehaviour
{
    public event Action LoseGame;
    public event EventHandler<WinGameEventArgs> WinGame;
    public event Action GetDamage;
    public event Action Explosion;
    public event Action RaimbowBrick;

    public static EventAggregator instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    public void OnExplosion()
    {
        Explosion?.Invoke();
    }
    public void OnLoseGame()
    {
        LoseGame?.Invoke();
    }
    public void OnRaimbowBlock()
    {
        RaimbowBrick?.Invoke();
    }
    public void OnWinGame(int playerHealthPoint)
    {
        WinGameEventArgs winGame = new WinGameEventArgs { HealthPoint = playerHealthPoint };
        WinGame?.Invoke(this, winGame);
    }
    public void OnPlayerGetDamage()
    {
        GetDamage?.Invoke();
    }
}
public class WinGameEventArgs : EventArgs
{
    public int HealthPoint { get; set; }
}