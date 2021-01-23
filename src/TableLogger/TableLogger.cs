using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;

namespace TableLogger
{
    public class TableLogger
    {
        private readonly List<string> _rows = new();
        private readonly List<string> _columns = new();
        private readonly Dictionary<Type, ILogProvider> _logProviders = new();

        public TableLogger(params string[] columns)
        {
            _columns.AddRange(columns.ToList());
            _logProviders.Add(typeof(ConsoleLogProvider), new ConsoleLogProvider());
        }

        public IReadOnlyCollection<string> Rows => _rows;
        public IReadOnlyCollection<string> Columns => _columns;

        public void AddRow(params string[] rowData)
        {
            Guard.Against.NullOrEmpty(rowData, nameof(rowData));

            _rows.AddRange(rowData);
        }

        public void WriteTable()
        {
            if (!_rows.Any())
                return;

            foreach (var row in _rows)
            {
                _logProviders.Values.ToList()
                    .ForEach(logger => logger.WriteLine(row));
            }
        }

        public void AddLogProvider(ILogProvider logProvider)
        {
            if (_logProviders.ContainsKey(logProvider.GetType()))
                throw new InvalidOperationException($"The log provider of type \"{logProvider.GetType()}\" are already registered as a provider.");

            _logProviders.Add(logProvider.GetType(),  logProvider);
        }
    }
}