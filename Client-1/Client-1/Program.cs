using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace Client_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            Console.WriteLine("\t\t\t***Client-1***");
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

            //Console.ReadLine();
        }
        static void Connect(String server)
        {
            int x = 3, y = 4, z = 2;
            try
            {

                //NetworkStream stream = client.GetStream();
                int count = 0;

                Byte[] X = System.Text.Encoding.ASCII.GetBytes(x.ToString());
                Byte[] Y = System.Text.Encoding.ASCII.GetBytes(y.ToString());
                Byte[] Z = System.Text.Encoding.ASCII.GetBytes(z.ToString());
                while (count < 3)
                {
                    
                    TcpClient client = new TcpClient(server, 4001);
                    NetworkStream stream = client.GetStream();
                    switch (count)
                    {
                        case 0:
                            stream.Write(X, 0, X.Length);
                            break;
                        case 1:
                            stream.Write(Y, 0, Y.Length);
                            break;
                        case 2:
                            stream.Write(Z, 0, Z.Length);
                            break;
                    }
                    Thread.Sleep(1);
                    
                    stream.Close();
                    client.Close();

                    count++;
                }
                //stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("!!!Client-1 BAĞLANTI HATASI!!!");
            }
            Console.WriteLine("Gönderilen Veriler :  \nX = " + x + "\nY = " + y + "\nZ = " + z);
            
            Console.Write("\nVeri göndermek için ENTER tuşuna basın     -     Çıkış için herhangi bir tuşa basın ");
            //Console.Read();
        }
    }
}
