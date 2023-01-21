namespace Mediator
{
    /// <summary>
    /// You can check the link below for better understanding
    /// https://refactoring.guru/design-patterns/mediator
    /// </summary>
    public interface IManageRestaurant
    {
        void Notify(object sender, string ev);
    }


    class ConcreteManageRestaurant : IManageRestaurant
    {
        private Waiter _waiter;

        private Chef _chef;

        public ConcreteManageRestaurant(Waiter waiter, Chef chef)
        {
            _waiter = waiter;
            this._waiter.SetMediator(this);
            _chef = chef;
            this._chef.SetMediator(this);
        }

        public void Notify(object sender, string ev)
        {

            if (ev == "order")
            {
                Console.WriteLine("Your order has been given to the chef");
                this._chef.Cook();
            }
            if (ev == "ready")
            {
                Console.WriteLine("your order is ready");
                this._waiter.TakeOrder();
            }
            if(ev== "tookOrder")
            {
                Console.WriteLine("pay and good luck");
            }
        }
    }


    class BaseComponent
    {
        protected ConcreteManageRestaurant _mediator;

        public BaseComponent(ConcreteManageRestaurant mediator = null)
        {
            this._mediator = mediator;
        }

        public void SetMediator(ConcreteManageRestaurant mediator)
        {
            this._mediator = mediator;
        }
    }


    class Waiter : BaseComponent
    {
        public void GetOrder()
        {
            Console.WriteLine("The waiter get order");
            this._mediator.Notify(this, "order");
        }

        public void TakeOrder()
        {
            Console.WriteLine("The waiter take order");
            this._mediator.Notify(this, "tookOrder");
        }
    }

    class Chef : BaseComponent
    {
        public void Cook()
        {
            Console.WriteLine("The chef is cooking");
            this._mediator.Notify(this, "ready");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            var waiter = new Waiter();
            var chef = new Chef();
            new ConcreteManageRestaurant(waiter, chef);

            Console.WriteLine("Client taking the order");
            waiter.GetOrder();
        }
    }
}