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
        public sealed class CatalogProduct
        {
            public string Title { get; set; } = string.Empty;
            public string Sub { get; set; } = string.Empty;
            public string ImageUrl { get; set; } = string.Empty;
            public string Price { get; set; } = "₱0.00";
            public string Stock { get; set; } = "0 units";
            public string Desc { get; set; } = string.Empty;
            public string MaxPower { get; set; } = string.Empty;
            public string MaxTorque { get; set; } = string.Empty;
            public string Transmission { get; set; } = string.Empty;
            public string FuelSystem { get; set; } = string.Empty;
            public string Cooling { get; set; } = string.Empty;
            public string FuelCapacity { get; set; } = string.Empty;
            public string SeatHeight { get; set; } = string.Empty;
            public string CurbWeight { get; set; } = string.Empty;
            public string GroundClearance { get; set; } = string.Empty;
            public string Wheelbase { get; set; } = string.Empty;
            public string BrakeSystem { get; set; } = string.Empty;
            public string Suspension { get; set; } = string.Empty;
            public string[] Colors { get; set; } = Array.Empty<string>();
            public string[] Features { get; set; } = Array.Empty<string>();
        }

        public sealed class ProductInput
        {
            public string Title { get; set; } = string.Empty;
            public string ModelYear { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public string ImageUrl { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public string Description { get; set; } = string.Empty;
            public string MaxPower { get; set; } = string.Empty;
            public string MaxTorque { get; set; } = string.Empty;
            public string Transmission { get; set; } = string.Empty;
            public string FuelSystem { get; set; } = string.Empty;
            public string Cooling { get; set; } = string.Empty;
            public string FuelCapacity { get; set; } = string.Empty;
            public string SeatHeight { get; set; } = string.Empty;
            public string CurbWeight { get; set; } = string.Empty;
            public string GroundClearance { get; set; } = string.Empty;
            public string Wheelbase { get; set; } = string.Empty;
            public string BrakeSystem { get; set; } = string.Empty;
            public string Suspension { get; set; } = string.Empty;
            public string Colors { get; set; } = string.Empty;
            public string Features { get; set; } = string.Empty;
        }

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
                SeedDefaultProductsIfEmpty();// Step 4 — insert catalog products
                BackfillProductImageUrls();  // Step 5 — ensure image_url uses link-based values

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
            CreateUsersTableIfMissing();
            CreateProductsTableIfMissing();
        }

        private static void CreateUsersTableIfMissing()
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

        private static void CreateProductsTableIfMissing()
        {
            using var checkConn = GetConnection();
            using var checkCmd = new MySqlCommand(
                "SELECT COUNT(*) FROM information_schema.tables " +
                "WHERE table_schema = @db AND table_name = 'products';",
                checkConn);
            checkCmd.Parameters.AddWithValue("@db", DATABASE_NAME);
            long tableExists = Convert.ToInt64(checkCmd.ExecuteScalar());

            if (tableExists == 0)
            {
                const string sql = @"
                    CREATE TABLE `products` (
                        `id`                INT AUTO_INCREMENT PRIMARY KEY,
                        `title`             VARCHAR(150) NOT NULL,
                        `model_year`        VARCHAR(10) NOT NULL,
                        `category`          VARCHAR(50) NOT NULL,
                        `image_url`         VARCHAR(500) NOT NULL DEFAULT '',
                        `price`             DECIMAL(12,2) NOT NULL,
                        `stock`             INT NOT NULL DEFAULT 0,
                        `description`       TEXT NOT NULL,
                        `max_power`         VARCHAR(100) NOT NULL,
                        `max_torque`        VARCHAR(100) NOT NULL,
                        `transmission`      VARCHAR(100) NOT NULL,
                        `fuel_system`       VARCHAR(100) NOT NULL,
                        `cooling`           VARCHAR(100) NOT NULL,
                        `fuel_capacity`     VARCHAR(50)  NOT NULL,
                        `seat_height`       VARCHAR(50)  NOT NULL,
                        `curb_weight`       VARCHAR(50)  NOT NULL,
                        `ground_clearance`  VARCHAR(50)  NOT NULL,
                        `wheelbase`         VARCHAR(50)  NOT NULL,
                        `brake_system`      VARCHAR(150) NOT NULL,
                        `suspension`        VARCHAR(200) NOT NULL,
                        `colors`            VARCHAR(300) NOT NULL,
                        `features`          VARCHAR(500) NOT NULL,
                        `is_active`         TINYINT(1) NOT NULL DEFAULT 1,
                        `created_at`        DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                        INDEX idx_products_active (`is_active`),
                        INDEX idx_products_title (`title`)
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;";

                using var createCmd = new MySqlCommand(sql, checkConn);
                createCmd.ExecuteNonQuery();

                System.Diagnostics.Debug.WriteLine(
                    "[DatabaseManager] Table `products` created ✔");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(
                    "[DatabaseManager] Table `products` already exists.");
            }

            EnsureProductsSchema(checkConn);
        }

        private static void EnsureProductsSchema(MySqlConnection connection)
        {
            using var checkCmd = new MySqlCommand(
                "SELECT COUNT(*) FROM information_schema.columns " +
                "WHERE table_schema = @db AND table_name = 'products' AND column_name = 'image_url';",
                connection);
            checkCmd.Parameters.AddWithValue("@db", DATABASE_NAME);
            long hasImageUrl = Convert.ToInt64(checkCmd.ExecuteScalar());

            if (hasImageUrl == 0)
            {
                using var alterCmd = new MySqlCommand(
                    "ALTER TABLE products ADD COLUMN image_url VARCHAR(500) NOT NULL DEFAULT '' AFTER category;",
                    connection);
                alterCmd.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine("[DatabaseManager] Added products.image_url column ✔");
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

        private static void SeedDefaultProductsIfEmpty()
        {
            using var conn = GetConnection();
            const string insertSql = @"
                INSERT INTO products
                (title, model_year, category, image_url, price, stock, description,
                 max_power, max_torque, transmission, fuel_system, cooling,
                 fuel_capacity, seat_height, curb_weight, ground_clearance,
                 wheelbase, brake_system, suspension, colors, features)
                SELECT
                 @title, @year, @category, @imageUrl, @price, @stock, @description,
                 @maxPower, @maxTorque, @transmission, @fuelSystem, @cooling,
                 @fuelCapacity, @seatHeight, @curbWeight, @groundClearance,
                 @wheelbase, @brakeSystem, @suspension, @colors, @features
                WHERE NOT EXISTS (
                    SELECT 1 FROM products
                    WHERE title = @title AND model_year = @year
                );";

            var seeds = new[]
            {
                new
                {
                    Title = "Honda ADV 160", Year = "2024", Category = "Honda",
                    Price = 145000m, Stock = 3,
                    Description = "Well-maintained unit with complete papers and low mileage.",
                    MaxPower = "15.8 hp @ 8,500 rpm", MaxTorque = "14.7 Nm @ 6,500 rpm",
                    Transmission = "CVT Automatic", FuelSystem = "PGM-FI", Cooling = "Liquid-cooled",
                    FuelCapacity = "8.1 liters", SeatHeight = "795 mm", CurbWeight = "133 kg",
                    GroundClearance = "165 mm", Wheelbase = "1,324 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "Telescopic Front, Twin Shock Rear",
                    Colors = "Matte Black|Red", Features = "ABS|LED Headlights|Smart Key"
                },
                new
                {
                    Title = "Honda Click 160", Year = "2025", Category = "Honda",
                    Price = 122900m, Stock = 8,
                    Description = "Fuel-efficient scooter perfect for daily city commute.",
                    MaxPower = "15.3 hp @ 8,500 rpm", MaxTorque = "13.8 Nm @ 7,000 rpm",
                    Transmission = "CVT Automatic", FuelSystem = "PGM-FI", Cooling = "Liquid-cooled",
                    FuelCapacity = "5.5 liters", SeatHeight = "778 mm", CurbWeight = "117 kg",
                    GroundClearance = "138 mm", Wheelbase = "1,278 mm",
                    BrakeSystem = "Front Disc / Rear Drum with CBS",
                    Suspension = "Telescopic Front, Unit Swing Rear",
                    Colors = "Pearl White|Matte Black|Red", Features = "Idling Stop|LED Lights|Combi Brake"
                },
                new
                {
                    Title = "Honda PCX 160", Year = "2025", Category = "Honda",
                    Price = 133900m, Stock = 6,
                    Description = "Premium scooter with smart key and modern comfort features.",
                    MaxPower = "15.8 hp @ 8,500 rpm", MaxTorque = "15.0 Nm @ 6,500 rpm",
                    Transmission = "CVT Automatic", FuelSystem = "PGM-FI", Cooling = "Liquid-cooled",
                    FuelCapacity = "8.1 liters", SeatHeight = "764 mm", CurbWeight = "131 kg",
                    GroundClearance = "135 mm", Wheelbase = "1,313 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "Telescopic Front, Twin Shock Rear",
                    Colors = "Silver|Black|Blue", Features = "ABS|Smart Key|USB Charger"
                },
                new
                {
                    Title = "Yamaha NMAX 155", Year = "2024", Category = "Yamaha",
                    Price = 151000m, Stock = 4,
                    Description = "Comfortable city and long-ride scooter with strong resale value.",
                    MaxPower = "15.1 hp @ 8,000 rpm", MaxTorque = "13.9 Nm @ 6,500 rpm",
                    Transmission = "CVT Automatic", FuelSystem = "Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "7.1 liters", SeatHeight = "765 mm", CurbWeight = "131 kg",
                    GroundClearance = "125 mm", Wheelbase = "1,340 mm",
                    BrakeSystem = "Front/Rear Disc, Single Channel ABS",
                    Suspension = "Telescopic Fork, Twin Shock",
                    Colors = "Matte Blue|Gloss Black", Features = "ABS|Traction Control|Smart Key"
                },
                new
                {
                    Title = "Yamaha Aerox 155", Year = "2025", Category = "Yamaha",
                    Price = 132900m, Stock = 5,
                    Description = "Sporty scooter with aggressive styling and excellent acceleration.",
                    MaxPower = "15.1 hp @ 8,000 rpm", MaxTorque = "13.9 Nm @ 6,500 rpm",
                    Transmission = "CVT Automatic", FuelSystem = "Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "5.5 liters", SeatHeight = "790 mm", CurbWeight = "125 kg",
                    GroundClearance = "145 mm", Wheelbase = "1,350 mm",
                    BrakeSystem = "Front/Rear Disc, ABS variant",
                    Suspension = "Telescopic Fork, Twin Shock Rear",
                    Colors = "Black|Blue|Gray", Features = "Smart Key|VVA Engine|LED Lighting"
                },
                new
                {
                    Title = "Yamaha Mio Gear", Year = "2025", Category = "Yamaha",
                    Price = 81900m, Stock = 7,
                    Description = "Practical and lightweight scooter for beginner-friendly riding.",
                    MaxPower = "9.3 hp @ 8,000 rpm", MaxTorque = "9.5 Nm @ 5,500 rpm",
                    Transmission = "CVT Automatic", FuelSystem = "Fuel Injection", Cooling = "Air-cooled",
                    FuelCapacity = "4.2 liters", SeatHeight = "750 mm", CurbWeight = "95 kg",
                    GroundClearance = "135 mm", Wheelbase = "1,260 mm",
                    BrakeSystem = "Front Disc / Rear Drum",
                    Suspension = "Telescopic Front, Unit Swing Rear",
                    Colors = "Red|Blue|Black", Features = "Stop & Start System|LED Position Light|Utility Hook"
                },
                new
                {
                    Title = "Kawasaki Ninja 400", Year = "2024", Category = "Kawasaki",
                    Price = 331000m, Stock = 2,
                    Description = "Entry-level sportbike with balanced power and modern ergonomics.",
                    MaxPower = "47 hp @ 10,000 rpm", MaxTorque = "38 Nm @ 8,000 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "14.0 liters", SeatHeight = "785 mm", CurbWeight = "167 kg",
                    GroundClearance = "140 mm", Wheelbase = "1,370 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "Telescopic Front, Uni-Trak Rear",
                    Colors = "Lime Green|Black", Features = "ABS|Assist & Slipper Clutch|LED Tail Light"
                },
                new
                {
                    Title = "Kawasaki Z400", Year = "2025", Category = "Kawasaki",
                    Price = 279000m, Stock = 3,
                    Description = "Naked bike with responsive handling for urban and weekend rides.",
                    MaxPower = "47 hp @ 10,000 rpm", MaxTorque = "38 Nm @ 8,000 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "14.0 liters", SeatHeight = "785 mm", CurbWeight = "167 kg",
                    GroundClearance = "145 mm", Wheelbase = "1,370 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "Telescopic Front, Horizontal Back-link Rear",
                    Colors = "Green|Gray", Features = "ABS|Slipper Clutch|Digital Cluster"
                },
                new
                {
                    Title = "Kawasaki Barako II", Year = "2025", Category = "Kawasaki",
                    Price = 98000m, Stock = 6,
                    Description = "Durable workhorse motorcycle built for heavy daily use.",
                    MaxPower = "12.0 hp @ 7,500 rpm", MaxTorque = "11.7 Nm @ 5,500 rpm",
                    Transmission = "4-Speed Manual", FuelSystem = "Carburetor", Cooling = "Air-cooled",
                    FuelCapacity = "13.0 liters", SeatHeight = "775 mm", CurbWeight = "112 kg",
                    GroundClearance = "165 mm", Wheelbase = "1,285 mm",
                    BrakeSystem = "Front Disc / Rear Drum",
                    Suspension = "Telescopic Front, Twin Shock Rear",
                    Colors = "Black|Red", Features = "Heavy-Duty Carrier|Electric/Kick Start|Utility Build"
                },
                new
                {
                    Title = "Suzuki Raider R150", Year = "2025", Category = "Suzuki",
                    Price = 178000m, Stock = 5,
                    Description = "High-performance underbone ideal for daily use and spirited riding.",
                    MaxPower = "18.2 hp @ 10,000 rpm", MaxTorque = "13.8 Nm @ 8,500 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "4.0 liters", SeatHeight = "764 mm", CurbWeight = "109 kg",
                    GroundClearance = "150 mm", Wheelbase = "1,280 mm",
                    BrakeSystem = "Front Disc / Rear Disc",
                    Suspension = "Telescopic Front, Swingarm Rear",
                    Colors = "Blue|Black|Red", Features = "DOHC Engine|Digital Panel|LED Tail Light"
                },
                new
                {
                    Title = "Suzuki Burgman Street", Year = "2024", Category = "Suzuki",
                    Price = 92400m, Stock = 7,
                    Description = "Stylish maxi-scooter inspired commuter with practical storage.",
                    MaxPower = "8.7 hp @ 6,750 rpm", MaxTorque = "10 Nm @ 5,500 rpm",
                    Transmission = "CVT Automatic", FuelSystem = "Fuel Injection", Cooling = "Air-cooled",
                    FuelCapacity = "5.5 liters", SeatHeight = "780 mm", CurbWeight = "110 kg",
                    GroundClearance = "160 mm", Wheelbase = "1,265 mm",
                    BrakeSystem = "Front Disc / Rear Drum",
                    Suspension = "Telescopic Front, Swingarm Rear",
                    Colors = "Matte Black|Pearl White", Features = "SEP Engine|USB Charger|Large Footboard"
                },
                new
                {
                    Title = "Suzuki GSX-S150", Year = "2025", Category = "Suzuki",
                    Price = 168000m, Stock = 4,
                    Description = "Lightweight naked bike with sporty handling and sharp throttle response.",
                    MaxPower = "18.9 hp @ 10,500 rpm", MaxTorque = "14 Nm @ 9,000 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "11.0 liters", SeatHeight = "785 mm", CurbWeight = "130 kg",
                    GroundClearance = "155 mm", Wheelbase = "1,300 mm",
                    BrakeSystem = "Front/Rear Disc",
                    Suspension = "Telescopic Front, Link-Type Rear",
                    Colors = "Blue|Black|Red", Features = "LED Headlight|DOHC|Digital Meter"
                },
                new
                {
                    Title = "KTM Duke 200", Year = "2025", Category = "KTM",
                    Price = 165000m, Stock = 3,
                    Description = "Streetfighter with agile chassis and punchy single-cylinder engine.",
                    MaxPower = "25 hp @ 10,000 rpm", MaxTorque = "19.3 Nm @ 8,000 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "13.5 liters", SeatHeight = "822 mm", CurbWeight = "159 kg",
                    GroundClearance = "170 mm", Wheelbase = "1,357 mm",
                    BrakeSystem = "Front/Rear Disc with Dual-Channel ABS",
                    Suspension = "WP USD Front, WP Monoshock Rear",
                    Colors = "Orange|Black", Features = "ABS|TFT Display|Slipper Clutch"
                },
                new
                {
                    Title = "KTM RC 200", Year = "2024", Category = "KTM",
                    Price = 165000m, Stock = 4,
                    Description = "Track-inspired sportbike with aggressive riding ergonomics.",
                    MaxPower = "25 hp @ 10,000 rpm", MaxTorque = "19.2 Nm @ 8,000 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "13.7 liters", SeatHeight = "835 mm", CurbWeight = "160 kg",
                    GroundClearance = "158 mm", Wheelbase = "1,340 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "WP APEX USD Front, Monoshock Rear",
                    Colors = "Orange|Blue", Features = "ABS|Race Ergonomics|LED Projector"
                },
                new
                {
                    Title = "KTM Duke 390", Year = "2025", Category = "KTM",
                    Price = 310000m, Stock = 2,
                    Description = "High-performance lightweight naked bike with premium electronics.",
                    MaxPower = "44 hp @ 9,000 rpm", MaxTorque = "37 Nm @ 7,000 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "15.0 liters", SeatHeight = "820 mm", CurbWeight = "168 kg",
                    GroundClearance = "183 mm", Wheelbase = "1,357 mm",
                    BrakeSystem = "Front/Rear Disc with Cornering ABS",
                    Suspension = "WP USD Front, Adjustable Monoshock",
                    Colors = "Orange|Gray", Features = "Quickshifter|TFT|Ride Modes"
                },
                new
                {
                    Title = "Rusi Rapid 150", Year = "2025", Category = "Rusi",
                    Price = 69900m, Stock = 10,
                    Description = "Budget-friendly motorcycle ideal for practical daily transport.",
                    MaxPower = "11.0 hp @ 8,000 rpm", MaxTorque = "11.0 Nm @ 6,500 rpm",
                    Transmission = "5-Speed Manual", FuelSystem = "Carburetor", Cooling = "Air-cooled",
                    FuelCapacity = "11.0 liters", SeatHeight = "780 mm", CurbWeight = "118 kg",
                    GroundClearance = "160 mm", Wheelbase = "1,290 mm",
                    BrakeSystem = "Front Disc / Rear Drum",
                    Suspension = "Telescopic Front, Twin Shock Rear",
                    Colors = "Black|Red", Features = "Electric Start|Alloy Wheels|Economical"
                },
                new
                {
                    Title = "Rusi Classic 250", Year = "2024", Category = "Rusi",
                    Price = 89900m, Stock = 6,
                    Description = "Retro-styled bike offering comfort and value.",
                    MaxPower = "16.0 hp @ 7,500 rpm", MaxTorque = "18.0 Nm @ 6,000 rpm",
                    Transmission = "5-Speed Manual", FuelSystem = "Carburetor", Cooling = "Air-cooled",
                    FuelCapacity = "14.0 liters", SeatHeight = "770 mm", CurbWeight = "145 kg",
                    GroundClearance = "165 mm", Wheelbase = "1,360 mm",
                    BrakeSystem = "Front/Rear Disc",
                    Suspension = "Telescopic Front, Twin Shock Rear",
                    Colors = "Green|Black|Cream", Features = "Retro Design|Dual Disc|Comfort Seat"
                },
                new
                {
                    Title = "Rusi Sigma 250", Year = "2025", Category = "Rusi",
                    Price = 109900m, Stock = 4,
                    Description = "Touring-ready machine with strong value for long rides.",
                    MaxPower = "20.0 hp @ 8,000 rpm", MaxTorque = "21.0 Nm @ 6,500 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Fuel Injection", Cooling = "Oil-cooled",
                    FuelCapacity = "16.0 liters", SeatHeight = "800 mm", CurbWeight = "158 kg",
                    GroundClearance = "170 mm", Wheelbase = "1,390 mm",
                    BrakeSystem = "Front/Rear Disc with CBS",
                    Suspension = "Telescopic Front, Monoshock Rear",
                    Colors = "Black|Gray", Features = "Digital Panel|Large Tank|USB Charger"
                },
                new
                {
                    Title = "Harley-Davidson Sportster S", Year = "2024", Category = "Harley-Davidson",
                    Price = 1290000m, Stock = 1,
                    Description = "Power cruiser with iconic style and modern performance.",
                    MaxPower = "121 hp @ 7,500 rpm", MaxTorque = "125 Nm @ 6,000 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Electronic Sequential Port Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "11.8 liters", SeatHeight = "765 mm", CurbWeight = "228 kg",
                    GroundClearance = "90 mm", Wheelbase = "1,520 mm",
                    BrakeSystem = "Brembo Front/Rear Disc with ABS",
                    Suspension = "USD Front, Linkage Monoshock Rear",
                    Colors = "Vivid Black|Gray Haze", Features = "Ride Modes|ABS|Traction Control"
                },
                new
                {
                    Title = "Harley-Davidson Iron 883", Year = "2023", Category = "Harley-Davidson",
                    Price = 760000m, Stock = 2,
                    Description = "Classic urban cruiser with authentic Harley character.",
                    MaxPower = "49 hp @ 5,500 rpm", MaxTorque = "73 Nm @ 3,750 rpm",
                    Transmission = "5-Speed Manual", FuelSystem = "Electronic Fuel Injection", Cooling = "Air-cooled",
                    FuelCapacity = "12.5 liters", SeatHeight = "760 mm", CurbWeight = "256 kg",
                    GroundClearance = "140 mm", Wheelbase = "1,520 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "Telescopic Front, Twin Shock Rear",
                    Colors = "Black Denim|Gray", Features = "ABS|Keyless Security|Low Seat"
                },
                new
                {
                    Title = "Harley-Davidson Street Bob", Year = "2024", Category = "Harley-Davidson",
                    Price = 1150000m, Stock = 1,
                    Description = "Softail cruiser blending classic silhouette and modern torque.",
                    MaxPower = "86 hp @ 5,000 rpm", MaxTorque = "155 Nm @ 3,000 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Electronic Fuel Injection", Cooling = "Air/Oil-cooled",
                    FuelCapacity = "13.2 liters", SeatHeight = "680 mm", CurbWeight = "297 kg",
                    GroundClearance = "125 mm", Wheelbase = "1,630 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "Telescopic Front, Hidden Monoshock Rear",
                    Colors = "Black|Red", Features = "ABS|LED Lighting|Cruise-ready"
                },
                new
                {
                    Title = "CFMOTO 300NK", Year = "2025", Category = "CFMOTO",
                    Price = 172000m, Stock = 6,
                    Description = "Entry-level naked bike with sharp styling and smooth engine.",
                    MaxPower = "27 hp @ 8,750 rpm", MaxTorque = "25 Nm @ 7,000 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "12.5 liters", SeatHeight = "795 mm", CurbWeight = "151 kg",
                    GroundClearance = "150 mm", Wheelbase = "1,360 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "USD Front, Monoshock Rear",
                    Colors = "Black|Blue", Features = "ABS|TFT Screen|LED Lighting"
                },
                new
                {
                    Title = "CFMOTO 450SR", Year = "2025", Category = "CFMOTO",
                    Price = 328000m, Stock = 3,
                    Description = "Sportbike with premium looks and strong twin-cylinder performance.",
                    MaxPower = "50 hp @ 9,500 rpm", MaxTorque = "39 Nm @ 7,600 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "14.0 liters", SeatHeight = "795 mm", CurbWeight = "168 kg",
                    GroundClearance = "140 mm", Wheelbase = "1,370 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "USD Front, Multi-link Rear",
                    Colors = "Nebula Black|Zircon Black", Features = "ABS|Slipper Clutch|TCS"
                },
                new
                {
                    Title = "CFMOTO 650MT", Year = "2024", Category = "CFMOTO",
                    Price = 358000m, Stock = 2,
                    Description = "Adventure-touring bike designed for comfort and long-distance rides.",
                    MaxPower = "60 hp @ 8,750 rpm", MaxTorque = "56 Nm @ 7,000 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "18.0 liters", SeatHeight = "840 mm", CurbWeight = "218 kg",
                    GroundClearance = "170 mm", Wheelbase = "1,425 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "USD Front, Monoshock Rear",
                    Colors = "Blue|Gray", Features = "ABS|Adjustable Windshield|TFT"
                },
                new
                {
                    Title = "BMW G 310 R", Year = "2025", Category = "BMW",
                    Price = 295000m, Stock = 1,
                    Description = "Premium entry naked bike with refined ride quality.",
                    MaxPower = "34 hp @ 9,500 rpm", MaxTorque = "28 Nm @ 7,500 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Electronic Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "11.0 liters", SeatHeight = "785 mm", CurbWeight = "158 kg",
                    GroundClearance = "165 mm", Wheelbase = "1,374 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "USD Front, Rear Monoshock",
                    Colors = "Racing Blue|Triple Black", Features = "ABS|Ride-by-Wire|LED Headlight"
                },
                new
                {
                    Title = "BMW G 310 GS", Year = "2025", Category = "BMW",
                    Price = 335000m, Stock = 2,
                    Description = "Adventure-ready lightweight bike for mixed road conditions.",
                    MaxPower = "34 hp @ 9,500 rpm", MaxTorque = "28 Nm @ 7,500 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Electronic Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "11.0 liters", SeatHeight = "835 mm", CurbWeight = "175 kg",
                    GroundClearance = "220 mm", Wheelbase = "1,420 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "USD Front, Long-travel Monoshock",
                    Colors = "White|Black", Features = "ABS|Adjustable Levers|Dual-purpose Setup"
                },
                new
                {
                    Title = "BMW C 400 GT", Year = "2024", Category = "BMW",
                    Price = 595000m, Stock = 1,
                    Description = "Premium maxi-scooter with touring comfort and storage convenience.",
                    MaxPower = "34 hp @ 7,500 rpm", MaxTorque = "35 Nm @ 5,750 rpm",
                    Transmission = "CVT Automatic", FuelSystem = "Electronic Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "12.8 liters", SeatHeight = "775 mm", CurbWeight = "214 kg",
                    GroundClearance = "130 mm", Wheelbase = "1,565 mm",
                    BrakeSystem = "Dual Disc Front / Single Disc Rear with ABS",
                    Suspension = "Telescopic Front, Twin Shock Rear",
                    Colors = "Black Storm|Alpine White", Features = "ABS|TFT|Heated Grips"
                },
                new
                {
                    Title = "Ducati Monster", Year = "2024", Category = "Ducati",
                    Price = 875000m, Stock = 1,
                    Description = "Iconic naked sportbike with strong torque and premium handling.",
                    MaxPower = "111 hp @ 9,250 rpm", MaxTorque = "93 Nm @ 6,500 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Electronic Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "14.0 liters", SeatHeight = "820 mm", CurbWeight = "188 kg",
                    GroundClearance = "125 mm", Wheelbase = "1,474 mm",
                    BrakeSystem = "Brembo Front/Rear Disc with ABS",
                    Suspension = "USD Front, Rear Monoshock",
                    Colors = "Ducati Red|Dark Stealth", Features = "Quickshifter|ABS Cornering|Traction Control"
                },
                new
                {
                    Title = "Ducati Scrambler Icon", Year = "2025", Category = "Ducati",
                    Price = 780000m, Stock = 2,
                    Description = "Retro-modern lifestyle bike with approachable ergonomics.",
                    MaxPower = "73 hp @ 8,250 rpm", MaxTorque = "65 Nm @ 7,000 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Electronic Fuel Injection", Cooling = "Air-cooled",
                    FuelCapacity = "13.5 liters", SeatHeight = "798 mm", CurbWeight = "189 kg",
                    GroundClearance = "170 mm", Wheelbase = "1,445 mm",
                    BrakeSystem = "Front/Rear Disc with ABS",
                    Suspension = "USD Front, Kayaba Rear Monoshock",
                    Colors = "Yellow|Black|Orange", Features = "ABS|Ride Modes|TFT Display"
                },
                new
                {
                    Title = "Ducati Panigale V2", Year = "2024", Category = "Ducati",
                    Price = 1295000m, Stock = 1,
                    Description = "Track-focused supersport with advanced electronics package.",
                    MaxPower = "155 hp @ 10,750 rpm", MaxTorque = "104 Nm @ 9,000 rpm",
                    Transmission = "6-Speed Manual", FuelSystem = "Electronic Fuel Injection", Cooling = "Liquid-cooled",
                    FuelCapacity = "17.0 liters", SeatHeight = "840 mm", CurbWeight = "200 kg",
                    GroundClearance = "120 mm", Wheelbase = "1,436 mm",
                    BrakeSystem = "Brembo Front/Rear Disc with ABS",
                    Suspension = "Showa BPF Front, Sachs Rear",
                    Colors = "Ducati Red|White", Features = "Quickshifter|Cornering ABS|Wheelie Control"
                }
            };

            int inserted = 0;
            foreach (var s in seeds)
            {
                using var cmd = new MySqlCommand(insertSql, conn);
                cmd.Parameters.AddWithValue("@title", s.Title);
                cmd.Parameters.AddWithValue("@year", s.Year);
                cmd.Parameters.AddWithValue("@category", s.Category);
                // URL-based image path so you can replace each source with your own link later.
                cmd.Parameters.AddWithValue("@imageUrl", BuildDefaultImageUrl(s.Title));
                cmd.Parameters.AddWithValue("@price", s.Price);
                cmd.Parameters.AddWithValue("@stock", s.Stock);
                cmd.Parameters.AddWithValue("@description", s.Description);
                cmd.Parameters.AddWithValue("@maxPower", s.MaxPower);
                cmd.Parameters.AddWithValue("@maxTorque", s.MaxTorque);
                cmd.Parameters.AddWithValue("@transmission", s.Transmission);
                cmd.Parameters.AddWithValue("@fuelSystem", s.FuelSystem);
                cmd.Parameters.AddWithValue("@cooling", s.Cooling);
                cmd.Parameters.AddWithValue("@fuelCapacity", s.FuelCapacity);
                cmd.Parameters.AddWithValue("@seatHeight", s.SeatHeight);
                cmd.Parameters.AddWithValue("@curbWeight", s.CurbWeight);
                cmd.Parameters.AddWithValue("@groundClearance", s.GroundClearance);
                cmd.Parameters.AddWithValue("@wheelbase", s.Wheelbase);
                cmd.Parameters.AddWithValue("@brakeSystem", s.BrakeSystem);
                cmd.Parameters.AddWithValue("@suspension", s.Suspension);
                cmd.Parameters.AddWithValue("@colors", s.Colors);
                cmd.Parameters.AddWithValue("@features", s.Features);
                inserted += cmd.ExecuteNonQuery();
            }

            System.Diagnostics.Debug.WriteLine($"[DatabaseManager] Product seed sync complete ✔ Added: {inserted}");
        }

        private static void BackfillProductImageUrls()
        {
            try
            {
                using var conn = GetConnection();
                const string selectSql = "SELECT id, title, image_url FROM products;";
                using var selectCmd = new MySqlCommand(selectSql, conn);
                using var reader = selectCmd.ExecuteReader();

                var updates = new List<(int Id, string Url)>();
                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string title = reader.GetString("title");
                    string imageUrl = reader.IsDBNull(reader.GetOrdinal("image_url"))
                        ? string.Empty
                        : reader.GetString("image_url");

                    // If no link yet (or legacy local path), switch to URL-based placeholder.
                    bool needsBackfill = string.IsNullOrWhiteSpace(imageUrl)
                        || imageUrl.StartsWith("images/products/", StringComparison.OrdinalIgnoreCase);

                    if (needsBackfill)
                    {
                        updates.Add((id, BuildDefaultImageUrl(title)));
                    }
                }

                reader.Close();

                if (updates.Count == 0)
                {
                    return;
                }

                const string updateSql = "UPDATE products SET image_url = @url WHERE id = @id;";
                foreach (var item in updates)
                {
                    using var updateCmd = new MySqlCommand(updateSql, conn);
                    updateCmd.Parameters.AddWithValue("@url", item.Url);
                    updateCmd.Parameters.AddWithValue("@id", item.Id);
                    updateCmd.ExecuteNonQuery();
                }

                System.Diagnostics.Debug.WriteLine($"[DatabaseManager] Product image URL backfill complete ✔ Updated: {updates.Count}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DatabaseManager] BackfillProductImageUrls error: {ex.Message}");
            }
        }

        public static List<CatalogProduct> GetCatalogProducts()
        {
            var list = new List<CatalogProduct>();
            try
            {
                using var conn = GetConnection();
                const string sql = @"
                    SELECT title, model_year, category, image_url, price, stock, description,
                           max_power, max_torque, transmission, fuel_system, cooling,
                           fuel_capacity, seat_height, curb_weight, ground_clearance,
                           wheelbase, brake_system, suspension, colors, features
                    FROM products
                    WHERE is_active = 1
                    ORDER BY id;";

                using var cmd = new MySqlCommand(sql, conn);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    string title = r.GetString("title");
                    string imageUrl = r.IsDBNull(r.GetOrdinal("image_url"))
                        ? string.Empty
                        : r.GetString("image_url");

                    list.Add(new CatalogProduct
                    {
                        Title = title,
                        Sub = $"{r.GetString("model_year")} • {r.GetString("category")}",
                        // Always return a URL so card rendering can load immediately.
                        ImageUrl = string.IsNullOrWhiteSpace(imageUrl) ? BuildDefaultImageUrl(title) : imageUrl,
                        Price = $"₱{r.GetDecimal("price"):N2}",
                        Stock = $"{r.GetInt32("stock")} units",
                        Desc = r.GetString("description"),
                        MaxPower = r.GetString("max_power"),
                        MaxTorque = r.GetString("max_torque"),
                        Transmission = r.GetString("transmission"),
                        FuelSystem = r.GetString("fuel_system"),
                        Cooling = r.GetString("cooling"),
                        FuelCapacity = r.GetString("fuel_capacity"),
                        SeatHeight = r.GetString("seat_height"),
                        CurbWeight = r.GetString("curb_weight"),
                        GroundClearance = r.GetString("ground_clearance"),
                        Wheelbase = r.GetString("wheelbase"),
                        BrakeSystem = r.GetString("brake_system"),
                        Suspension = r.GetString("suspension"),
                        Colors = SplitPipeList(r.GetString("colors")),
                        Features = SplitPipeList(r.GetString("features"))
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[DatabaseManager] GetCatalogProducts error: {ex.Message}");
            }

            return list;
        }

        public static bool AddProduct(ProductInput input)
        {
            try
            {
                string title = input.Title.Trim();
                if (string.IsNullOrWhiteSpace(title))
                {
                    return false;
                }

                string modelYear = string.IsNullOrWhiteSpace(input.ModelYear) ? DateTime.Now.Year.ToString() : input.ModelYear.Trim();
                string category = string.IsNullOrWhiteSpace(input.Category) ? "Uncategorized" : input.Category.Trim();
                string imageUrl = string.IsNullOrWhiteSpace(input.ImageUrl) ? BuildDefaultImageUrl(title) : input.ImageUrl.Trim();

                using var conn = GetConnection();
                const string sql = @"
                    INSERT INTO products
                    (title, model_year, category, image_url, price, stock, description,
                     max_power, max_torque, transmission, fuel_system, cooling,
                     fuel_capacity, seat_height, curb_weight, ground_clearance,
                     wheelbase, brake_system, suspension, colors, features, is_active)
                    VALUES
                    (@title, @modelYear, @category, @imageUrl, @price, @stock, @description,
                     @maxPower, @maxTorque, @transmission, @fuelSystem, @cooling,
                     @fuelCapacity, @seatHeight, @curbWeight, @groundClearance,
                     @wheelbase, @brakeSystem, @suspension, @colors, @features, 1);";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@modelYear", modelYear);
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@imageUrl", imageUrl);
                cmd.Parameters.AddWithValue("@price", input.Price);
                cmd.Parameters.AddWithValue("@stock", input.Stock);
                cmd.Parameters.AddWithValue("@description", input.Description.Trim());
                cmd.Parameters.AddWithValue("@maxPower", input.MaxPower.Trim());
                cmd.Parameters.AddWithValue("@maxTorque", input.MaxTorque.Trim());
                cmd.Parameters.AddWithValue("@transmission", input.Transmission.Trim());
                cmd.Parameters.AddWithValue("@fuelSystem", input.FuelSystem.Trim());
                cmd.Parameters.AddWithValue("@cooling", input.Cooling.Trim());
                cmd.Parameters.AddWithValue("@fuelCapacity", input.FuelCapacity.Trim());
                cmd.Parameters.AddWithValue("@seatHeight", input.SeatHeight.Trim());
                cmd.Parameters.AddWithValue("@curbWeight", input.CurbWeight.Trim());
                cmd.Parameters.AddWithValue("@groundClearance", input.GroundClearance.Trim());
                cmd.Parameters.AddWithValue("@wheelbase", input.Wheelbase.Trim());
                cmd.Parameters.AddWithValue("@brakeSystem", input.BrakeSystem.Trim());
                cmd.Parameters.AddWithValue("@suspension", input.Suspension.Trim());
                cmd.Parameters.AddWithValue("@colors", input.Colors.Trim());
                cmd.Parameters.AddWithValue("@features", input.Features.Trim());

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DatabaseManager] AddProduct error: {ex.Message}");
                return false;
            }
        }

        private static string[] SplitPipeList(string value)
        {
            return value.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        private static string BuildDefaultImageUrl(string title)
        {
            // Link-based fallback image generator.
            // Replace these URLs in DB anytime with your preferred image sources.
            if (string.IsNullOrWhiteSpace(title))
            {
                return "https://picsum.photos/seed/motorcycle-default/900/500";
            }

            var chars = new List<char>(title.Length);
            foreach (char c in title.ToLowerInvariant())
            {
                if (char.IsLetterOrDigit(c))
                {
                    chars.Add(c);
                }
                else if (chars.Count == 0 || chars[^1] != '-')
                {
                    chars.Add('-');
                }
            }

            string slug = new string(chars.ToArray()).Trim('-');
            if (string.IsNullOrWhiteSpace(slug))
            {
                slug = "motorcycle-default";
            }

            return $"https://picsum.photos/seed/{slug}/900/500";
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