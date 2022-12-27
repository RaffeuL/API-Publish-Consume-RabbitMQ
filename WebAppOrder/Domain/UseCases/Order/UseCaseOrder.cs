using Domain.SharedKernel.Models;
using Domain.UseCases.Order;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace WebAppOrder.Domain.UseCases.Order
{
    public interface IUseCaseOrder 
    {
        public Task<BaseOrderModel> MakeOrder(HttpRequest httpRequest, BaseOrderModel request);
        public Task ConsumeOrderQueue();
    }


    public class UseCaseOrder : IUseCaseOrder
    {
        public async Task<BaseOrderModel> MakeOrder(HttpRequest httpRequest, BaseOrderModel request)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using var connection = factory.CreateConnection();
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "orderQueue",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    int id = GeraId();
                    request.OrderId = id;
                    string message = JsonSerializer.Serialize(request);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: "orderQueue",
                        basicProperties: null,
                        body: body);

                    Console.WriteLine($"[x] Sent {message}");
                }

                return request;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region Consume Queue (Not Working)

        public async Task ConsumeOrderQueue()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            //var order = new BaseOrderModel();
            using var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "orderQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var order = JsonSerializer.Deserialize<BaseOrderModel>(message);

                        Console.WriteLine($"Order Id: {order.OrderId} | Order Name: {order.Name} | Order Price: R${order.Price:N2}");
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        channel.BasicNack(ea.DeliveryTag, false, true);
                    }
                };
                channel.BasicConsume(queue: "orderQueue",
                                    autoAck: false,
                                    consumer: consumer);
            }

        }
        #endregion
        
        
        private static int GeraId()
        {
            int id = new Random().Next(100);
            return id;
        }

    }
}
