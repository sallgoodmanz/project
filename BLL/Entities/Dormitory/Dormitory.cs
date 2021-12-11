using BLL.RegEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Dormitory : IAddable, IRemovable
    {
        #region data
        private string name;
        private int capacityOfDormitory;
        public List<Student> studentDorm;
        #endregion
        #region properties
        public string Name
        {
            get { return name; }
            set
            {
                if (MyRegEx.DormName.IsMatch(value))
                {
                    name = value;
                }
                else throw new MyRegException("DormName");
            }
        }
        public int CapacityOfDormitory
        {
            get { return capacityOfDormitory; }
            set
            {
                if (MyRegEx.DormCapacity.IsMatch(value.ToString()))
                {
                    capacityOfDormitory = value;
                }
                else throw new MyRegException("DormName");
            }
        }
        public int NumberOfStudents { get; set; }
        #endregion

        public Dormitory() { }
        public Dormitory(string groupName, int capacity)
        {
            Name = groupName;
            CapacityOfDormitory = capacity;
            studentDorm = new List<Student>(CapacityOfDormitory);
            NumberOfStudents = 0;
        }

        public void AddStudentToTheGroup(Student student)
        {
            if (studentDorm.Count == CapacityOfDormitory) { throw new Exception("Гуртожиток заповнен! Створіть новий або звільніть місце."); }
            studentDorm.Add(student);
            student.DormName = this.Name;
            NumberOfStudents++;
        }
        public void RemoveStudent(Student student)
        {
            studentDorm.Remove(student);
            student.DormName = "Без гуртожитка";
            NumberOfStudents--;
        }
        public void TransferToAnotherDorm(Dormitory newDorm, Student student)
        {
            newDorm.AddStudentToTheGroup(student);
            studentDorm.Remove(student);
            this.NumberOfStudents--;
            student.DormName = newDorm.Name;
        }
    }
}
