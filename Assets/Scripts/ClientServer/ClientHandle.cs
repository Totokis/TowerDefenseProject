using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
   public static void Welcome(Packet packet)
   {
      string message = packet.ReadString();
      int myId = packet.ReadInt();

      Debug.Log($"Message from the server: {message}");
      Client.Instance.myId = myId;
      ClientSend.WelcomeReceived();

      Client.Instance.udp.Connect(((IPEndPoint)Client.Instance.tcp.socket.Client.LocalEndPoint).Port);
   }

   public static void SpawnPlayer(Packet packet)
   {
       int id = packet.ReadInt();
       string username = packet.ReadString();
       Vector3 position = packet.ReadVector3();
       Quaternion rotation = packet.ReadQuaternion();
       GameManager.instance.SpawnPlayer(id,username,position,rotation);
   }
   
}
