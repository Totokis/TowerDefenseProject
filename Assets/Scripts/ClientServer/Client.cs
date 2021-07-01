using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Client : MonoBehaviour
{
   public static Client Instance;
   public static int dataBufferSize = 4096;
   public string ip = "127.0.0.1";
   public int port = 26950;
   public int myId = 0;
   public TCP tcp;
   public UDP udp;

   delegate void PacketHandler(Packet packet);

   private static Dictionary<int, PacketHandler> _packetHandlers;

   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(this);
      }
   }

   private void Start()
   {
      tcp = new TCP();
      udp = new UDP();
   }

   public void ConnectToServer()
   {
      InitializeClientData();
      tcp.Connect();
   }

   public class TCP
   {
      public TcpClient socket;
      private NetworkStream _stream;
      private byte[] receiveBuffer;
      private Packet receivedData;

      public void Connect()
      {
         socket = new TcpClient
         {
            ReceiveBufferSize = dataBufferSize,
            SendBufferSize = dataBufferSize
         };

         receiveBuffer = new byte[dataBufferSize];
         socket.BeginConnect(Instance.ip, Instance.port, ConnectCallback, socket);
      }
      private void ConnectCallback(IAsyncResult result)
      {
         socket.EndConnect(result);
         if (!socket.Connected)
         {
            Debug.Log("Client not connected");
            return;
         }
         _stream = socket.GetStream();
         receivedData = new Packet();
         Debug.Log("Client connected");
         _stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
      }

      private void ReceiveCallback(IAsyncResult result)
      {
         try
         {
            int byteLength = _stream.EndRead(result);
            if (byteLength <= 0)
            {
               //Disconnect
               Debug.Log("Client disconnected");
               return;
            }
            Debug.Log("Client connected");
            byte[] data = new byte[byteLength];
            Array.Copy(receiveBuffer,data,byteLength);
            receivedData.Reset(HandleData(data));
            _stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
         }
         catch (Exception e)
         {
            Console.WriteLine($"ReceiveCallback error: {e}");
            throw;
         }
      }
      private bool HandleData(byte[] data)
      {
         int packetLength = 0;
         receivedData.SetBytes(data);
         
         if (receivedData.UnreadLength() >= 4)
         {
            packetLength = receivedData.ReadInt();
            if (packetLength <= 0)
            {
               return true;
            }
         }

         while (packetLength > 0 && packetLength <= receivedData.UnreadLength())
         {
            byte[] packetBytes = receivedData.ReadBytes(packetLength);
            ThreadManager.ExecuteOnMainThread(() => {
               using(Packet packet = new Packet(packetBytes))
               {
                  int packetId = packet.ReadInt();
                  _packetHandlers[packetId](packet);
               }
            });
            packetLength = 0;
            
            if (receivedData.UnreadLength() >= 4)
            {
               packetLength = receivedData.ReadInt();
               if (packetLength <= 0)
                  return true;
            }

            if (packetLength <= 1)
               return true;
         }
         return false;
      }
      public void SendData(Packet packet)
      {
         try
         {
            if (socket != null)
            {
               _stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
            }
         }
         catch (Exception e)
         {
            Console.WriteLine($"Error sending data to server via TCP: {e}");
            throw;
         }
      }
   }

   public class UDP
   {
      public UdpClient socket;
      public IPEndPoint endPoint;

      public UDP()
      {
         endPoint = new IPEndPoint(IPAddress.Parse(Instance.ip), Instance.port);
      }
      public void Connect(int localPort)
      {
         socket = new UdpClient(localPort);
         socket.Connect(endPoint);
         socket.BeginReceive(ReveiveCallback, null);

         using (Packet packet = new Packet())
         {
            SendData(packet);
         }
      }

      public void SendData(Packet packet)
      {
         try
         {
            packet.InsertInt(Instance.myId);
            if (socket!= null)
            {
               socket.BeginSend(packet.ToArray(), packet.Length(), null, null);
            }
         }
         catch (Exception e)
         {
            Debug.Log($"Error sending data to server via UDP: {e}");
            throw;
         }
      }
      private void ReveiveCallback(IAsyncResult result)
      {
         try
         {
            byte[] data = socket.EndReceive(result, ref endPoint);
            socket.BeginReceive(ReveiveCallback, null);

            if (data.Length < 4)
            {
               return;
            }
            HandleData(data);
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
            throw;
         }
      }
      private void HandleData(byte[] data)
      {
         using (Packet packet = new Packet(data))
         {
            int packetLength = packet.ReadInt();
            data = packet.ReadBytes(packetLength);
            
            ThreadManager.ExecuteOnMainThread(() => {
               using (Packet packet = new Packet(data))
               {
                  int packetId = packet.ReadInt();
                  _packetHandlers[packetId](packet);
               }
            });
         }
         
      }
   }
   void InitializeClientData()
   {
      _packetHandlers = new Dictionary<int, PacketHandler>
      {
         { (int)ServerPackets.Welcome, ClientHandle.Welcome },
         {(int)ServerPackets.SpawnPlayer,ClientHandle.SpawnPlayer},
      };

      Debug.Log("Initialize packets");
   }

}
