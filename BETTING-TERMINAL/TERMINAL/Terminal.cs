﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TERMINAL
{
    public static class Terminal
    {
        static decimal currentBalance;
        static List<Account> AccountsData = new List<Account>();
        static bool TerminalStatus = false;
        static Account SignedAccount = null;

        public static void Registration(Player pl, Currency cur)
        {
            string password;
            do
            {
                password = NewPassword();
            }
            while (AccountsData.Find(x => x.password == password) != null);

            Account newAccount = new Account(pl, cur, password);
            AccountsData.Add(newAccount);
            Console.WriteLine($"New Player Registered, {pl.ToString()}.\nThe password is -----> {password}");
        }

        public static void SignIn(string name, string pass)
        {
            if (TerminalStatus) { Console.WriteLine("You can't sign in");  return; };
            SignedAccount = AccountsData.Find(x => (x.player.FirstName == name) && (x.password == pass));
            if(SignedAccount != null)
            {
                Console.WriteLine($"Player, {SignedAccount.player.ToString()} Signed In");
                TerminalStatus = true;
            }
            else
            {
                Console.WriteLine("Not registered account, or wrong password");
            }
        }

        public static void SignOut()
        {
            TerminalStatus = false;
            if (SignedAccount == null) return;
            Console.WriteLine($"Player, {SignedAccount.player.ToString()} Signed Out");
            SignedAccount = null;
        }



        public static void AddMoney(Money money)
        {
            if (!TerminalStatus) { Console.WriteLine("Sign in to Add Money"); return; };
            if (money.Curency != SignedAccount.currency) { Console.WriteLine($"Error(Can't Add money) Account currency is in {SignedAccount.currency}"); return; };

            Console.WriteLine("Add  money working");
            currentBalance = SignedAccount.Balance + money.Amount;
        }

        public static void Bet(Money money)
        {
            if (!TerminalStatus) { Console.WriteLine("Sign in to Bet"); return; };
            if (money.Curency != SignedAccount.currency) { Console.WriteLine($"Error(Can't Bet) Account currency is in {SignedAccount.currency}"); return; };
            Console.WriteLine("Betting working");
            if(SignedAccount.Balance - money.Amount >= 0)
            {
                Console.WriteLine("Bet");
                currentBalance = SignedAccount.Balance - money.Amount;
            }
            else Console.WriteLine("You have no enough money on your account");            
        }


        private static string NewPassword()
        {
            char[] newpassword = new char[4];
            string hex = "0123456789ABCDEF";
            byte[] data = new byte[newpassword.Length];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(data);
            var seed = BitConverter.ToInt32(data, 0);
            var rnd = new Random(seed);
            for (int i = 0; i < 4; i++)
            {
                newpassword[i] = hex[rnd.Next(0, hex.Length - 1)];
            }
            return new string(newpassword);
        }
    }
}
