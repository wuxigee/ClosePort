﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
// 下载于www.mycodes.net
namespace 关闭端口小工具
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
