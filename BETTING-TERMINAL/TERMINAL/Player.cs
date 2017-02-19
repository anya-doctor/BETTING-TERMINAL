﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TERMINAL
{
    public class Player
    {
        public Player(string firstName, string lastName, DateTime birtDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birtDate;
            iD = Guid.NewGuid();

        }

        string firstName;
        string lastName;
        DateTime birthDate;
        Guid iD;
        const string wrongValue = "wrong value";
        DateTime wrongBirthdate = DateTime.MinValue;
        
        public string FirstName
        {
            get
            {
                return firstName;
            }

            private set
            {
                firstName = (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value)) ? wrongValue : value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            private set
            {
                lastName = (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value)) ? wrongValue : value;
            }
        }

        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }

            private set
            {
                int age = DateTime.Now.Year - value.Year;
                birthDate = (age < 18) ? wrongBirthdate : value;
            }
        }

        public Guid ID
        {
            get
            {
                return iD;
            }
        }
    }
}
