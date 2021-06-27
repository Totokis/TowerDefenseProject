using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest {CustomId = SystemInfo.deviceUniqueIdentifier,CreateAccount = true};
        PlayFabClientAPI.LoginWithCustomID(request,OnSucces,OnError);
    }
    void OnSucces(LoginResult obj)
    {
        Debug.Log("Successful login");
    }
    void OnError(PlayFabError obj)
    {
        Debug.Log("Error while logging");
        Debug.Log(obj.GenerateErrorReport());
    }

}
