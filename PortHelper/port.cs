using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.ComponentModel;

namespace PortHelper
{
    public class port
    {
        public bool IsSelect { get; set; } = false;

        public int PortID { get; set; } = 0;

        public String PortType { get; set; } = "TCP";
        /// <summary>
        /// 占用该端口的进程ID
        /// </summary>
        public int PortProgramID { get; set; } = 0;


        //https://www.cnblogs.com/Kconnie/p/4675153.html
        /// <summary>
        /// 获取第一个可用的端口号
        /// </summary>
        /// <returns></returns>
        public static int GetFirstAvailablePort()
        {
            int MAX_PORT = 6000; //系统tcp/udp端口数最大是65535           
            int BEGIN_PORT = 5000;//从这个端口开始检测

            for (int i = BEGIN_PORT; i < MAX_PORT; i++)
            {
                if (PortIsAvailable(i)) return i;
            }

            return -1;
        }

        /// <summary>
        /// 获取操作系统已用的端口号
        /// </summary>
        /// <returns></returns>
        public static BindingList<port> PortIsUsed()
        {
            //获取本地计算机的网络连接和通信统计数据的信息
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //返回本地计算机上的所有Tcp监听程序
            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();

            //返回本地计算机上的所有UDP监听程序
            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

            //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            BindingList<port> allPorts = new BindingList<port>();
            foreach (IPEndPoint ep in ipsTCP) allPorts.Add(new port() { PortID = ep.Port,PortType="TCP" });
            foreach (IPEndPoint ep in ipsUDP) allPorts.Add(new port() { PortID = ep.Port, PortType = "UDP" });
            //foreach (TcpConnectionInformation conn in tcpConnInfoArray) allPorts.Add(conn.LocalEndPoint.Port);

            return allPorts;
        }

        /// <summary>
        /// 检查指定端口是否已用
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool PortIsAvailable(int port)
        {
            bool isAvailable = true;

            BindingList<port> portUsed = PortIsUsed();

            foreach (port p in portUsed)
            {
                if (p.PortID == port)
                {
                    isAvailable = false; break;
                }
            }

            return isAvailable;
        }
    }
}
