
OrderProcessor orderProcessor = new OrderProcessor();
Warehouse warehouse = new Warehouse();
NotificationService notificationService = new NotificationService();

warehouse.Subscribe(orderProcessor);
notificationService.InformMessageForCustomer(orderProcessor);
//Order order1 = new Order(1,"Laptop",2);
//Order order2 = new Order(2,"Keyboard",4);
//Order order3 = new Order(3,"Headset", 8);
Order order1 = new Order { OrderId = 1, Product = "Laptop", Quantity = 2 };
Order order2 = new Order { OrderId = 2, Product = "Smartphone", Quantity = 1 };
orderProcessor.PlaceOrder(order1);
orderProcessor.ProcessOrder(order1,true);

orderProcessor.PlaceOrder(order2);
orderProcessor.ProcessOrder(order2, true);

//orderProcessor.PlaceOrder(order3);
//orderProcessor.ProcessOrder(order3, false);


public class Order
{
    public int OrderId { get; set; }
    public string Product { get; set; }
    public int Quantity { get; set; }
}

public delegate void OrderHandler(Order order);

public class OrderProcessor
{
    public event OrderHandler OnOrderPlaced ;
    public event OrderHandler OnOrderProcessed;
    public event OrderHandler OnOrderFailed;

    public void PlaceOrder(Order order)
    {
        OnOrderPlaced?.Invoke(order);
    }
    public void ProcessOrder(Order order, bool isProductInStock) 
    {
        if(isProductInStock)
            OnOrderProcessed?.Invoke(order);
        else
            OnOrderFailed?.Invoke(order);
    }

}

public class Warehouse
{
    public void CheckInventory(Order order)
    {


        bool isInStock = CheckProductAvailability(order.Product);
        if (isInStock)
            Console.WriteLine($"Product {order.Product} is available. Обработка заказа {order.OrderId} ");
        else Console.WriteLine($"Product {order.Product} is not available at this moment. Заказ {order.OrderId} не может быть обработан.");



    }
    public void Subscribe(OrderProcessor orderProcessor)
    {
        orderProcessor.OnOrderPlaced += CheckInventory;
    }
    public bool CheckProductAvailability(string product)

    {
        return true;
    }
}

public class NotificationService
{    
    public void InformMessageForCustomer(OrderProcessor orderProcessor)
    {
        orderProcessor.OnOrderProcessed += NotifyCustomer;
        orderProcessor.OnOrderFailed += NotifyCustomer;
    }
    public void NotifyCustomer(Order order)
    {
        Console.WriteLine("Inform customer about order " + order.OrderId);
    }
}

    