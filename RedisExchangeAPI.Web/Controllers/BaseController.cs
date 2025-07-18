﻿using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly RedisService _redisService;
        protected IDatabase db;
      
        public BaseController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDatabase(3);

        }
    }
}
