using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace iqoption.core.Collections {
    public class ConcurrencyReactiveCollection<T> : ConcurrentBag<T>
    {

        [NonSerialized]
        private bool isDisposed = false;

        private Subject<T> AddSubject;
        public IObservable<T> AddObservable()
        {
            if (isDisposed) Observable.Empty<T>();
            return AddSubject ?? (AddSubject = new Subject<T>());
        }

        public ConcurrencyReactiveCollection()
        {

        }

        public new void Add(T item)
        {
            AddSubject.OnNext(item);

            base.Add(item);
        }


        public virtual void Remove(Func<T, bool> t) {
            var item = base.ToArray().FirstOrDefault(t);
            this.Remove(item);

            if (item is IDisposable) {
                ((IDisposable)item).Dispose();
            }
        }
        public void Remove(T item) {
            while (base.Count > 0)
            {
                T result;
                base.TryTake(out result);

                if (result.Equals(item))
                {
                    break;
                }

                base.Add(result);
            }
        }
    }
}