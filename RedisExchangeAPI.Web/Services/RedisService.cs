using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Services
{
    public class RedisService
    {
       
        private readonly string _redisPort;
        private readonly string _redisHost;
        private ConnectionMultiplexer _redis;
        public IDatabase  db { get; set; }

        public RedisService(IConfiguration configuration)
        {
           

            _redisPort = configuration["Redis:Port"];
            _redisHost = configuration["Redis:Host"];
        }


        public void Connect()
        {
            string confString = $"{_redisHost}:{_redisPort}";
            _redis=ConnectionMultiplexer.Connect(confString);

        }

        public IDatabase GetDatabase(int db ) { 
        
           return  _redis.GetDatabase(db); 
        
        }


    }
}
