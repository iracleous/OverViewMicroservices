namespace OrderService.Services;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Transactions;

public class DistributedTransactionService
{
    private readonly string _connectionString1;
    private readonly string _connectionString2;

    public DistributedTransactionService(IConfiguration configuration)
    {
        _connectionString1 = configuration.GetConnectionString("Database1") ?? "";
        _connectionString2 = configuration.GetConnectionString("Database2") ?? "";
    }

    public void PerformDistributedTransaction()
    {
        using (var transactionScope = new TransactionScope(TransactionScopeOption.Required,
            TransactionScopeAsyncFlowOption.Enabled))
        {
            // Step 1: Connect to the first database and perform an operation
            using (var connection1 = new SqlConnection(_connectionString1))
            {
                connection1.Open();
                var command1 = new SqlCommand("UPDATE Accounts SET Balance = Balance - 100 WHERE Id = 1", connection1);
                command1.ExecuteNonQuery();
            }

            // Step 2: Connect to the second database and perform an operation
            using (var connection2 = new SqlConnection(_connectionString2))
            {
                connection2.Open();
                var command2 = new SqlCommand("UPDATE Accounts SET Balance = Balance + 100 WHERE Id = 2", connection2);
                command2.ExecuteNonQuery();
            }

            // Step 3: Complete the transaction. Both operations will commit or rollback together
            transactionScope.Complete();
        }
    }
}
