namespace Chapter2_Objective6
{
    using System;
    using System.IO;

    public class UnmanagedWrapper : IDisposable
    {
        public UnmanagedWrapper()
        {
            Stream = File.Open("temp.dat", FileMode.Create);
        }

        ~UnmanagedWrapper()
        {
            Dispose(false);
        }

        public FileStream Stream { get; private set; }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Stream != null)
                {
                    Stream.Close();
                }
            }
        }
    }
}
