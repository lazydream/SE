﻿using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SE
{
    static class StatisticsDownloader
    {

        public static List<StatisticEvent> GetStatisticsEvent()
        {
            WebClient webClient = new WebClient();
            var statisticEventsString = webClient.DownloadString("http://api.lod-misis.ru/testassignment");

            if (statisticEventsString.Trim('"') == "")
            {
                Console.WriteLine("Nothing happened");
                return null;
            }
            var statisticsEventArray = statisticEventsString.Trim('"').Split(';');

            List<StatisticEvent> listOfEvents = new List<StatisticEvent>();
            foreach (var statEvent in statisticsEventArray)
            {
                string[] genderAndCondition = statEvent.Split(':');
                if (genderAndCondition.Length == 2)
                {
                    StatisticEvent statisticsEvent = new StatisticEvent { Gender = genderAndCondition[0], Condition = genderAndCondition[1] };
                    listOfEvents.Add(statisticsEvent);
                };
                Console.WriteLine(statEvent);
            }
            return listOfEvents;
        }
    }
}
