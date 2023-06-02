using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{

    [SerializeField] private TMP_Text winnerNameText = null;
    [SerializeField] private GameObject gameOverDisplayParent = null;
  
    void Start()
    {
        GameOverHandler.ClientOnGameOver +=ClientHandleGameOver;
    }

    private void OnDestroy() {
        GameOverHandler.ClientOnGameOver -=ClientHandleGameOver;
    }

    public void LeaveGame(){
        if(NetworkServer.active && NetworkClient.isConnected){
            // stop hosting
            NetworkManager.singleton.StopHost();
        }else{
            // stop client
            NetworkManager.singleton.StopClient();
        }
    }

    private void ClientHandleGameOver(string winner){
        winnerNameText.text=$"{winner} Has Won";
        gameOverDisplayParent.SetActive(true);
    }
    
}
