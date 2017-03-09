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
            var sas = "?sv=2016-05-31&ss=bfqt&srt=sco&sp=rwdlacup&se=2019-03-08T23:51:51Z&st=2017-03-08T15:51:51Z&spr=https&sig=bo0pYjWD695lV7oIE3wWMMYDZqjUX5wJSpU9mWdlv5A%3D";
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
