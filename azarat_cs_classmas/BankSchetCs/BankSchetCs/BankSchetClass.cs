using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSchetCs
{
    class BankSchetClass
    {
        private uint number;
        private DateTime dateOpen;
        private Fio fio;
        private double balance;
        private int month;

        public uint Number { get { return number; } }
        public DateTime DateOpen { get { return dateOpen; } set { dateOpen = value; } }
        public Fio FIO { get { return fio; } }
        public double Balance { get { return balance; } set { balance = value; } }
        public int Month { get { return month; } set { month = value; } }

        public BankSchetClass(uint num, DateTime date, Fio fioo, double blnc, int mon)
        {
            number = num;
            dateOpen = date;
            fio = fioo;
            balance = blnc;
            month = mon;
        }

        public void ImportSchet(double money)
        {
            balance += money;
        }

        public void ExportSchet (double money)
        {
            if (money < balance)
                balance -= money;
        }

        public void ExportAll ()
        {
            if (balance > 0)
                balance = 0;
        }

        public void Info ()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Номер счета: {number} " +
                $"\nФИО: {fio.sName} {fio.fName} " +
                $"{fio.tName}\nДата открытия:{dateOpen.Date} " +
                $"\nСрок вклада: {month}" +
                $"\nДата закрытия: {InfoClose()}" +
                $"\nБаланс: {balance.ToString("F2")}\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static BankSchetClass operator +(BankSchetClass bsc1, BankSchetClass bsc2)
        {
            if (bsc1 != bsc2)
            {
                bsc1.Balance = bsc1.Balance + bsc2.Balance;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Операция отклонена. Невозможно объеденить счёт с тем же счетом");
                Console.ForegroundColor = ConsoleColor.White;
            }
            return bsc1;
        }

        public static BankSchetClass operator%(BankSchetClass bsc, double proc)
        {
            if (proc <= 30)
                bsc.balance *= Math.Pow((1+proc / 100 /365),bsc.month*30);
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Операция отклонена. На больше, чем 30% годовых мы не согласны");
                Console.ForegroundColor = ConsoleColor.White;
            }
           
            return bsc;
        }

        public static BankSchetClass operator++(BankSchetClass bsc)
        {
            bsc.balance *= Math.Pow((1.0 + 4.7 / 100.0 / 365.0), 30);

            return bsc;
        }

        public static bool operator==(BankSchetClass bsc1, BankSchetClass bsc2)
        {
            if (bsc1.Balance == bsc2.Balance)
                return true;

            return false;
        }

        public static bool operator!=(BankSchetClass bsc1, BankSchetClass bsc2)
        {
            if (bsc1.Balance == bsc2.Balance)
                return false;
                return true;
        }

        public static BankSchetClass operator-(BankSchetClass bsc, double money)
        {
            if (money <= bsc.balance)
                bsc.balance -= money;
            return bsc;
        }

        public static BankSchetClass operator-(BankSchetClass bsc1, BankSchetClass bsc2)
        {
            double money = bsc1.Balance ;
            bsc1.balance -= money;
            bsc2.balance += money;
            return bsc2;
        }

        public static BankSchetClass operator+(BankSchetClass bsc, double money)
        {
            bsc.balance+= money;
            return bsc;
        }


        public void Transfer (BankSchetClass bankSchet, double money)
        {
            if (money <= balance)
            {
                bankSchet.balance += money;
                balance -= money;
            }
        }

        private DateTime InfoClose ()
        {
            DateTime data = dateOpen.AddMonths(month);
            return data.Date;
            
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
