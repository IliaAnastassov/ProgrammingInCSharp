namespace Chapter2_Objective2
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;

    public class Program
    {
        public static void Main(string[] args)
        {
        }

        private static void UseExplicitInterfaceIplementedMethod()
        {
            var implementer = new Implementation();
            ((IInterfaceA)implementer).InterfaceMethod();
        }

        private static void UseDynamicObject()
        {
            dynamic myObject = new SampleObject();
            Console.WriteLine(myObject.ShmuleyBoteach);
        }

        private static void ExportThreadsToExcel()
        {
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Add();

            dynamic worksheet = excelApp.ActiveSheet;

            worksheet.Cells[1, "A"] = "PROCESS";
            worksheet.Cells[1, "B"] = "THREADS PER PROCESS";
            worksheet.Cells[1, "C"] = "TOTAL THREAD COUNT";
            worksheet.Cells[1, "A"].EntireRow.Font.Bold = true;

            var row = 1;

            foreach (var process in Process.GetProcesses())
            {
                row++;
                worksheet.Cells[row, "A"] = process.ProcessName;
                worksheet.Cells[row, "B"] = process.Threads.Count;
            }

            worksheet.Cells[2, "C"] = "=SUM(B:B)";

            worksheet.Columns[1].AutoFit();
            worksheet.Columns[2].AutoFit();
            worksheet.Columns[3].AutoFit();
        }

        private static void UseDisplayInExcel()
        {
            var entities = new List<dynamic>
            {
                new
                {
                    ColumnA = 1,
                    ColumnB = "Foo"
                },
                new
                {
                    ColumnA = 2,
                    ColumnB = "Bar"
                }
            };

            DispalyInExcel(entities);
        }

        private static void DispalyInExcel(IEnumerable<dynamic> entities)
        {
            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Add();

            dynamic worksheet = excelApp.ActiveSheet;

            worksheet.Cells[1, "A"] = "HEADER A";
            worksheet.Cells[1, "B"] = "HEADER B";

            var row = 1;

            foreach (var entity in entities)
            {
                row++;
                worksheet.Cells[row, "A"] = entity.ColumnA;
                worksheet.Cells[row, "B"] = entity.ColumnB;
            }

            worksheet.Columns[1].AutoFIt();
            worksheet.Columns[2].AutoFIt();
        }

        private static void OpenDbConnection(DbConnection connection)
        {
            if (connection is SqlConnection)
            {
                Console.WriteLine($"The connection to {connection.Database.ToString()} is an SQL connection");
            }
        }

        private static void LogStream(Stream stream)
        {
            var memoryStream = stream as MemoryStream;

            if (memoryStream != null)
            {
                Console.WriteLine($"The stream {memoryStream.ToString()} is a memory stream");
            }
        }

        private static void UseUserDefinedConverion()
        {
            var money = new Money(66.99M);
            decimal amount = money;
            int truncatedAmount = (int)money;

            Console.WriteLine(amount);
            Console.WriteLine(truncatedAmount);
        }
    }
}
