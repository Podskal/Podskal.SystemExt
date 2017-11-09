using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podskal.SystemExt.LinqExt
{
    internal class ItemsInCollectionDisposable<T> : IDisposable
    {
        private IEnumerable<T> m_collection;
        private Action<T> m_dispose;

        public ItemsInCollectionDisposable(IEnumerable<T> collection, Action<T> dispose)
        {
            if (collection == null)
                throw new ArgumentNullException("collection"); 
            if (dispose == null)
                throw new ArgumentNullException("dispose");

            this.m_collection = collection;
            this.m_dispose = dispose;
        }

        public void Dispose()
        {
            foreach (var item in this.m_collection)
            {
                this.m_dispose(item);
            }
        }
    }
}
