using System;

namespace SolidRefactoringLab
{
    public class ReportManager
    {

        private readonly IReportFormatter _formatter;

        // Inject formatter into ReportManager
        public ReportManager(IReportFormatter formatter)
        {
            _formatter = formatter;
        }
        public string ReportData { get; set; } = string.Empty; // Removes nullable warning

        public void GenerateReport()
        {
            _formatter.FormatReport();
        }
    }

    // 2. FIX OCP: Remove the if/else block and create a contract that each format fulfills
    public interface IReportFormatter
    {
        void FormatReport();
    }

    // 2.1 FIX OCP: Create three new classes that implement the IReportFormatter interface for each report format

    public class PdfFormatter : IReportFormatter
    {
        public void FormatReport()
        {
            Console.WriteLine("Formatting report as a PDF...");
        }
    }

    public class WordFormatter : IReportFormatter
    {
        public void FormatReport()
        {
            Console.WriteLine("Formatting report as a Word document...");
        }
    }

    public class PlainTextFormatter : IReportFormatter
    {
        public void FormatReport()
        {
            Console.WriteLine("Formatting report as plain text...");
        }
    }

    // 1. FIX SRP: Move SaveToFile out of ReportManager and put it in its own class
    public class ReportSaver
    {
        public void SaveToFile(string filename)
        {
            Console.WriteLine($"Saving report data to {filename}...");
        }
    }

    class Program
    {
        // Updated Main method  
        static void Main(string[] args)
        {
            // Choose the formatter (OCP)
            IReportFormatter formatter = new PdfFormatter();

            // Pass it to the manager
            ReportManager manager = new ReportManager(formatter);
            manager.GenerateReport();

            // Use the dedicated saver (SRP)
            ReportSaver saver = new ReportSaver();
            saver.SaveToFile("Q1_Report.pdf");
        }
    }
}
