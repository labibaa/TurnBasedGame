using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class PlayerNetworkManager : NetworkBehaviour
{

    public static PlayerNetworkManager instance;
    public static event Action OnPlayerJoin;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    private NetworkVariable<int> playersInGame = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);



    public int PlayersInGame
    {
        get
        {
            return playersInGame.Value;
        }
    }






    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) => {

            
                OnPlayerJoin?.Invoke();
                Debug.Log($"{id} just connected");
                playersInGame.Value++;
            
        
        };
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {

            
                OnPlayerJoin?.Invoke();
                Debug.Log($"{id} just Disconnected");
                playersInGame.Value--;
            
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
