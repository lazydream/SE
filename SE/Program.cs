using System;
using System.Collections.Generic;
using System.Timers;
using System.Configuration;

namespace SoftwareEngineeringProject_1
{
    class Program
    {
        private static Timer requestFromServerTimer;
        private static Timer showAnalyticsTimer;
        private static StatisticAnalyzer analyzer;

        public static int currentPopulation;
        public static int womenPopulation;
        public static int menPopulation;
        public static int showAnalyticsInterval = 10000;

        static void Main(string[] names)
        {
            analyzer = new StatisticAnalyzer();
            GetCurrentPopulation();
            Console.Clear();
            analyzer.PrintOutResults();

            SetTimer();
            Console.ReadKey();
            requestFromServerTimer.Stop();
            requestFromServerTimer.Dispose();
            showAnalyticsTimer.Stop();
            showAnalyticsTimer.Dispose();
            Console.WriteLine("Вызов завершен, нажмите любую клавишу для выхода из программы");
            Console.ReadKey();
        }

        static void GetCurrentPopulation()
        {
            Console.WriteLine("Введите количество жителей города:");
            bool parseResult = int.TryParse(Console.ReadLine(), out currentPopulation);
            Console.WriteLine("Из них женщин: ");
            parseResult = int.TryParse(Console.ReadLine(), out womenPopulation);
            menPopulation = currentPopulation - womenPopulation;
        }
        private static void SetTimer()
        {
            //StatisticAnalyzer analyzer = new StatisticAnalyzer();
            requestFromServerTimer = new Timer(double.Parse(ConfigurationManager.AppSettings["RequestPeriodInSeconds"]));
            showAnalyticsTimer = new Timer(showAnalyticsInterval);

            requestFromServerTimer.Elapsed += (source, elapsedEventArgs) =>
            {
                List<StatisticEvent> eventList = StatisticsDownloader.GetStatisticsEvent();
                StatisticAnalyzer.AnalyzeRequestEvent(eventList);
            };

            showAnalyticsTimer.Elapsed += (source, elapsedEventArgs) =>
            {
                Console.Clear();
                analyzer.PrintOutResults();
            };

            requestFromServerTimer.AutoReset = true;
            requestFromServerTimer.Enabled = true;
            showAnalyticsTimer.AutoReset = true;
            showAnalyticsTimer.Enabled = true;
        }
    }
}
