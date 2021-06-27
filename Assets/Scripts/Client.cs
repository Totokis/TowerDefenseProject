using System;
using System.Collections;
using System.Collections.Generic;
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
   }

   public void ConnectToServer()
   {
      throw new NotImplementedException();
   }

   public class TCP
   {
      public TcpClient socket;
      private NetworkStream _stream;
      private byte[] receiveBuffer;

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
            return;
         }
         _stream = socket.GetStream();

         _stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
      }

      private void ReceiveCallback(IAsyncResult result)
      {
         try
         {

         }
         catch (Exception e)
         {
            Console.WriteLine(e);
            throw;
         }
      }
   }

}
