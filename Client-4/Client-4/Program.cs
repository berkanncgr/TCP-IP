using System;
using System.Net.Sockets;
using System.Threading;

namespace Client_4
{
    class Program
    {
        static int a, b, c, d;
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t***Client-4***");
            Console.Write("Bağlanmak istediğiniz IP adresini tuşlayınız (localhost için 0 tuşlayın) :    ");
            string IP = Console.ReadLine();
            if (IP == "0")
                IP = "127.0.0.1";

            //Console.WriteLine("A - B - C - D  değerlerini giriniz... ");
            //Console.Write("A :   "); a = Convert.ToInt32(Console.ReadLine());
            //Console.Write("B :   "); b = Convert.ToInt32(Console.ReadLine());
            //Console.Write("C :   "); c = Convert.ToInt32(Console.ReadLine());
            //Console.Write("D :   "); d = Convert.ToInt32(Console.ReadLine());
            do
            {
                Console.WriteLine("A - B - C - D  değerlerini giriniz... ");
                Console.Write("A :   "); a = Convert.ToInt32(Console.ReadLine());
                Console.Write("B :   "); b = Convert.ToInt32(Console.ReadLine());
                Console.Write("C :   "); c = Convert.ToInt32(Console.ReadLine());
                Console.Write("D :   "); d = Convert.ToInt32(Console.ReadLine());
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
                //NetworkStream stream = client.GetStream();
                int count = 0;
                Byte[] A = System.Text.Encoding.ASCII.GetBytes(a.ToString());
                Byte[] B = System.Text.Encoding.ASCII.GetBytes(b.ToString());
                Byte[] C = System.Text.Encoding.ASCII.GetBytes(c.ToString());
                Byte[] D = System.Text.Encoding.ASCII.GetBytes(d.ToString());
                while (count < 4)
                {
                    TcpClient client = new TcpClient(server, 4004);
                    NetworkStream stream = client.GetStream();
                    switch (count)
                    {
                        case 0:
                            stream.Write(A, 0, A.Length);
                            break;
                        case 1:
                            stream.Write(B, 0, B.Length);
                            break;
                        case 2:
                            stream.Write(C, 0, C.Length);
                            break;
                        case 3:
                            stream.Write(D, 0, D.Length);
                            break;
                    }
                    Thread.Sleep(2000);
                    stream.Close();
                    client.Close();
                    count++;
                }
                Console.WriteLine("Gönderilen Veriler :  \nA = "+a+"\nB = "+b+"\nC = "+c+"\nD = "+d);
                Console.Write("\nVeri göndermek için ENTER tuşuna basın     -     Çıkış için herhangi bir tuşa basın ");
                //stream.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("!!!Client-4 BAĞLANTI HATASI!!!");
            }
            //Console.Read();
        }
    }
}
