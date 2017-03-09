using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Auth;

namespace Xamarin17.Data.AzureStorage
{
    public class StorageTableBase
    {
        private CloudTableClient _tableClient;

        public StorageTableBase()
        {
            var sas = "?sv=2016-05-31&ss=t&srt=sco&sp=rl&se=2019-03-09T18:23:42Z&st=2017-03-09T10:23:42Z&spr=https&sig=aw34BixOCh6ZIOvk8yPncG%2FS1Pl8JiqAWO4JGnzPpI8%3D";
            StorageCredentials creds = new StorageCredentials(sas);
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(creds, null, null, new Uri("https://watermyplants.table.core.windows.net/"), null);

            _tableClient = cloudStorageAccount.CreateCloudTableClient();           
        }

        public CloudTableClient TableClient
        {
            get { return _tableClient; }
        }
    }
}
