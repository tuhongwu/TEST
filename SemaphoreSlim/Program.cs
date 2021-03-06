﻿using System;
using System.Threading;

/// <summary>
/// 互斥信号量
/// </summary>
namespace SemaphoreSlimTest
{
    class Program
    {
        //SemaphoreSlim是轻量级的
        static SemaphoreSlim _semaphorSlim = new SemaphoreSlim(4);
        static Semaphore _semaphor = new Semaphore(2,6);
        static void Main(string[] args)
        {


            for (int i = 1; i <= 6; i++)
            {
                string threadName = "Thread" + i;
                int secondsToWait = 2 + 2 * i;
                var t = new Thread(() => _semaphorT(threadName, secondsToWait));
                t.Start();
            }
            Console.ReadKey();



            for (int i = 1; i <= 9; i++)
            {
                string threadName = "Thread" + i;
                int secondsToWait = 2 + 2 * i;
                var t = new Thread(() => _semaphorSlimT(threadName, secondsToWait));
                t.Start();
            }
            Console.ReadKey();
        }


        static void _semaphorT(string name, int seconds)
        {
            Console.WriteLine("{0} 等待信号量", name);
            _semaphor.WaitOne();
            _semaphor.WaitOne();
            //_semaphor.WaitOne();
            Console.WriteLine("{0} 获得信号量，开始执行", name);
            Thread.Sleep(1000 * seconds);
            Console.WriteLine("{0} 结束", name);
            _semaphor.Release(3);
        }



        static void _semaphorSlimT(string name, int seconds)
        {
            Console.WriteLine("{0} 等待信号量", name);
            _semaphorSlim.Wait();
            Console.WriteLine("{0} 获得信号量，开始执行", name);
            Thread.Sleep(1000* seconds);
            Console.WriteLine("{0} 结束", name);
            _semaphorSlim.Release();
        }
        
    }
}
