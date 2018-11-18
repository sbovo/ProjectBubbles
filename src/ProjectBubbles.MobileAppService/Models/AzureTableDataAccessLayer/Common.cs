using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

public class Common
{
    /// <summary>
    /// Validate the connection string information in app.config and throws an exception if it looks like 
    /// the user hasn't updated this to valid values. 
    /// </summary>
    /// <param name="storageConnectionString">Connection string for the storage service or the emulator</param>
    /// <returns>CloudStorageAccount object</returns>
    public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
    {
        CloudStorageAccount storageAccount;
        try
        {
            storageAccount = CloudStorageAccount.Parse(storageConnectionString);
        }
        catch (FormatException)
        {
            Debug.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
            throw;
        }
        catch (ArgumentException)
        {
            Debug.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
            throw;
        }

        return storageAccount;
    }


    /// <summary>
    /// Create a table for the sample application to process messages in. 
    /// </summary>
    /// <returns>A CloudTable object</returns>
    public static async Task<CloudTable> CreateTableAsync(string tableName)
    {
        // Retrieve storage account information from connection string.
        // TODO: sbovo - Use appsetting.json and Azure Key Vaul for production
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=bubblestorageaccount;AccountKey=mS0ZlvGbor9esniz1wU+GAgb6A6p6JqEq3NUBmsq6E4idiBIRaRLb4xjv4s9qf3+UmHz7GacpZgfjiYFFs7/Mw==;";
        CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(connectionString);

        // Create a table client for interacting with the table service
        CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

        Debug.WriteLine("Create a Table for the demo");

        // Create a table client for interacting with the table service 
        CloudTable table = tableClient.GetTableReference(tableName);
        try
        {
            if (await table.CreateIfNotExistsAsync())
            {
                Debug.WriteLine("Created Table named: {0}", tableName);
            }
            else
            {
                Debug.WriteLine("Table {0} already exists", tableName);
            }
        }
        catch (StorageException)
        {
            Debug.WriteLine("If you are running with the default configuration please make sure you have started the storage emulator. Press the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample.");
            throw;
        }

        return table;
    }

}