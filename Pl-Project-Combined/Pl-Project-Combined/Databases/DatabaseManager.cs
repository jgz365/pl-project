// ═══════════════════════════════════════════════════════════════════
// FILE: DatabaseManager.cs
// PURPOSE: Database connection, auto-creation, tables, and seeding
// Modeled after Rovic's proven VB DatabaseManager / DatabaseInitializer
// ═══════════════════════════════════════════════════════════════════
using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;

namespace inventory_ni_Percie
{
    public static class DatabaseManager
    {
        // ── Config ────────────────────────────────────────────────────────
        public const string DATABASE_NAME = "rovic_inventory_db";
        private const string SERVER = "127.0.0.1";
        private const uint PORT = 3306;
        private const string UID = "root";
        private const string PWD = "";         // XAMPP default: no password

        private static readonly object _lock = new();
        private static bool _initialized = false;

        // ════════════════════════════════════════════════════════════════
        //  CONNECTION STRING BUILDERS
        // ════════════════════════════════════════════════════════════════

        /// <summary>Connection string WITH the database selected.</summary>
        public static string ConnectionString
        {
            get
            {
                var b = BaseBuilder();
                b.Database = DATABASE_NAME;
                return b.ToString();
            }
        }

        /// <summary>Connection string WITHOUT a database — used to CREATE the DB.</summary>
        public static string ConnectionStringWithoutDatabase
            => BaseBuilder().ToString();

        private static MySqlConnectionStringBuilder BaseBuilder()
        {
            return new MySqlConnectionStringBuilder
            {
                Server = SERVER,
                Port = PORT,
                UserID = UID,
                Password = PWD,
                CharacterSet = "utf8mb4",
                SslMode = MySqlSslMode.Disabled,   // ← None works on all XAMPP versions
                AllowUserVariables = true,
                Pooling = true,
                MinimumPoolSize = 0,
                MaximumPoolSize = 100,
                ConnectionTimeout = 30,
            };
        }

        // ════════════════════════════════════════════════════════════════
        //  XAMPP AUTO-DETECTION
        // ════════════════════════════════════════════════════════════════
        private static void DetectXampp()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                string path = $@"{c}:\xampp\mysql\bin\mysqld.exe";
                if (File.Exists(path))
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"[DatabaseManager] XAMPP found on drive {c}:\\");
                    return;
                }
            }
            System.Diagnostics.Debug.WriteLine(
                "[DatabaseManager] XAMPP mysqld.exe not found — assuming standalone MySQL on localhost.");
        }

        // ════════════════════════════════════════════════════════════════
        //  PUBLIC: Initialize  ← call this from Program.cs on startup
        //  Throws on failure so Program.cs can show a proper error.
        // ════════════════════════════════════════════════════════════════
        public static void Initialize()
        {
            if (_initialized) return;
            lock (_lock)
            {
                if (_initialized) return;

                DetectXampp();
                CreateDatabaseIfMissing();   // Step 1 — create DB
                CreateTablesIfMissing();     // Step 2 — create tables
                SeedDefaultUsersIfEmpty();   // Step 3 — insert default accounts

                _initialized = true;
                System.Diagnostics.Debug.WriteLine(
                    "[DatabaseManager] Initialization complete ✔");
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  PUBLIC FACTORY METHODS
        // ════════════════════════════════════════════════════════════════

        /// <summary>Returns an OPEN connection to rovic_inventory_db.</summary>
        public static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        /// <summary>Returns an OPEN connection without a database selected.</summary>
        public static MySqlConnection GetConnectionWithoutDatabase()
        {
            var conn = new MySqlConnection(ConnectionStringWithoutDatabase);
            conn.Open();
            return conn;
        }

        // ════════════════════════════════════════════════════════════════
        //  TEST CONNECTION
        // ════════════════════════════════════════════════════════════════
        public static bool TestConnection()
        {
            try
            {
                using var conn = new MySqlConnection(ConnectionString);
                conn.Open();
                return true;
            }
            catch { return false; }
        }

        public static bool IsConnected() => TestConnection();

        // ════════════════════════════════════════════════════════════════
        //  STEP 1 — CREATE DATABASE IF MISSING
        // ════════════════════════════════════════════════════════════════
        private static void CreateDatabaseIfMissing()
        {
            // Check first using information_schema (same as VB version)
            using var checkConn = GetConnectionWithoutDatabase();
            using var checkCmd = new MySqlCommand(
                "SELECT COUNT(*) FROM information_schema.schemata WHERE schema_name = @db;",
                checkConn);
            checkCmd.Parameters.AddWithValue("@db", DATABASE_NAME);
            long exists = Convert.ToInt64(checkCmd.ExecuteScalar());

            if (exists == 0)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[DatabaseManager] Database `{DATABASE_NAME}` not found — creating...");

                using var createCmd = new MySqlCommand(
                    $"CREATE DATABASE `{DATABASE_NAME}` " +
                    $"CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;",
                    checkConn);
                createCmd.ExecuteNonQuery();

                System.Diagnostics.Debug.WriteLine(
                    $"[DatabaseManager] Database `{DATABASE_NAME}` created ✔");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[DatabaseManager] Database `{DATABASE_NAME}` already exists.");
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  STEP 2 — CREATE TABLES IF MISSING
        // ════════════════════════════════════════════════════════════════
        private static void CreateTablesIfMissing()
        {
            // Check if users table exists (same approach as VB TableExists)
            using var checkConn = GetConnection();
            using var checkCmd = new MySqlCommand(
                "SELECT COUNT(*) FROM information_schema.tables " +
                "WHERE table_schema = @db AND table_name = 'users';",
                checkConn);
            checkCmd.Parameters.AddWithValue("@db", DATABASE_NAME);
            long tableExists = Convert.ToInt64(checkCmd.ExecuteScalar());

            if (tableExists == 0)
            {
                const string sql = @"
                    CREATE TABLE `users` (
                        `id`         INT AUTO_INCREMENT PRIMARY KEY,
                        `full_name`  VARCHAR(100) NOT NULL,
                        `username`   VARCHAR(50)  NOT NULL UNIQUE,
                        `password`   VARCHAR(255) NOT NULL,
                        `role`       ENUM('SuperAdmin','Admin','Assessor','POSCashier','Inventory') NOT NULL,
                        `status`     ENUM('Active','Inactive','Suspended') NOT NULL DEFAULT 'Active',
                        `last_login` DATETIME NULL,
                        `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        INDEX idx_username (username),
                        INDEX idx_role (role)
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

                using var createCmd = new MySqlCommand(sql, checkConn);
                createCmd.ExecuteNonQuery();

                System.Diagnostics.Debug.WriteLine(
                    "[DatabaseManager] Table `users` created ✔");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(
                    "[DatabaseManager] Table `users` already exists.");
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  STEP 3 — SEED DEFAULT USERS IF TABLE IS EMPTY
        // ════════════════════════════════════════════════════════════════
        private static void SeedDefaultUsersIfEmpty()
        {
            using var conn = GetConnection();

            using var countCmd = new MySqlCommand(
                "SELECT COUNT(*) FROM users;", conn);
            long count = Convert.ToInt64(countCmd.ExecuteScalar());

            if (count > 0)
            {
                System.Diagnostics.Debug.WriteLine(
                    "[DatabaseManager] Users already exist — skipping seed.");
                return;
            }

            const string insertSql = @"
                INSERT INTO users (full_name, username, password, role, status)
                VALUES (@fullName, @username, @password, @role, 'Active');";

            var seeds = new[]
            {
                new { FullName = "Super Administrator", Username = "superadmin",
                      Password = "SuperAdmin@123",      Role     = "SuperAdmin" },
                new { FullName = "System Admin",        Username = "admin",
                      Password = "Admin@123",           Role     = "Admin" }
            };

            foreach (var s in seeds)
            {
                string hashed = BCrypt.Net.BCrypt.HashPassword(s.Password);
                using var cmd = new MySqlCommand(insertSql, conn);
                cmd.Parameters.AddWithValue("@fullName", s.FullName);
                cmd.Parameters.AddWithValue("@username", s.Username);
                cmd.Parameters.AddWithValue("@password", hashed);
                cmd.Parameters.AddWithValue("@role", s.Role);
                cmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine(
                    $"[DatabaseManager] Seeded user: {s.Username} ({s.Role})");
            }

            System.Diagnostics.Debug.WriteLine(
                "[DatabaseManager] Default users seeded ✔");
        }

        // ════════════════════════════════════════════════════════════════
        //  PUBLIC: ValidateLogin
        // ════════════════════════════════════════════════════════════════
        public static UserModel? ValidateLogin(string username, string password)
        {
            try
            {
                using var conn = GetConnection();
                const string sql = @"
                    SELECT id, full_name, username, password, role,
                           status, last_login, created_at
                    FROM   users
                    WHERE  username = @username AND status = 'Active'
                    LIMIT  1;";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username.Trim());

                using var r = cmd.ExecuteReader();
                if (r.Read() && BCrypt.Net.BCrypt.Verify(password, r.GetString("password")))
                {
                    return new UserModel
                    {
                        Id = r.GetInt32("id"),
                        FullName = r.GetString("full_name"),
                        Username = r.GetString("username"),
                        Role = r.GetString("role"),
                        Status = r.GetString("status"),
                        LastLogin = r.IsDBNull(r.GetOrdinal("last_login"))
                                        ? null : r.GetDateTime("last_login"),
                        CreatedAt = r.GetDateTime("created_at")
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[DatabaseManager] ValidateLogin error: {ex.Message}");
                throw; // Re-throw so LoginUiForm can show the error
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  PUBLIC: UpdateLastLogin
        // ════════════════════════════════════════════════════════════════
        public static void UpdateLastLogin(int userId)
        {
            try
            {
                using var conn = GetConnection();
                using var cmd = new MySqlCommand(
                    "UPDATE users SET last_login = NOW() WHERE id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[DatabaseManager] UpdateLastLogin error: {ex.Message}");
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  PUBLIC: GetAllUsers
        // ════════════════════════════════════════════════════════════════
        public static List<UserModel> GetAllUsers()
        {
            var list = new List<UserModel>();
            try
            {
                using var conn = GetConnection();
                const string sql =
                    "SELECT id, full_name, username, role, status, " +
                    "last_login, created_at FROM users ORDER BY id;";
                using var cmd = new MySqlCommand(sql, conn);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    list.Add(new UserModel
                    {
                        Id = r.GetInt32("id"),
                        FullName = r.GetString("full_name"),
                        Username = r.GetString("username"),
                        Role = r.GetString("role"),
                        Status = r.GetString("status"),
                        LastLogin = r.IsDBNull(r.GetOrdinal("last_login"))
                                        ? null : r.GetDateTime("last_login"),
                        CreatedAt = r.GetDateTime("created_at")
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[DatabaseManager] GetAllUsers error: {ex.Message}");
            }
            return list;
        }

        // ════════════════════════════════════════════════════════════════
        //  PUBLIC: AddUser
        // ════════════════════════════════════════════════════════════════
        public static bool AddUser(UserModel user, string plainPassword)
        {
            try
            {
                string hashed = BCrypt.Net.BCrypt.HashPassword(plainPassword);
                using var conn = GetConnection();
                const string sql = @"
                    INSERT INTO users (full_name, username, password, role, status)
                    VALUES (@fullName, @username, @password, @role, @status);";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@fullName", user.FullName);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", hashed);
                cmd.Parameters.AddWithValue("@role", user.Role);
                cmd.Parameters.AddWithValue("@status", user.Status);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[DatabaseManager] AddUser error: {ex.Message}");
                return false;
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  PUBLIC: UpdateUser
        // ════════════════════════════════════════════════════════════════
        public static bool UpdateUser(UserModel user, string? newPlainPassword)
        {
            try
            {
                using var conn = GetConnection();
                string sql;
                MySqlCommand cmd;

                if (!string.IsNullOrWhiteSpace(newPlainPassword))
                {
                    string hashed = BCrypt.Net.BCrypt.HashPassword(newPlainPassword);
                    sql = @"UPDATE users
                            SET full_name=@fullName, username=@username,
                                password=@password, role=@role, status=@status
                            WHERE id=@id;";
                    cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@password", hashed);
                }
                else
                {
                    sql = @"UPDATE users
                            SET full_name=@fullName, username=@username,
                                role=@role, status=@status
                            WHERE id=@id;";
                    cmd = new MySqlCommand(sql, conn);
                }

                cmd.Parameters.AddWithValue("@fullName", user.FullName);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@role", user.Role);
                cmd.Parameters.AddWithValue("@status", user.Status);
                cmd.Parameters.AddWithValue("@id", user.Id);
                int rows = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return rows > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[DatabaseManager] UpdateUser error: {ex.Message}");
                return false;
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  PUBLIC: DeleteUser
        // ════════════════════════════════════════════════════════════════
        public static bool DeleteUser(int userId)
        {
            try
            {
                using var conn = GetConnection();
                using var cmd = new MySqlCommand(
                    "DELETE FROM users WHERE id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", userId);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[DatabaseManager] DeleteUser error: {ex.Message}");
                return false;
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  PUBLIC: UserExists
        // ════════════════════════════════════════════════════════════════
        public static bool UserExists(string username, int excludeId = 0)
        {
            try
            {
                using var conn = GetConnection();
                const string sql =
                    "SELECT COUNT(*) FROM users " +
                    "WHERE username = @username AND id <> @excludeId;";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", username.Trim());
                cmd.Parameters.AddWithValue("@excludeId", excludeId);
                return Convert.ToInt64(cmd.ExecuteScalar()) > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[DatabaseManager] UserExists error: {ex.Message}");
                return false;
            }
        }
    }
}