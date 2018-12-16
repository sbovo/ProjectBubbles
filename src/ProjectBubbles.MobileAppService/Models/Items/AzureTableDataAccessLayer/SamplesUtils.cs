using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using ProjectBubbles.AzureTableDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProjectBubbles.Models
{
    partial class SamplesUtils
    {
        public static object Date { get; private set; }

        /// <summary>
        /// Demonstrate the most efficient storage query - the point query - where both partition key and row key are specified.
        /// </summary>
        /// <param name="table">Azure table name</param>
        /// <param name="partitionKey">Partition key - i.e., TeamId</param>
        /// <param name="rowKey">Row key - i.e., MeetingDatePlus</param>
        /// <returns>A Task object</returns>
        public static async Task<TableItem> RetrieveEntityUsingPointQueryAsync(CloudTable table, string partitionKey, string rowKey)
        {
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<TableItem>(partitionKey, rowKey);
                TableResult result = await table.ExecuteAsync(retrieveOperation);
                TableItem item = result.Result as TableItem;
                if (item != null)
                {
                    Debug.WriteLine("\t{0}\t{1}\t{2}\t{3}", item.PartitionKey, item.RowKey,
                        item.UserName, item.Location);
                }

                return item;
            }
            catch (StorageException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }


        public static async Task<List<TableItem>> GetList(CloudTable table, string partitionKey, String meetingNameStarting)
        {
            DateTime date;
            if (meetingNameStarting != null)
            {
                date = DateTime.Parse(meetingNameStarting);
                //date.Subtract(new TimeSpan(1, 0, 0, 1));
            }
            else
            {
                date = DateTime.Today;
            }

            //Query
            string partitionKeyFilter = TableQuery.GenerateFilterCondition("PartitionKey",
                QueryComparisons.Equal, partitionKey);
            string RowKeyFilterLow = TableQuery.GenerateFilterCondition("RowKey",
                QueryComparisons.GreaterThanOrEqual, date.GetUNIVERSALString());
            string RowKeyFilterHigh = TableQuery.GenerateFilterCondition("RowKey",
           QueryComparisons.LessThan, date.AddDays(1).GetUNIVERSALString());
            string combinedFilters = TableQuery.CombineFilters(partitionKeyFilter, TableOperators.And, RowKeyFilterLow);
            combinedFilters = TableQuery.CombineFilters(combinedFilters, TableOperators.And, RowKeyFilterHigh);

            TableQuery<TableItem> query = new TableQuery<TableItem>().Where(combinedFilters);

            List<TableItem> results = new List<TableItem>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<TableItem> queryResults =
                    await table.ExecuteQuerySegmentedAsync(query, continuationToken);

                continuationToken = queryResults.ContinuationToken;

                results.AddRange(queryResults.Results);

            } while (continuationToken != null);

            return results;
        }


        /// <summary>
        /// The Table Service supports two main types of insert operations.
        ///  1. Insert - insert a new entity. If an entity already exists with the same PK + RK an exception will be thrown.
        ///  2. Replace - replace an existing entity. Replace an existing entity with a new entity.
        ///  3. Insert or Replace - insert the entity if the entity does not exist, or if the entity exists, replace the existing one.
        ///  4. Insert or Merge - insert the entity if the entity does not exist or, if the entity exists, merges the provided entity properties with the already existing ones.
        /// </summary>
        /// <param name="table">The sample table name</param>
        /// <param name="entity">The entity to insert or merge</param>
        /// <returns>A Task object</returns>
        public static async Task<TableItem> InsertOrMergeEntityAsync(CloudTable table, TableItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                TableItem insertedItem = result.Result as TableItem;

                return insertedItem;
            }
            catch (StorageException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="table">Sample table name</param>
        /// <param name="deleteEntity">Entity to delete</param>
        /// <returns>A Task object</returns>
        public static async Task DeleteEntityAsync(CloudTable table, TableItem deleteEntity)
        {
            try
            {
                if (deleteEntity == null)
                {
                    throw new ArgumentNullException("deleteEntity");
                }

                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                await table.ExecuteAsync(deleteOperation);
            }
            catch (StorageException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}