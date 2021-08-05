using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    [SerializeField] GameObject localPlayerPrefab;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject femalePrefab;
    [SerializeField] GameObject femaleLocalPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SpawnPlayer(int id, string username, Vector3 positon, Quaternion rotation)
    {
        GameObject player;
        if (id == Client.Instance.myId)
        {
            if (username.LastOrDefault() == 'a')
            {
                player = Instantiate(femaleLocalPrefab, positon, rotation);
            }
            else
            {
                player = Instantiate(localPlayerPrefab, positon, rotation);
            }
           
        }
        else
        {
            if (username.LastOrDefault() == 'a')
            {
                player = Instantiate(femalePrefab, positon, rotation);
            }
            else
            {
                player = Instantiate(playerPrefab, positon, rotation);
            }
            
        }

        player.GetComponent<PlayerManager>().Initialize(id,username);
        players.Add(id,player.GetComponent<PlayerManager>());
        Debug.Log($" {id} Player was spawned at: {DateTime.Now.ToShortTimeString()}, and his name is {username} !");
    }
    
}
