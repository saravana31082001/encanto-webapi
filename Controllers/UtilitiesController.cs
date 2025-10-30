using EncantoWebAPI.Accessors;
using Microsoft.AspNetCore.Mvc;

namespace EncantoWebAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class UtilitiesController : ControllerBase
    {

        [HttpGet("test-db-connection")]
        public ActionResult TestDatabase()
        {
            var mongo = new MongoDBAccessor();
            return mongo.TestConnection() ? Ok("✅ Connected to MongoDB!") : StatusCode(500, "❌ MongoDB connection failed.");
        }

    }
}
