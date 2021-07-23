using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   public static UIManager Instance;

   [SerializeField] private Button connect;
   [SerializeField] GameObject startMenu;
   [SerializeField] public TMP_InputField usernameField;


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

      Client.Instance.ConnectToServer();
   }
}
