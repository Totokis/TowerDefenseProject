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
       Debug.Log($"Spawning player with id: {id}");
       string username = packet.ReadString();
       Vector3 position = packet.ReadVector3();
       Quaternion rotation = packet.ReadQuaternion();
       GameManager.instance.SpawnPlayer(id,username,position,rotation);
   }

   public static void PlayerPosition(Packet packet)
   {
       int id = packet.ReadInt();
       Vector3 position = packet.ReadVector3();
       if(GameManager.players.ContainsKey(id))
            GameManager.players[id].transform.position = position;
   }
   public static void PlayerRotation(Packet packet)
   {
       int id = packet.ReadInt();

       Quaternion rotation = packet.ReadQuaternion();
       
       if(GameManager.players.ContainsKey(id))
            GameManager.players[id].transform.rotation = rotation;
   }

   public static void PlayerDisconnected(Packet packet)
   {
       int id = packet.ReadInt();
       
       Destroy(GameManager.players[id].gameObject);
       GameManager.players.Remove(id);
   }
   
   public static void PlayerHealth(Packet packet)
   {
       int id = packet.ReadInt();
       float health = packet.ReadFloat();
       Debug.Log($"Players {id} health: {health}");
       GameManager.players[id].Health = health;
   }
   
   public static void PlayerRespawned(Packet packet)
   {
       int id = packet.ReadInt();
       
       GameManager.players[id].Respawn();
   }
}
