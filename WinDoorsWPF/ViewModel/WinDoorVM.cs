using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinDoorsWPF.Model;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WinDoorsWPF.ViewModel
{
    class WinDoorVM : INotifyPropertyChanged
    {
        public ICommand GetPricesGoogleCom
        {
            get {
                if (GetPricesGoogleCom == null)
                {
                    GetPricesGoogle(); DataToWorksheet();
                }
                return GetPricesGoogleCom; }

        }

        public ICommand GetPricesCom
        {
            get {
                GetPrices();
                return GetPricesCom;
            }
        }

        Person person = new Person();
        PriceList pList = new PriceList();

       public PriceList PList
        {
            get { return pList; }
            set { pList = value; OnPropertyChanged("pList"); }
        }


        public WinDoorVM()
        {
            //GetPricesGoogle();
        }
            
        
        
        public static double GetDouble(string value, double defaultValue)
        {
            double result;

            //Try parsing in the current culture
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                //Then try in US english
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                //Then in neutral language
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
            }

            return result;
        }

        public void GetPricesGoogle()
        {
            PriceList tmpPrice = new PriceList();
            string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
            string ApplicationName = "WindowsDoors";
            UserCredential credential;

            using (var stream =
                new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1St3ncTv58_rLWLWtT8LnOQyu1ddAPTW9BoghcuwgDBM";
            String range = "1!A1:C38";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            var values = response.Values;

           // DataTable dt = new DataTable("Лист1");
        //    DataRow dr = null;

            if (values != null && values.Count > 0)
            {
                // Console.WriteLine("Name, Major");
                int i = 0;
                foreach (var row in values)
                {
                    Material tmp = new Material();
                    //  if (i > 0) priceList.Materials = priceList.Materials.Add(tmp);
                    for (int j = 0; j < row.Count; j++)
                        if (i == 0)
                            break;
                        else
                        if (values[j] != null)
                        {
                            switch (j)
                            {
                                case 0:
                                    { tmp.Name = row[j].ToString(); break; }
                                case 1:
                                    { tmp.Metr = row[j].ToString(); break; }
                                case 2:
                                    { tmp.Price = GetDouble(row[j].ToString(), -1) ; break; }
                            }
                            tmpPrice.Materials.Add(tmp);
                        }
                    i++;

                }
            }

            PList = tmpPrice;
         
        }
        public void GetPrices()
        {
            FileInfo newFile = new FileInfo("price.xlsx");
            ExcelPackage package = new ExcelPackage(newFile);
            ExcelWorksheet osheet = package.Workbook.Worksheets[1];
            // Materials = WorksheetToDataTable(osheet);
            DataTable tmpMatDT = WorksheetToDataTable(osheet);
            PriceList tmpPrice = new PriceList();
            foreach(DataRow values in tmpMatDT.Rows)
            {
                Material tmp = new Material();
                for (int j = 0; j < 3; j++)
                 //   if (values[j] != null)
                    {
                        switch (j)
                        {
                            case 0:
                                { tmp.Name = values[j].ToString(); break; }
                            case 1:
                                { tmp.Metr = values[j].ToString(); break; }
                            case 2:
                                { tmp.Price = GetDouble(values[j].ToString(), -1); break; }
                        }
                        tmpPrice.Materials.Add(tmp);
                    }
            }
            pList = tmpPrice;
        }

        private DataTable WorksheetToDataTable(ExcelWorksheet oSheet)
        {
            int totalRows = oSheet.Dimension.End.Row;
            int totalCols = oSheet.Dimension.End.Column;
            DataTable dt = new DataTable(oSheet.Name);
            DataRow dr = null;
            for (int i = 1; i <= totalRows; i++)
            {
                if (i > 1) dr = dt.Rows.Add();
                for (int j = 1; j <= totalCols; j++)
                {
                    if (i == 1)
                        dt.Columns.Add(oSheet.Cells[i, j].Value.ToString());
                    else
                        if (oSheet.Cells[i, j].Value != null)
                        dr[j - 1] = oSheet.Cells[i, j].Value.ToString();
                }
            }
            return dt;
        }

        public void DataToWorksheet()
        {
            FileInfo newFile = new FileInfo("price.xlsx");
            //  newFile.Delete();
            using (ExcelPackage pck = new ExcelPackage(newFile))
            {

                ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                DataTable pG = ConvertToDataTable(pList.Materials);

                ws.Cells["A1"].LoadFromDataTable(pG, true);


                ws.Cells.Style.Font.Size = 12; // Размер шрифта по умолчанию для всего листа
                ws.Cells.Style.Font.Name = "Times New Roman"; // Default Имя шрифта для всего листа

                using (var cells = ws.Cells[ws.Dimension.Address])
                {
                    cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    cells.AutoFitColumns();
                }

                pck.Save();

            }
        }

        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
