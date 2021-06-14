using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t***Client-2***");
            Console.Write("Bağlanmak istediğiniz IP adresini tuşlayınız (localhost için 0 tuşlayın) :    ");
            string IP = Console.ReadLine();
            if (IP == "0")
                IP = "127.0.0.1";
            do
            {
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Connect(IP);
                }).Start();
            }
            while ("".Equals(Console.ReadLine()));
        }

        static void Connect(String server)
        {

            try
            {
                TcpClient client = new TcpClient(server, 4002);
                NetworkStream stream = client.GetStream();
                int count = 0;
                while (count++ < 2)
                {
                    Byte[] data = new Byte[256];
                    String response = String.Empty;

                    Int32 bytes = stream.Read(data, 0, data.Length);
                    response = Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine(response);
                    //Thread.Sleep(2000);
                }
                Console.Write("\nVeri göndermek için ENTER tuşuna basın     -     Çıkış için herhangi bir tuşa basın ");
                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("!!!BAĞLANTI HATASI!!!");
            }
            

            //Console.Read();
        }
    }
}
