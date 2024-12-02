using RabbitMQ.Client;

namespace ProductionApi.Services
{
    public class RabbitMQConnection
    {
        private readonly IConfiguration _configuration;
        private IConnection _connection;

        public RabbitMQConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IModel GetChannel()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                var factory = new ConnectionFactory
                {
                    HostName = _configuration["RabbitMQ:HostName"],
                    UserName = _configuration["RabbitMQ:UserName"],
                    Password = _configuration["RabbitMQ:Password"]
                };

                _connection = factory.CreateConnection();
            }

            return _connection.CreateModel();
        }
    }
}
