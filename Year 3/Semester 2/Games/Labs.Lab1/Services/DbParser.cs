using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using EFCore.BulkExtensions;
using Serilog;
using Newtonsoft.Json.Linq;
using Labs.Lab1.Database;
using Labs.Lab1.Database.Models;

namespace Labs.Lab1.Services
{
    public sealed class DbParser
    {
        private readonly GameDataDbContext _context;
        private readonly ILogger _logger;

        public DbParser(GameDataDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task ParseJsonsToDatabaseAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var files = Directory.EnumerateFiles("JSON", "*.json");

            foreach (var file in files)
            {
                try
                {
                    await ParseJsonFile(file);
                }
                catch(Exception ex)
                {
                    _logger.Fatal(ex, "Fatal exception: {0}");
                    Console.ReadLine();
                }
            }

            return;
        }

        private async Task ParseJsonFile(string file)
        {
            _logger.Information("Parsing file [{0}]", file);
            var jsonContent = await File.ReadAllTextAsync(file);
            var desetializedJson = JArray.Parse(jsonContent);
            _logger.Information("Found [{0}] JObjects", desetializedJson.Count);

            var users = desetializedJson.Where(x => x["event_id"].ToObject<int>() == 2)
                .Select(x => ParseUser((JObject)x))
                .ToList();

            await _context.BulkInsertOrUpdateAsync(users);
            _logger.Information("Inserted [{0}] entities of type [{1}]", users.Count, "User");

            var gameStart = desetializedJson.Where(x => x["event_id"].ToObject<int>() == 1)
                .Select(x => ParseGameStart((JObject)x))
                .ToList();

            await _context.BulkInsertOrUpdateAsync(gameStart);
            _logger.Information("Inserted [{0}] entities of type [{1}]", gameStart.Count, "GameStart");

            var stageStart = desetializedJson.Where(x => x["event_id"].ToObject<int>() == 3)
                .Select(x => ParseStageStart((JObject)x))
                .ToList();

            await _context.BulkInsertOrUpdateAsync(stageStart);
            _logger.Information("Inserted [{0}] entities of type [{1}]", stageStart.Count, "StageStart");

            var stageEnd = desetializedJson.Where(x => x["event_id"].ToObject<int>() == 4)
                .Select(x => ParseStageEnd((JObject)x))
                .ToList();

            await _context.BulkInsertOrUpdateAsync(stageEnd);
            _logger.Information("Inserted [{0}] entities of type [{1}]", stageEnd.Count, "StageEnd");

            var itemBuy = desetializedJson.Where(x => x["event_id"].ToObject<int>() == 5)
                .Select(x => ParseItemBuy((JObject)x))
                .ToList();

            await _context.BulkInsertOrUpdateAsync(itemBuy);
            _logger.Information("Inserted [{0}] entities of type [{1}]", itemBuy.Count, "ItemBuy");

            var currencyBuy = desetializedJson.Where(x => x["event_id"].ToObject<int>() == 6)
                .Select(x => ParseCurrencyBuy((JObject)x))
                .ToList();

            await _context.BulkInsertOrUpdateAsync(currencyBuy);
            _logger.Information("Inserted [{0}] entities of type [{1}]", currencyBuy.Count, "CurrencyBuy");
        }

        private User ParseUser(JObject jObject)
        {
            return new User
            {
                Id = jObject["udid"].ToObject<Guid>(),
                Date = jObject["date"].ToObject<DateTime>(),
                Age = jObject["parameters"]["age"].ToObject<int>(),
                Gender = jObject["parameters"]["gender"].ToObject<string>(),
                Country = jObject["parameters"]["country"].ToObject<string>()
            };
        }

        private GameStart ParseGameStart(JObject jObject)
        {
            return new GameStart
            {
                Id = Guid.NewGuid(),
                UserId = jObject["udid"].ToObject<Guid>(),
                Date = jObject["date"].ToObject<DateTime>()
            };
        }

        private StageStart ParseStageStart(JObject jObject)
        {
            return new StageStart
            {
                Id = Guid.NewGuid(),
                UserId = jObject["udid"].ToObject<Guid>(),
                Date = jObject["date"].ToObject<DateTime>(),
                Stage = jObject["parameters"]["stage"].ToObject<int>()
            };
        }

        private StageEnd ParseStageEnd(JObject jObject)
        {
            return new StageEnd
            {
                Id = Guid.NewGuid(),
                UserId = jObject["udid"].ToObject<Guid>(),
                Date = jObject["date"].ToObject<DateTime>(),
                Stage = jObject["parameters"]["stage"].ToObject<int>(),
                Win = jObject["parameters"]["win"].ToObject<bool>(),
                Time = jObject["parameters"]["time"].ToObject<int>(),
                Income = jObject["parameters"]["income"].ToObject<int>()
            };
        }

        private ItemBuy ParseItemBuy(JObject jObject)
        {
            return new ItemBuy
            {
                Id = Guid.NewGuid(),
                UserId = jObject["udid"].ToObject<Guid>(),
                Date = jObject["date"].ToObject<DateTime>(),
                Price = jObject["parameters"]["price"].ToObject<int>(),
                Item = jObject["parameters"]["item"].ToObject<string>()
            };
        }

        private CurrencyBuy ParseCurrencyBuy(JObject jObject)
        {
            return new CurrencyBuy
            {
                Id = Guid.NewGuid(),
                UserId = jObject["udid"].ToObject<Guid>(),
                Date = jObject["date"].ToObject<DateTime>(),
                Price = jObject["parameters"]["price"].ToObject<float>(),
                Name = jObject["parameters"]["name"].ToObject<string>(),
                Income = jObject["parameters"]["income"].ToObject<int>()
            };
        }
    }
}
