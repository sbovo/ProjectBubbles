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
        /// <summary>
        /// Demonstrate the most efficient storage query - the point query - where both partition key and row key are specified.
        /// </summary>
        /// <param name="table">Azure table name</param>
        /// <param name="partitionKey">Partition key - i.e., UserId</param>
        /// <param name="rowKey">Row key - i.e., Username</param>
        /// <returns>A Task object</returns>
        public static async Task<ProfileItem> RetrieveProfileAsync(CloudTable table, string partitionKey, string rowKey)
        {
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<ProfileItem>(partitionKey, rowKey);
                TableResult result = await table.ExecuteAsync(retrieveOperation);
                ProfileItem item = result.Result as ProfileItem;
                if (item != null)
                {
                    Debug.WriteLine($"\t{item.PartitionKey}\t{item.RowKey}");
                }

                return item;
            }
            catch (StorageException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
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
        public static async Task<ProfileItem> InsertOrMergeEntityAsync(CloudTable table, ProfileItem entity)
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
                ProfileItem insertedProfile = result.Result as ProfileItem;

                return insertedProfile;
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
        public static async Task DeleteEntityAsync(CloudTable table, ProfileItem deleteEntity)
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
