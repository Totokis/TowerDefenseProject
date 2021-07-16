using System.Collections.Generic;
// using PlayFab;
// using PlayFab.ClientModels;
// using PlayFab.MultiplayerModels;
using UnityEngine;

public class Configurator : MonoBehaviour
{
   [SerializeField] bool isRemote = false;
   [SerializeField] string buildId;
   [SerializeField] string ipAddress;
   [SerializeField] int port;

   public bool IsRemote => isRemote;
   
   public void StartRemoteLogin()
   {
       //LoginRemoteUser();
   }
   // void LoginRemoteUser()
   // {
   //     Debug.Log("[ClientStartUp].LoginRemoteUser");
		 //
   //     //We need to login a user to get at PlayFab API's. 
   //     Debug.Log($"TITLE ID: {PlayFabSettings.TitleId}");
   //      LoginWithCustomIDRequest request = new LoginWithCustomIDRequest()
   //      {
   //          TitleId = PlayFabSettings.TitleId,
   //          CreateAccount = true,
   //          CustomId = SystemInfo.deviceUniqueIdentifier,
   //      };
   //     
   //      PlayFabClientAPI.LoginWithCustomID(request, OnPlayFabLoginSuccess, OnLoginError);
   // }
   //
   // void OnLoginError(PlayFabError response)
   // {
   //     Debug.Log(response.ToString());
   // }
   //
   // void OnPlayFabLoginSuccess(LoginResult response)
   // {
   //     Debug.Log(response.ToString());
   //     if (ipAddress == "")
   //     {   //We need to grab an IP and Port from a server based on the buildId. Copy this and add it to your Configuration.
   //         RequestMultiplayerServer(); 
   //     }
   //     else
   //     {
   //         ConnectRemoteClient();
   //     }
   // }
   //
   // void RequestMultiplayerServer()
   // {
   //     Debug.Log("[ClientStartUp].RequestMultiplayerServer");
   //     RequestMultiplayerServerRequest requestData = new RequestMultiplayerServerRequest();
   //     requestData.BuildId = buildId;
   //     requestData.SessionId = System.Guid.NewGuid().ToString();
   //     requestData.PreferredRegions = new List<string>() { "NorthEurope" };
   //     PlayFabMultiplayerAPI.RequestMultiplayerServer(requestData, OnRequestMultiplayerServer, OnRequestMultiplayerServerError);
   // }
   //
   // void ConnectRemoteClient(RequestMultiplayerServerResponse response = null)
   // {
   //     if(response == null)
   //     {
   //         Client.Instance.ip = ipAddress;
   //         Client.Instance.port = port;
   //     }
   //     else
   //     {
   //         Debug.Log("**** ADD THIS TO YOUR CONFIGURATION **** -- IP: " + response.IPV4Address + " Port: " + (ushort)response.Ports[0].Num);
   //         Client.Instance.ip = response.IPV4Address;
   //         Client.Instance.port = (ushort)response.Ports[0].Num;
   //     }
   //
   //     Client.Instance.ConnectToServer();
   // }
   //
   // void OnRequestMultiplayerServer(RequestMultiplayerServerResponse response)
   // {
   //     Debug.Log(response.ToString());
   //     ConnectRemoteClient(response);
   // }
   //
   // void OnRequestMultiplayerServerError(PlayFabError error)
   // {
   //     Debug.Log(error.ErrorDetails);
   // }
}
