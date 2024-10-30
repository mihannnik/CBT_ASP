using CBT.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Domain.Options
{
    public class SQLiteOptions : IOption
    {
        public static string SectionName = nameof(SQLiteOptions);
        public required string DataSource { get; set; }
    }
}
