using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadADB
{
    class Program
    {
        public static Process p;
        public static int i = 0;

        public static void StartAdb(string deviceInfo)
        {
            string whatDevice = "tablet";
            if (!deviceInfo.Equals("4300af121dc650bb"))
                whatDevice = "phone";

            p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "adb.exe ";
            startInfo.Arguments = "-s " + deviceInfo + " shell input text " + whatDevice + i;
            Console.WriteLine(String.Format(" device - {0} + i - {1} -- whatDevice - {2}", deviceInfo, i, whatDevice));
            i++;
            startInfo.UseShellExecute = false;
            p.StartInfo = startInfo;
            p.Start();
            //p.WaitForExit();
            Thread.Sleep(5000);
        }

        public static void NewLineAdb(string deviceInfo)
        {
            p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "adb.exe";
            startInfo.Arguments = "-s " + deviceInfo + " shell NL=$'\n'";
            Console.WriteLine(String.Format("Newline device - {0}", deviceInfo));
            startInfo.UseShellExecute = false;
            p.StartInfo = startInfo;
            p.Start();
            Thread.Sleep(5000);
        }

        public static void AdbTest(Object deviceInfo)
        {
            for (int i = 0; i < 5; i++)
            {
                //lock (deviceInfo)
                //{
                    StartAdb((string)deviceInfo);
                    //NewLineAdb((string)deviceInfo);
                //}
            }
        }

        static void Main(string[] args)
        {

            Thread t = new Thread(
                testq =>
                {
                    AdbTest(testq);
                });

            Thread t2 = new Thread(
                test2 =>
                {
                    AdbTest(test2);
                });
            t.Start("17d347b1");
            t2.Start("4300af121dc650bb");
            t.Join();
            Console.ReadLine();
        }
    }
}
