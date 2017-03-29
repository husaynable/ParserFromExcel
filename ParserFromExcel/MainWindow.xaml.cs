using ExcelParsingApp;
using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ParserFromExcel.Models;

namespace ParserFromExcel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ExcelConverter converter;
        private DbWorker dbWorker;

        public MainWindow()
        {
            InitializeComponent();

            converter = new ExcelConverter();
            dbWorker = new DbWorker();

            converter.ErrorEvent += Converter_ErrorEvent;
        }

        private void Converter_ErrorEvent(object sender, ExcelParsingApp.ErrorEventArgs e)
        {
            AddMessageToLog($"Failed to load {e.QuestionNumber}'th question from '{System.IO.Path.GetFileName(e.Filepath)}' file.");
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Multiselect = true;
            openDialog.Filter = "Excel Files (*.XLS;*.XLSX)|*.XLS;*.XLSX";
            
            if (openDialog.ShowDialog() == true)
            {
                AddMessageToLog("Convertation is started...");
                await ConvertDataFromExcel(openDialog.FileNames.ToList());
                AddMessageToLog("Convertation is finished.");
            }
        }

        private void AddMessageToLog(string v)
        {
            MainLog.AppendText($"{DateTime.Now.ToShortTimeString()}: {v}\n");
        }

        private async Task ConvertDataFromExcel(IEnumerable<string> filepathes)
        {
            List<QuestionTheme> questionThemes = new List<QuestionTheme>();

            foreach(var filepath in filepathes)
            {
                var convertedData = converter.GetDataModel(filepath);
                if(convertedData!=null)
                    questionThemes.Add(convertedData);
            }
            
            await dbWorker.AddQuestionThemeRangeToDb(questionThemes);
        }
    }
}
