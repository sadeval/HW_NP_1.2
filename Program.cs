using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UdpTimeServer
{
    class Program
    {
        static void Main()
        {
            int port = 12345;
            UdpClient udpServer = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, port); // Используем широковещательный адрес

            Console.WriteLine("UDP-сервер запущен. Отправка текущего времени в сеть...");

            while (true)
            {
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                string alarmTime = "12:00:00"; // Время звонка
                byte[] data = Encoding.UTF8.GetBytes(currentTime);

                udpServer.Send(data, data.Length, endPoint); // Отправляем время всем клиентам
                Console.WriteLine($"Отправлено время: {currentTime}");

                if (currentTime == alarmTime)
                {
                    byte[] alarmData = Encoding.UTF8.GetBytes("Звонок!");
                    udpServer.Send(alarmData, alarmData.Length, endPoint);
                    Console.WriteLine("Отправлено сообщение о звонке!");
                }

                Thread.Sleep(1000);
            }
        }
    }
}
