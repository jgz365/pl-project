using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using inventory_ni_Percie;
using POSCashierSystem;

namespace Pl_Project_Combined.Databases
{
    internal static class RecentTransactionDatabase
    {
        private static readonly object SyncRoot = new();
        private static bool initialized;

        public static void Initialize()
        {
            if (initialized) return;

            lock (SyncRoot)
            {
                if (initialized) return;
                CreateTableIfMissing();
                initialized = true;
            }
        }

        public static bool Add(Transaction tx)
        {
            Initialize();

            try
            {
                using var conn = DatabaseManager.GetConnection();
                const string sql = @"
INSERT INTO recent_transactions
(
    transaction_id,
    processed_at,
    payment_type,
    customer_name,
    unit_model,
    amount,
    status
)
VALUES
(
    @transaction_id,
    @processed_at,
    @payment_type,
    @customer_name,
    @unit_model,
    @amount,
    @status
);";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@transaction_id", tx.TransactionId ?? string.Empty);
                cmd.Parameters.AddWithValue("@processed_at", tx.DateTime);
                cmd.Parameters.AddWithValue("@payment_type", tx.PaymentType ?? string.Empty);
                cmd.Parameters.AddWithValue("@customer_name", tx.CustomerName ?? string.Empty);
                cmd.Parameters.AddWithValue("@unit_model", tx.UnitModel ?? string.Empty);
                cmd.Parameters.AddWithValue("@amount", tx.Amount);
                cmd.Parameters.AddWithValue("@status", tx.Status ?? "Paid");

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[RecentTransactionDatabase] Add error: {ex.Message}");
                return false;
            }
        }

        public static List<Transaction> GetAll()
        {
            Initialize();
            var list = new List<Transaction>();

            try
            {
                using var conn = DatabaseManager.GetConnection();
                const string sql = @"
SELECT
    transaction_id,
    processed_at,
    payment_type,
    customer_name,
    unit_model,
    amount,
    status
FROM recent_transactions
ORDER BY processed_at DESC, id DESC;";

                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Transaction
                    {
                        TransactionId = reader.GetString("transaction_id"),
                        DateTime = reader.GetDateTime("processed_at"),
                        PaymentType = reader.GetString("payment_type"),
                        CustomerName = reader.GetString("customer_name"),
                        UnitModel = reader.GetString("unit_model"),
                        Amount = reader.GetDecimal("amount"),
                        Status = reader.GetString("status")
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[RecentTransactionDatabase] GetAll error: {ex.Message}");
            }

            return list;
        }

        public static void ClearAll()
        {
            Initialize();

            try
            {
                using var conn = DatabaseManager.GetConnection();
                using var cmd = new MySqlCommand("DELETE FROM recent_transactions;", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[RecentTransactionDatabase] ClearAll error: {ex.Message}");
            }
        }

        private static void CreateTableIfMissing()
        {
            using var conn = DatabaseManager.GetConnection();

            using var checkCmd = new MySqlCommand(
                "SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = @db AND table_name = 'recent_transactions';",
                conn);
            checkCmd.Parameters.AddWithValue("@db", DatabaseManager.DATABASE_NAME);

            long exists = Convert.ToInt64(checkCmd.ExecuteScalar());
            if (exists > 0)
            {
                return;
            }

            const string createSql = @"
CREATE TABLE `recent_transactions` (
    `id`             BIGINT AUTO_INCREMENT PRIMARY KEY,
    `transaction_id` VARCHAR(64) NOT NULL,
    `processed_at`   DATETIME NOT NULL,
    `payment_type`   VARCHAR(64) NOT NULL,
    `customer_name`  VARCHAR(160) NOT NULL,
    `unit_model`     VARCHAR(200) NOT NULL,
    `amount`         DECIMAL(12,2) NOT NULL DEFAULT 0,
    `status`         VARCHAR(20) NOT NULL DEFAULT 'Paid',
    `created_at`     DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    INDEX `idx_rt_processed` (`processed_at`),
    INDEX `idx_rt_type` (`payment_type`),
    INDEX `idx_rt_status` (`status`),
    INDEX `idx_rt_txid` (`transaction_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

            using var createCmd = new MySqlCommand(createSql, conn);
            createCmd.ExecuteNonQuery();

            System.Diagnostics.Debug.WriteLine("[RecentTransactionDatabase] Table `recent_transactions` created ✔");
        }
    }
}
