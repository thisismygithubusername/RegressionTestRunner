using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.GData.Client;
using Google.GData.Spreadsheets;

namespace RegressionTestRunner.Models
{

    public static class GSpreadsheetDataFetcher
    {
        private const int TestNameColumn = 1;
        private const int SecondColumn = 1;
        private const int ThirdColumn = 2;
        private const int ForthColumn = 3; 
        private const string SpreadSheetName = "Updated Broken Tests Spreadsheet";
        
        /*
         * Returns a List of strings retrieved from a cells value
         */
        public static List<String> ImportTestColumnFromGoogleDoc()
        {
            var myService = GetSpreadsheetService();
            var updatedTestsSheet = GetSpreadSheet(myService);
            var wsFeed = updatedTestsSheet.Worksheets;
            var worksheet = (WorksheetEntry)wsFeed.Entries[0];

            Console.WriteLine("Current SpreadSheet is: " + updatedTestsSheet.Title.Text);
            Console.WriteLine("Current Worksheet is: " + worksheet.Title.Text);

            return GetCellValuesForSecondColumn(myService, worksheet);

        }

        /*
         * Returns a spreadsheet from a SpreadsheetService
         */
        public static SpreadsheetEntry GetSpreadSheet(SpreadsheetsService myService)
        {

            var query = new SpreadsheetQuery();
            SpreadsheetFeed spreadSheetFeed = myService.Query(query);
            SpreadsheetEntry mySpreadSheet = FindSpreadSheetByName(spreadSheetFeed, SpreadSheetName);

            return mySpreadSheet;
        }

        public static List<String> GetCellValuesForTestColumn(SpreadsheetsService myService, WorksheetEntry entry)
        {
            return GetColumnCellsValue(myService, entry, TestNameColumn);
        }
        public static List<String> GetCellValuesForSecondColumn(SpreadsheetsService myService, WorksheetEntry entry)
        {
            return GetColumnCellsValue(myService, entry, SecondColumn);
        }

        public static List<String> GetCellValuesForThirdColumn(SpreadsheetsService myService, WorksheetEntry entry)
        {
            return GetColumnCellsValue(myService, entry, ThirdColumn);
        }

        public static List<String> GetCellValuesForFourthColumn(SpreadsheetsService myService, WorksheetEntry entry)
        {
            return GetColumnCellsValue(myService, entry, ForthColumn);
        }

/****************************************************************** Private helpers ***************************************************/


        private static SpreadsheetsService GetSpreadsheetService()
        {
            var myService = new SpreadsheetsService("myApp");
            myService.setUserCredentials("jorge.salcedo@mindbodyonline.com", "Na9ch1to");
            return myService;
        }

        /*
         * Helper methods that finds the sreadsheet based on a name
         */
        private static SpreadsheetEntry FindSpreadSheetByName(SpreadsheetFeed feed, String name)
        {
            foreach (SpreadsheetEntry entry in feed.Entries)
            {
                if (entry.Title.Text.Contains(name))
                {
                    return entry;
                }
            }
            return new SpreadsheetEntry();
        }

        /*
         * Get Test names from google docs 
         */
        private static List<String> GetColumnCellsValue(SpreadsheetsService myService, WorksheetEntry entry, uint column)
        {
            var result = new List<String>();
            AtomLink cellFeedLink = entry.Links.FindService(GDataSpreadsheetsNameTable.CellRel, null);

            CellQuery query = new CellQuery(cellFeedLink.HRef.ToString());
            query.MinimumColumn = column;
            query.MaximumColumn = column;
            query.MinimumRow = 1;

            CellFeed feed = myService.Query(query);
            foreach (CellEntry curCell in feed.Entries)
            {
                result.Add(curCell.Cell.Value);
            }
            return result;
        }
    }
}
