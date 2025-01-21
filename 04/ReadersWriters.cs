using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNE04
{
    public class ReadersWriters
    {
        private static Semaphore writerSemaphore = new Semaphore(1, 1); // Max 1 pisatel 
        private static int readers = 0;
        private static object readerLock = new object();
        public static void Read()
        {
            lock (readerLock)
            {
                readers++;
                if (readers == 1)
                {
                    writerSemaphore.WaitOne(); // první čtenář bloikuje pisatele 
                }
            }

            // Simulace čtení z paměti
            Console.WriteLine("Čtenář {0} čte data...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);

            lock (readerLock)
            {
                readers--;
                if (readers == 0)
                {
                    writerSemaphore.Release(); // poslední čtenář odblokuje pisatele
                }
            }
        }
        public static void Write()
        {
            writerSemaphore.WaitOne(); // Pisatel čeká 

            // Simulace zápisu do paměti
            Console.WriteLine("Pisatel {0} zapisuje data...", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);

            writerSemaphore.Release(); // uvolnění semaforu po dokončení zápisu
        }
    }
}
