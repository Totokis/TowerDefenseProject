using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
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
            player = Instantiate(localPlayerPrefab, positon, rotation);
        }
        else
        {
            player = Instantiate(playerPrefab, positon, rotation);
        }

        player.GetComponent<PlayerManager>().id = id;
        player.GetComponent<PlayerManager>().username = username;
        players.Add(id,player.GetComponent<PlayerManager>());
    }
    
}
