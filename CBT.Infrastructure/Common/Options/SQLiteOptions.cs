﻿namespace CBT.Infrastructure.Common.Options
{
    public class SQLiteOptions
    {
        public static string SectionName = nameof(SQLiteOptions);
        public required string DataSource { get; set; }
    }
}
