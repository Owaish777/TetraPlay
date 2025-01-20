using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



[System.Serializable]
public struct Player
{
    public string name;
    public string password;
    public string id;

    Player(string name , string password){
        this.name = name;
        this.password = password;
        id = null;
    }

}


[System.Serializable]
public struct Score
{
    public string playerId;
    public string gameId;
    public string id;

    Score(string playerId, string gameId)
    {
        this.playerId = playerId;
        this.gameId = gameId;
        id = null;
    }

}