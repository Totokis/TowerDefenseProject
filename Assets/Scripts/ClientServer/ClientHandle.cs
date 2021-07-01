using System.Collections;
using System.Collections.Generic;
using System.Net;
using kcp2k;
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
   public static void UdpTest(Packet packet)
   {
      string msg = packet.ReadString();

      Debug.Log($"Received packet via UDP. Contains message: {msg}");
      ClientSend.UdpTestReceived();
   }
}
