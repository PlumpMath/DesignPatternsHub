using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace DesignPatterns.Behavioural.Observer
{
    public class Observer
    {
        public static class Application
        {
            public static void Run()
            {
                var SampleData = new List<Stock>()
                    {
                        new Stock() { Code = "GOOGLE" },
                        new Stock() { Code = "MS" },
                        new Stock() { Code = "IBM" } 
                    };

                var st = new StockTicker();

                var gf = new GoogleObserver();
                var ms = new MSObserver();

                using (st.Subscribe(gf))
                using (st.Subscribe(ms))
                {
                    foreach (var s in SampleData)
                        st.Stock = s;
                }
            }
        }


        public class Stock
        { 
            public string Code { get; set; }
        }


        public class StockTicker : IObservable<Stock>
        {
            List<IObserver<Stock>> observers = new List<IObserver<Stock>>();

            private Stock stock;
            public Stock Stock
            {
                get { return stock; }
                set
                {
                    stock = value;
                    this.Notify(this.stock);
                }
            }

            private void Notify(Stock s)
            {
                foreach (var o in observers)
                {
                    // if error -> o.OnError();
                    o.OnNext(s);
                }
            }

            private void Stop()
            {
                foreach (var o in observers)
                {
                    o.OnCompleted();
                }
                observers.Clear();
            }


            public IDisposable Subscribe(IObserver<Stock> observer)
            {
                if (!observers.Contains(observer))
                {
                    observers.Add(observer);
                }
                return new Unsubscriber(observers, observer);
            }

            private class Unsubscriber : IDisposable
            {
                private List<IObserver<Stock>> _observers;
                private IObserver<Stock> _observer;
                public Unsubscriber(List<IObserver<Stock>> observers, IObserver<Stock> observer)
                {
                    _observers = observers;
                    _observer = observer;
                }

                public void Dispose()
                {
                    if (_observers != null && _observers.Contains(_observer))
                    {
                        _observers.Remove(_observer);
                    }
                }
            }
        }

        public class GoogleObserver : IObserver<Stock>
        {
            public void OnCompleted()
            {
                throw new NotImplementedException();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(Stock stock)
            {
                // check if stock is for Google.
                if (stock.Code == "GOOGLE")
                {
                    using (var scope = AutoFacInstance.Container.BeginLifetimeScope())
                    {
                        var outputWriter = AutoFacInstance.Container.Resolve<IOutputWriter>();
                        outputWriter.Write("Google Observed");
                    }
                }
            }
        }

        public class MSObserver : IObserver<Stock>
        {
            public void OnCompleted()
            {
                throw new NotImplementedException();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(Stock stock)
            {
                // check if stock is for MS
                if (stock.Code == "MS")
                {
                    var outputWriter = AutoFacInstance.Container.Resolve<IOutputWriter>();
                    outputWriter.Write("MS Observed");
                    //Console.WriteLine("MS Observed");
                }
            }
        }

    }
}
