using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
   static void SendTCPData(Packet packet)
   {
      packet.WriteLength();
      Client.Instance.tcp.SendData(packet);
   }  

   static void SendUdpData(Packet packet)
   {
      packet.WriteLength();
      Client.Instance.udp.SendData(packet);
   }
   public static void WelcomeReceived()
   {
      using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
      {
         packet.Write(Client.Instance.myId);
         packet.Write(UIManager.Instance.usernameField.text);
         
         SendTCPData(packet);
      }
   }
   public static void PlayerMovement(bool[] inputs)
   {
      using (Packet packet = new Packet((int)ClientPackets.playerMovement))
      {
         packet.Write(inputs.Length);
         foreach (var input in inputs)
         {
            packet.Write(input);
         }
         packet.Write(GameManager.players[Client.Instance.myId].transform.rotation);
         SendUdpData(packet);
      } 
   }
}
