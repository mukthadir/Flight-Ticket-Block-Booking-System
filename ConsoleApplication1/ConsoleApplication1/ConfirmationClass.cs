using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;

namespace FlightBlockSystem
{
    class ConfirmationClass
    {
        public static ArrayList cells = new ArrayList();

        public static Semaphore s = new Semaphore(4, 4);
        public static Semaphore readSemaphore = new Semaphore(0, 4);
        public static ReaderWriterLock rwLock = new ReaderWriterLock();

        public Int32 count = 0;

        private static object threadLock = new object();

        public static void setOne(String obj)
        {
            s.WaitOne();
            rwLock.AcquireWriterLock(Timeout.Infinite);
            try
            {
                cells.Add(obj);
                readSemaphore.Release();
            }
            finally
            {
                rwLock.ReleaseWriterLock();
            }
        }

        public static String getOne()
        {
            readSemaphore.WaitOne();
            rwLock.AcquireReaderLock(Timeout.Infinite);
            String temp;
            try
            {
                temp = cells[0].ToString();
                cells.Remove(temp);
                s.Release();
            }
            finally
            {
                rwLock.ReleaseReaderLock();
            }
            return temp;
        }
    }        
}

