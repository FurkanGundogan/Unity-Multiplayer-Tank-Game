using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class UnitBase : NetworkBehaviour
{
    [SerializeField] private Health health = null;
    

    public static event Action<UnitBase> ServerOnBaseSpawn;
    public static event Action<UnitBase> ServerOnBaseDeSpawn;
    public static event Action<int> ServerOnPlayerDie;

    #region Server

    public override void OnStartServer()
    {
        health.ServerOnDie += ServerHandleDie;

        ServerOnBaseSpawn?.Invoke(this);
    }

    public override void OnStopServer()
    {
        ServerOnBaseDeSpawn?.Invoke(this);
        health.ServerOnDie -= ServerHandleDie;

    }

    private void ServerHandleDie()
    {
        ServerOnPlayerDie?.Invoke(connectionToClient.connectionId);
        NetworkServer.Destroy(gameObject);
    }
    #endregion

    #region Client
    #endregion
}
