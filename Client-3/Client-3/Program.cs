using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client_3
{
    class Program
    {
        static double angle, kenar1, kenar2;
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t***Client-3***");
            Console.Write("Bağlanmak istediğiniz IP adresini tuşlayınız (localhost için 0 tuşlayın) :    ");
            string IP = Console.ReadLine();

            if (IP == "0")
                IP = "127.0.0.1";

            do
            {
                Console.WriteLine("Alfa ve kenar değerlerini giriniz... ");
                Console.Write("Alfa :   "); angle = Convert.ToDouble(Console.ReadLine());
                Console.Write("1. Kenar :   "); kenar1 = Convert.ToDouble(Console.ReadLine());
                Console.Write("2. Kenar :   "); kenar2 = Convert.ToDouble(Console.ReadLine());
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
            TcpClient client = new TcpClient(server, 4003);
            NetworkStream stream = client.GetStream();
            String response = String.Empty;
            try
            {
                int count = 0;
                Byte[] alfa = Encoding.ASCII.GetBytes(angle.ToString());
                Byte[] b = Encoding.ASCII.GetBytes(kenar1.ToString());
                Byte[] c = Encoding.ASCII.GetBytes(kenar2.ToString());

                while (count < 3)
                {

                    switch (count)
                    {
                        case 0:
                            stream.Write(alfa, 0, alfa.Length);
                            break;
                        case 1:
                            stream.Write(b, 0, b.Length);
                            break;
                        case 2:
                            stream.Write(c, 0, c.Length);
                            break;
                    }

                    
                    Byte[] data = new Byte[500];
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    response = Encoding.ASCII.GetString(data, 0, bytes);

                    Thread.Sleep(2000);

                    count++;
                }
                
                Console.WriteLine("\nGönderilen veriler :\nAlfa: " + angle + "\n1. Kenar: " + kenar1 + "\n2. Kenar: " + kenar2);
                Console.WriteLine("\nSunucudan Gelen Veri : "+response);
                Console.Write("\nVeri göndermek için ENTER tuşuna basın     -     Çıkış için herhangi bir tuşa basın ");

                stream.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("!!!BAĞLANTI HATASI!!!");
            }

        }
        //public static void ListenServer(object obj)
        //{
        //    TcpClient client = (TcpClient)obj;
        //    var stream = client.GetStream();
        //    try
        //    {
        //        String response = String.Empty;

        //        Byte[] data = new Byte[500];
        //        Int32 bytes = stream.Read(data, 0, data.Length);
        //        response = Encoding.ASCII.GetString(data, 0, bytes);
        //        Console.WriteLine(response);
        //        Thread.Sleep(2000);

        //        //stream.Close();
        //        //client.Close();

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("!!!BAĞLANTI HATASI!!!" + e.ToString());
        //    }
        //}
    }
}
