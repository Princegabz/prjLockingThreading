using System;

namespace MprjLockingThreading // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main";
            LoadFile();

            Thread t1 = new Thread(LoadFile);
            t1.Name = "Thread 1";
            Thread t2 = new Thread(LoadFile);
            t2.Name = "Thread 2";
            Thread t3 = new Thread(LoadFile);
            t3.Name = "Thread 3";

            t1.Start(); 
            t2.Start(); 
            t3.Start(); 

            t1.Join();
            t2.Join();  
            t3.Join();  
        }
        private static object objLock = new object();
        public static int total = 0;

        public static void LoadFile()
        {
            Console.WriteLine(Thread.CurrentThread.Name + " Trying to enter into critical area");
            Monitor.Enter(objLock);//monitor one method to run at a time 
            try
            {
                Console.WriteLine(Thread.CurrentThread.Name + " Has entered into critical area");
                Console.WriteLine("Loading... ");
                Thread.Sleep(5000);
                Console.WriteLine("The file...");
                total++;
            }
            finally //regardless of a statment failing it will always run
            {
                Monitor.Exit(objLock);
                Console.WriteLine(Thread.CurrentThread.Name + " Has EXITED the critical area");
            }
        } 
    }
}