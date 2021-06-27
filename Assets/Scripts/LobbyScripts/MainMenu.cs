using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainMenu : MonoBehaviour
{
    [SerializeField] NetworkManagerLobby networkManager = null;
    
    [Header("UI")]
    [SerializeField] GameObject landingPagePanel = null;

    public void HostLobby()
    {
        networkManager.StartHost();
        landingPagePanel.SetActive(false);
    }
}
