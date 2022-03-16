using System;

namespace CoffeeShop.Data.Insfrastructure
{
    public class Disposable : IDisposable
    {
        // To detect redundant calls
        private bool isDisposed;

        ~Disposable() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    DisposeCore();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                isDisposed = true;
            }
        }

        protected virtual void DisposeCore()
        {
        }
    }
}