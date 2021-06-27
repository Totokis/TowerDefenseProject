using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   public static UIManager Instance;
   
   [SerializeField] Button connect;
   [SerializeField] GameObject startMenu;
   [SerializeField] InputField usernameField;
   
   
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

   public void ConnectToServer()
   {
      startMenu.SetActive(false);
      usernameField.interactable = false;
      Client.Instance.ConnectToServer();
   }
}
