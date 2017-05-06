using IoTHubAPI.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IoTHubAPI.Serivces
{
    public class DocumentDb
    {
        private const string EndpointUri = "https://farmiot.documents.azure.com:443/";
        private const string PrimaryKey = "w179nPwMtyqu46UthNt9l51r2WaCmYCTUzGnQeruisrNQNyfwuLTfOfaOrsUwx870f3lp0kLxvuThVYdiuNofQ==";
        private const string DatabaseId = "farmiot";
        private const string CollectionId = "SensorData";
        private const string CollectionId2 = "UnvalidatedLog";
        private static DocumentClient client;

        internal bool TryToDeleteRecreateCollectionAsync()
        {
            throw new NotImplementedException();
        }

        public DocumentDb()
        {
            client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
        }

        public IEnumerable<SensorReadingMessage> GetSensorMessages()
        {
            // Set some common query options
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = 10 };

            return client.CreateDocumentQuery<SensorReadingMessage>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), queryOptions)
                    .Take(10)
                    .ToList();

        }

        public IEnumerable<SensorReadingMessage> GetErrorMessages()
        {
            // Set some common query options
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = 10 };
            return client.CreateDocumentQuery<SensorReadingMessage>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId2), queryOptions)
                    .Take(10)
                    .ToList();
        }

        public async Task<bool> ClearDocumentCollectionAsync(string collectionId)
        {
            Uri collectionUri = UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId);
            try
            {
                await client.DeleteDocumentCollectionAsync(collectionUri);
                var databaseUri = UriFactory.CreateDatabaseUri(DatabaseId);
                var dc = new DocumentCollection { Id = collectionId };
                await client
                    .CreateDocumentCollectionIfNotExistsAsync(databaseUri, dc);
                return true;
            }
            catch (Exception e) {
                return false;
            }
        }

    }
}