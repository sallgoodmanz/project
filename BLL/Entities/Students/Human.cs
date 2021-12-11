using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.RegEx;

namespace BLL
{
    public class Human : IWorkable
    {
        #region data
        private string name;
        private string surname;
        private string id;
        public DateTime dateOfBirth;
        #endregion
        #region properties
        public string Name
        {
            get { return name; }
            set
            {
                if (MyRegEx.Name.IsMatch(value))
                {
                    name = value;
                }
                else throw new MyRegException("Name");
            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                if (MyRegEx.Surname.IsMatch(value))
                {
                    surname = value;
                }
                else throw new MyRegException("Surname");
            }
        }
        public string PassportID
        {
            get { return id; }
            set
            {
                if (MyRegEx.PassportID.IsMatch(value))
                {
                    id = value;
                }
                else throw new MyRegException("PassportID");
            }
        }
        public int Age { get; set; }
        #endregion
        public Human() { }
        public Human(string name, string surname, string PassportID, DateTime dateTime)
        {
            Name = name;
            Surname = surname;
            this.PassportID = PassportID;
            dateOfBirth = dateTime;
            Age = 2021 - dateOfBirth.Year;
        }
        public virtual string DoWork() { return "Виконую деяку роботу..."; }
    }
}
