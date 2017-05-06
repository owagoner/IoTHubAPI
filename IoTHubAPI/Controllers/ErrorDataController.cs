using IoTHubAPI.Serivces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace IoTHubAPI.Controllers
{
    [Route("api/ErrorData")]
    public class ErrorDataController : ApiController
    {
        private string CollectionId = "UnvalidatedLog";
        // GET api/ErrorData
        [HttpGet]
        public IEnumerable<string> Get()
        {
            DocumentDb db = new DocumentDb();
            var rs = db.GetErrorMessages();
            yield return Newtonsoft.Json.JsonConvert.SerializeObject(rs);
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            if (id == 214213232)
            {
                DocumentDb db = new DocumentDb();

                return await db.ClearDocumentCollectionAsync(CollectionId);
            }
            else
            {
                return false;
            }

        }
    }
}
