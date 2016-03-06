using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE
{
    public class StatisticAnalyzer
    {
        private static int birthsCounter;
        private static int menBirthsCounter;
        private static int deathCounter;
        private static int menDeathCounter;

        public static void AnalyzeRequestEvent(List<StatisticEvent> listOfEvents)
        {
            if (listOfEvents != null)
            {
                foreach (var someEvent in listOfEvents)
                {
                    switch (someEvent.Condition)
                    {
                        case "Born":
                            birthsCounter++;
                            Program.currentPopulation++;
                            if (someEvent.Gender == "Male")
                            {
                                Program.menPopulation++;
                                menBirthsCounter++;
                            }
                            else
                            {
                                Program.womenPopulation++;
                            }
                            break;

                        case "Died":
                            deathCounter++;
                            Program.currentPopulation--;
                            if (someEvent.Gender == "Male")
                            {
                                Program.menPopulation--;
                                menDeathCounter++;
                            }
                            else
                            {
                                Program.womenPopulation--;
                            }
                            break;
                    }
                }
            }
        }
        public void PrintOutResults()
        {
            Console.WriteLine("Частота обращения к серверу: каждые 2 секунды\n" +
                "Частота формирования статистики: каждые 10 секунд\n" +
                "Нажмите любую клавишу для прекращения обращения к серверу:\n");
            Console.WriteLine($"Текущее население города: {Program.currentPopulation}, женщин: {Program.womenPopulation}, мужчин: {Program.menPopulation}");
            Console.WriteLine($"За последние {Program.showAnalyticsInterval/1000} секунд родилось {birthsCounter} человек(а), " +
                $"из них {menBirthsCounter} мальчиков и {birthsCounter-menBirthsCounter} девочек");
            Console.WriteLine($"В то же время в городе умерло {deathCounter} человек(а), " +
                $"из них {menDeathCounter} мужчин(а) и {deathCounter-menDeathCounter} женщин(а)\n");

            birthsCounter = 0;
            menBirthsCounter = 0;
            deathCounter = 0;
            menDeathCounter = 0;
        }
    }
}
