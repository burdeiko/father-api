using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace byTourSearch.Services.Infrastructure
{
    public static class IDataReaderExtensions
    {
        public static string GetStringSafe(this IDataReader reader, int colIndex)
        {
            return GetStringSafe(reader, colIndex, null);
        }

        public static int? GetInt32Safe(this IDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetInt32(colIndex);
            else
                return null;
        }

        public static string GetStringSafe(this IDataReader reader, int colIndex, string defaultValue)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            else
                return defaultValue;
        }

        public static int? GetInt32Safe(this IDataReader reader, string indexName)
        {
            return GetInt32Safe(reader, reader.GetOrdinal(indexName));
        }

        public static string GetStringSafe(this IDataReader reader, string indexName)
        {
            return GetStringSafe(reader, reader.GetOrdinal(indexName));
        }

        public static string GetStringSafe(this IDataReader reader, string indexName, string defaultValue)
        {
            return GetStringSafe(reader, reader.GetOrdinal(indexName), defaultValue);
        }
    }
}
