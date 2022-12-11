using RabitMQ.Common;

namespace RabbitMQ.Publisher
{
    public partial class RabbitMQPuslisher : Form
    {
        public RabbitMQPuslisher()
        {
            InitializeComponent();
        }

        RabbitMQFactory rabbitMQFactory;
        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            if (rabbitMQFactory == null)
                rabbitMQFactory = new RabbitMQFactory();

            for (int i = 0; i < 100; i++)
            {
                rabbitMQFactory.PublishMessage("DENEMEX", "Deneme", $"Mesaj {i}");
            }
        }
    }
}