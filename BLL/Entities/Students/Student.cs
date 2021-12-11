using BLL.RegEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Student : Human
    {
        #region data
        private int yearOfStudy;
        private string studentTicket;
        private string groupName;
        private string dormName;
        #endregion
        #region properties
        public int YearOfStudy
        {
            get { return yearOfStudy; }
            set
            {
                if (MyRegEx.YearOfStudy.IsMatch(value.ToString()))
                {
                    yearOfStudy = value;
                }
                else throw new MyRegException("YearOfStudy");
            }
        }
        public string StudentTicket
        {
            get { return studentTicket; }
            set
            {
                if (MyRegEx.StudentTicket.IsMatch(value))
                {
                    studentTicket = value;
                }
                else throw new MyRegException("StudentTicket");
            }
        }
        public string GroupName
        {
            get { return groupName; }
            set
            {
                groupName = value;
            }
        }
        public string DormName
        {
            get { return dormName; }
            set
            {
                dormName = value;
            }
        }
        #endregion
        public Student() { }
        public Student(string name, string surname, string PassportID, DateTime dateTime, int yearOfStudy, string studentTicket)
            : base(name, surname, PassportID, dateTime)
        {
            YearOfStudy = yearOfStudy;
            StudentTicket = studentTicket;
            GroupName = "Без групи";
            DormName = "Без гуртожитка";
        }
        public override string DoWork() { return "Виконую лабораторну роботу..."; }
    }
}
