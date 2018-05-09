using System;
using System.Collections.Concurrent;
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
    }
}