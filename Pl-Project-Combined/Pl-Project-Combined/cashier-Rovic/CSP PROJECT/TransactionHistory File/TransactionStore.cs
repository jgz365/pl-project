using System.Collections.Generic;
using Pl_Project_Combined.Databases;

namespace POSCashierSystem
{
    public static class TransactionStore
    {
        private static readonly List<Transaction> _store = new();

        static TransactionStore()
        {
            Refresh();
        }

        public static IReadOnlyList<Transaction> All => _store.AsReadOnly();

        public static void Add(Transaction tx)
        {
            if (tx == null)
            {
                return;
            }

            if (RecentTransactionDatabase.Add(tx))
            {
                _store.Insert(0, tx);
            }
        }

        public static void Refresh()
        {
            _store.Clear();
            _store.AddRange(RecentTransactionDatabase.GetAll());
        }

        public static void Clear()
        {
            RecentTransactionDatabase.ClearAll();
            _store.Clear();
        }
    }
}
