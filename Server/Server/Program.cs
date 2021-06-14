using System;
using System.Net;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\t\t\t***HOŞGELDİNİZ***\n");
            ServerCode myserver;
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                myserver = new ServerCode("127.0.0.1", 4001);
            }).Start();
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                myserver = new ServerCode("127.0.0.1", 4002);
            }).Start();
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                myserver = new ServerCode("127.0.0.1", 4003);
            }).Start();
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                myserver = new ServerCode("127.0.0.1", 4004);
            }).Start();

            Console.WriteLine("Sunucu Çalıştırıldı...\n");
            Console.ReadLine();
        }
    }
}