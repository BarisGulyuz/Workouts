using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Framing.Impl;
using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;


namespace RabbitMQ.Consumer
{
    public partial class RabbitMQConsumerService : ServiceBase
    {

        const string quueNAme = "Deneme";

        private IConnection connection;
        private IModel channel;
        private System.Threading.Timer timer;

        public RabbitMQConsumerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WriteToFile("Sistem Çalışmaya Başladı");
            var factory = new ConnectionFactory() { HostName = "localhost" }; //more configurations
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            timer = new System.Threading.Timer(CheckQueue, null, 0, 100);
        }

        private void CheckQueue(object state)
        {
            var messageCount = channel.MessageCount(quueNAme);
            if (messageCount > 0)
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = System.Text.Encoding.UTF8.GetString(body);
                    WriteToFile(message);
                };

                channel.BasicConsume(queue: quueNAme,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
        protected override void OnStop()
        {
            WriteToFile("Sistem çalışmayı durdurdu.");
            timer.Dispose();
            channel.Close();
            connection.Close();
        }

        private void WriteToFile(string message)
        {
            message = $"{DateTime.Now} Tarihli Message => {message}";
            string path = @"D:\Codes\RabbitMQMessages.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                    sw.WriteLine(message);
               
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                    sw.WriteLine(message);
            }
        }
    }

}


