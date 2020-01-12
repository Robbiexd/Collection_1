using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_1.Model
{
    /// <summary>
    /// Class represents the student.
    /// Student has name, goes in school class, was born on date and has gender.
    /// </summary>
    class Student
    {
        //atributes of student
        private string name;
        private string schoolClass;
        private BornDate bornDate;
        private bool gender; // true - man, false - woman

        //properties for outside getting
        public string Name { get { return name; } private set { name = value; } }
        public string SchoolClass { get { return schoolClass; } private set { schoolClass = value; } }
        public BornDate BornDate { get { return bornDate; } private set { bornDate = value; } }
        public bool Gender { get { return gender; } private set { gender = value; } }

        //constructor
        public Student(string name, string schoolClass, BornDate bornDate, bool gender)
        {
            Name = name;
            SchoolClass = schoolClass;
            BornDate = bornDate;
            Gender = gender;
        }

        /// <summary>
        /// Returns data of student in string format and endline.
        /// </summary>
        /// <returns>name schoolClass bornDate gender</returns>
        public override string ToString()
        {
            return String.Format("{0};{1};{2};{3}", name, schoolClass, bornDate.ToString(), (gender ? "muž" : "žena"));
        }
    }

    /// <summary>
    /// Struct represent a born date of Student.
    /// </summary>
    struct BornDate
    {
        private int day;
        private int month;
        private int year;

        public int Day { get { return day; } private set { day = value; } }
        public int Month { get { return month; } private set { month = value; } }
        public int Year { get { return year; } private set { year = value; } }

        //constructors:
        public BornDate(int d, int m, int y)
        {
            day = d;
            month = m;
            year = y;
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}.{2}", day.ToString(), month.ToString(), year.ToString());
        }
    }
}
