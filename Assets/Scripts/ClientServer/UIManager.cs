using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   public static UIManager Instance;
   
   [SerializeField] Button connect;
   [SerializeField] Configurator configurator;
   [SerializeField] GameObject startMenu;
   [SerializeField] public InputField usernameField;
   
   
   void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         connect.onClick.AddListener(ConnectToServer);
      }
      else
      {
         Destroy(this);
      }
   }

   void ConnectToServer()
   {
      startMenu.SetActive(false);
      usernameField.interactable = false;
      if (configurator.IsRemote)
      {
         configurator.StartRemoteLogin();
      }
      else
      { 
         Client.Instance.ConnectToServer();
      }
      
      
   }
}
