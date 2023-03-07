using System;
using System.Collections.Generic;

namespace Pz_7._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stock stock = new Stock();
            admin user = new admin("user", stock);
            Sys Syst = new Sys("Ошибка на сайте", stock);
            
            stock.event1();
        
            Syst.Eventfix();
            
            stock.event1();
           

            Console.Read();
        }
    

    interface IObserver
    {
        void Update(Object ob);
    }

    interface IObservable
    {
        void RegisterObserver(IObserver o);
        void RemoveObserver(IObserver o);
        void NotifyObservers();
    }

    class Stock : IObservable
    {
        StockInfo sInfo; // информация о торгах

        List<IObserver> observers;
        public Stock()
        {
            observers = new List<IObserver>();
            sInfo = new StockInfo();
        }
        public void RegisterObserver(IObserver o)
        {
            observers.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            observers.Remove(o);
        }

        public void NotifyObservers()
        {
            foreach (IObserver o in observers)
            {
                o.Update(sInfo);
            }
        }

        public void event1()
        {
            Random error = new Random();
            sInfo.finderror = error.Next(0,10);
            sInfo.fix = error.Next(0, 4);
            NotifyObservers();
        }
    }

    class StockInfo
    {
        public int finderror { get; set; }
        public int fix { get; set; }
    }

    class Sys : IObserver
    {
        public string Name { get; set; }
        IObservable stock;
        public Sys(string name, IObservable obs)
        {
            this.Name = name;
            stock = obs;
            stock.RegisterObserver(this);
        }
        public void Update(object ob)
        {
            StockInfo sInfo = (StockInfo)ob;

            if (sInfo.finderror > 2)
                Console.WriteLine("Пользователь {0} Обнаружил ошибок в системе    {1}", this.Name, sInfo.finderror);
            else
                Console.WriteLine("Нет жалаб на ошибки сайта");
        }
        public void Eventfix()
        {
            stock.RemoveObserver(this);
            stock = null;
        }
    }

    class admin : IObserver
    {
        public string Name { get; set; }
        IObservable stock;
        public admin(string name, IObservable obs)
        {
            this.Name = name;
            stock = obs;
            stock.RegisterObserver(this);
        }
        public void Update(object ob)
        {
            StockInfo sInfo = (StockInfo)ob;

            if (sInfo.fix > 2)
                Console.WriteLine("Администратор исправил {0} ошибок", sInfo.fix);
            else
                Console.WriteLine("Не получилась исправить ошибки");
        }
    }
}
    }