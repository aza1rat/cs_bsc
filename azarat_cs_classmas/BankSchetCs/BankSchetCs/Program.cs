using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSchetCs
{

    public struct Fio
    {
        public String sName;
        public String fName;
        public String tName;
    }

    class Program
    {
        public static BankSchetClass[] BankSchets = new BankSchetClass[2];
        public static Random rand = new Random();
        public static Fio FIO;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < BankSchets.Length; i++)
            {
                OpenSchet(ref BankSchets[i]);
            }
            while (true)
            {
                uint selectSchet = ControlUInt("Введите номер счёта " + GetNumbers(BankSchets));
                string choice = "";
                for (int i = 0; i < BankSchets.Length; i++)
                {
                    if (BankSchets[i].Number == selectSchet)
                    {
                       while(choice != "0")
                        {
                            choice = WriteMessage("Выберите опцию:\n1) Вывести информацию о счёте\n2) Вывести деньги со счёта\n3) Вывести все деньги" +
                                "\n4) Пополнить счёт \n5) Перевод\nПерегрузка операция\n\n6) Объединение двух счетов\n7) Начисление процентов в конце всего периода" +
                                "\n8) Начисление процентов в конце месяце\n9) Сравнение счетов\n10) Снятие средств с счета\n11) Пополнение средств\n12) Перевод всех средств");
                            switch (choice)
                            {
                                case "1": BankSchets[i].Info();break;
                                case "2": BankSchets[i].ExportSchet(ControlBalance("Введите сумму для вывода")); break;
                                case "3": BankSchets[i].ExportAll();break;
                                case "4": BankSchets[i].ImportSchet(ControlBalance("Введите сумму для вывода")); break;
                                case "5": TransferSchet(ref BankSchets, BankSchets[i], choice);break;
                                case "6": TransferSchet(ref BankSchets, BankSchets[i], choice); break;
                                case "7": BankSchets[i] = BankSchets[i] % ControlBalance("Введите кол-во процентов (не больше 30)");break;
                                case "8": BankSchets[i]++;break;
                                case "9": TransferSchet(ref BankSchets, BankSchets[i], choice); break;
                                case "10": BankSchets[i] = BankSchets[i] - ControlBalance("Введите кол-во средств для снятия"); break;
                                case "11": BankSchets[i] = BankSchets[i] + ControlBalance("Введите кол-во средств для пополнения"); break;
                                case "12": TransferSchet(ref BankSchets, BankSchets[i], choice); break;
                                case "13": Array.Resize(ref BankSchets, BankSchets.Length + 1); OpenSchet(ref BankSchets[BankSchets.Length - 1]);break;
                            }
                        }
                    }
                }
            }
        }


        static void OpenSchet(ref BankSchetClass bankSchets)
        {
            FIO.sName = WriteMessage("Введите Фамилию");
            FIO.fName = WriteMessage("Введите имя");
            FIO.tName = WriteMessage("Введите отчество");

            bankSchets = new BankSchetClass(GenerateNumber(), GenerateDate(), FIO, ControlBalance("Введите баланс"), ControlInt("Введите кол-во месяцев"));
        }


        static string WriteMessage(string mes)
        {
            
            Console.WriteLine(mes);
            string result = Console.ReadLine();
            return result;
        }

        static void TransferSchet(ref BankSchetClass[] bankSchets, BankSchetClass schet, string choice)
        {
            uint selectSchet = ControlUInt("Введите номер счёта");

            for (int i = 0; i < bankSchets.Length; i++)
            {
                if (selectSchet == bankSchets[i].Number)
                    switch (choice)
                    {
                        case "5": schet.Transfer(bankSchets[i], ControlBalance("Введите сумму для вывода"));break;
                        case "6": schet = bankSchets[i] + schet; break;
                        case "9": if (bankSchets[i] == schet)
                                Console.WriteLine("Баланс выбранных счетов одинаков");
                            if (bankSchets[i] != schet)
                                Console.WriteLine("Баланс выбранных счетов разные");break;
                        case "12": bankSchets[i] = schet - bankSchets[i]; break;

                    }
            }
        }

        static uint ControlUInt(string mess)
        {
            uint rezult; string temp;
            do
            {
                Console.WriteLine(mess);
                temp = Console.ReadLine();
            }
            while (!uint.TryParse(temp, out rezult));
            return (rezult);
        }

        static int ControlInt(string mess)
        {
            int rezult; string temp;
            do
            {
                Console.WriteLine(mess);
                temp = Console.ReadLine();
            }
            while (!int.TryParse(temp, out rezult));
            return (rezult);
        }

        static double ControlBalance(string mess)
        {
            double rezult; string temp;
            do
            {
                do
                {
                    Console.WriteLine(mess);
                    temp = Console.ReadLine();
                }
                while (!double.TryParse(temp, out rezult));
            }
            while (rezult < 0);
            rezult = Math.Round(rezult, 2);
            return (rezult);
        }

        static DateTime GenerateDate()
        {
            int minday, maxday, days;
            if (DateTime.Now.Day > 5)
            {
                minday = DateTime.Now.Day - 5;
                maxday = DateTime.Now.Day;
            }
            else
            {
                minday = DateTime.Now.Day;
                maxday = DateTime.Now.Day + 5;
            }
            days = rand.Next(minday, maxday);
            DateTime dat = new DateTime(DateTime.Now.Year, DateTime.Now.Month, days);
            return dat;

        }

        static string GetNumbers(BankSchetClass[] bankSchets)
        {
            string result = "";
            for (int i = 0; i < bankSchets.Length; i++)
            {
                result += bankSchets[i].Number.ToString() + " ";
            }
            return result;
        }

        private static uint GenerateNumber()
        {
            Random ran = new Random();
            uint key; string genKey = "";
            for (int j = 0; j < 3; j++)
            {
                genKey += ran.Next(0, 10).ToString();
            }
            key = Convert.ToUInt32(genKey);

            return key;
        }
    }
}
