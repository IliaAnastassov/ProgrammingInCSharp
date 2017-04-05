namespace Chapter3_Objective5
{
    using System.Diagnostics;

    public class Program
    {
        public static void Main(string[] args)
        {

        }

        private static void UseTraceSourceClass()
        {
            var traceSource = new TraceSource("MyTraceSource", SourceLevels.All);
            traceSource.TraceInformation("Tracing application...");
            traceSource.TraceEvent(TraceEventType.Critical, 0, "Critical trace");
            traceSource.TraceData(TraceEventType.Information, 1, new object[] { "a", "b", "c" });

            traceSource.Flush();
            traceSource.Close();
        }

        private static void UseDebugClass()
        {
            Debug.WriteLine("Starting Application");
            var number = 1;
            number++;
            Debug.Assert(number == 3);
            Debug.WriteLineIf(number > 0, "number is greater than 0");
        }
    }
}
