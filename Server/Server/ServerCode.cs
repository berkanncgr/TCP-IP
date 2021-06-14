using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Server
{
    class ServerCode
    {
        public static double sonuc, toplam, kenarSonuc;
        public int sayac = 0, sayac2 = 0, sayac3 = 0;
        public static string durum = "false";
        static bool kontrol_CL1 = false, kontrol_CL4 = false;
        public double[] veri = new double[4];
        public double[] veri2 = new double[3];



        TcpListener server = null;

        public ServerCode(string ip, int port)
        {
            IPAddress localAddr = IPAddress.Parse(ip);
            //server = new TcpListener(localAddr, port);
            server = new TcpListener(localAddr, port);
            server.Start();

            //Console.WriteLine("\nPORT : " + port);

            StartListener(port);
        }

        public void StartListener(int port)
        {
            try
            {
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();

                    switch (port)
                    {
                        case 4001:
                            Thread t = new Thread(new ParameterizedThreadStart(forClient1));
                            t.Start(client);
                            //StartListener(port);
                            break;
                        case 4002:
                            Thread t2 = new Thread(new ParameterizedThreadStart(forClient2));
                            t2.Start(client);
                            //StartListener(port);
                            break;
                        case 4003:
                            Thread t3 = new Thread(new ParameterizedThreadStart(forClient3));
                            t3.Start(client);
                            //StartListener(port);
                            break;
                        case 4004:
                            Thread t4 = new Thread(new ParameterizedThreadStart(forClient4));
                            t4.Start(client);
                            //StartListener(port);
                            break;
                        default:
                            Console.WriteLine("Port Hatası !!!");
                            break;
                    }
                    //Thread t = new Thread(new ParameterizedThreadStart(HandleDeivce));

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("Soket Hatası!!", e);
                //server.Stop();
            }
        }
        public void forClient1(Object obj)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            string imei = String.Empty;
            string data = null;
            Byte[] bytes = new Byte[256];

            int i;
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, i);

                    if (!data.Equals(null))
                    {
                        veri2[sayac] = Convert.ToDouble(data);
                        Console.WriteLine("4001 Portundan Gelen Veri : " + veri2[sayac]);
                        sayac++;
                    }
                }
                if (sayac == 3)
                {
                    double time = (double)0.03 - (watch.Elapsed.TotalSeconds); Convert.ToInt32(((double)0.03 - (watch.Elapsed.TotalSeconds)) * 1000);
                    Thread.Sleep(Convert.ToInt32(time*1000));
                    watch.Stop();
                    Console.WriteLine("\nBağlantı  süresi: {0}", watch.Elapsed.TotalSeconds + " saniye");
                    watch.Reset();
                    //Sonuç= 3     X      +  5 *   Y     *    Y     +    Z     *   Z     *    Z;
                    sonuc = (3 * veri2[0]) + (5 * veri2[1] * veri2[1]) + (veri2[2] * veri2[2] * veri2[2]);
                    sayac = 0;
                    kontrol_CL1 = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("!!!Client-1 Bağlantı HATASI!!!");
                client.Close();
            }
            
        }

        public void forClient2(Object obj)
        {
            string message;
            Byte[] msj;
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            try
            {
                message = "\nDurum :    " + durum + "\nSonuclarin hesaplanmasi bekleniyor...\n";
                msj = Encoding.ASCII.GetBytes(message);

                stream.Write(msj, 0, msj.Length);
                while (true)
                {
                    if (kontrol_CL1 && kontrol_CL4)
                    {
                        message = "\nSonuc :   " + sonuc.ToString() + "\nToplam :    " + toplam.ToString() + "\nDurum :    " + durum;          
                        msj = Encoding.ASCII.GetBytes(message);
                        
                        stream.Write(msj, 0, msj.Length);
                        break;
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("!!!Client-2 BAĞLANTI HATASI!!!" + e.ToString());
                client.Close();
            }
        }

        public void forClient3(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            int Start = 0;

            string message = null;

            string data = null;
            Byte[] bytes = new Byte[256];

            int i;
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    if (!data.Equals(null))
                    {
                        veri[sayac2] = Convert.ToDouble(data);
                        sayac2++;
                        Start = 1;
                    }
                    message = "\nStart :  " + Start + "\nTime :  " + DateTime.Now.ToString();
                    Byte[] msj = Encoding.ASCII.GetBytes(message);
                    stream.Write(msj, 0, msj.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("!!!Client-3 BAĞLANTI HATASI!!!");
                client.Close();
            }
            if (sayac2 == 3)
            {
                kenarSonuc = (veri[1] * veri[1]) + (veri[2] * veri[2]) - (2 * veri[1] * veri[2] * Math.Round(Math.Cos(veri[0]), 0));
                Console.WriteLine("\n4003 Portundan gelen veriler ile Karşı Kenar Hesabı Sonucu : " + Math.Sqrt(kenarSonuc));
                Console.WriteLine("\n4003 Portuna Gönderilen Mesaj :  " + message);
                sayac2 = 0;
            }


        }

        public void forClient4(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            string imei = String.Empty;
            string data = null;
            Byte[] bytes = new Byte[256];

            int i;
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, i);

                    if (!data.Equals(null))
                    {
                        veri[sayac3] = Convert.ToDouble(data);
                        //toplam += veri[sayac3];
                        Console.WriteLine("4004 Portundan Gelen Veri : " + veri[sayac3]);
                        sayac3++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("!!!Client-4 Bağlantı HATASI!!!" + e.ToString());
                client.Close();
            }
            if (sayac3 == 4)
            {
                toplam = veri[0] + veri[1] + veri[2] + veri[3];
                kontrol_CL4 = true;
                sayac3 = 0;
            }
        }
    }
}
