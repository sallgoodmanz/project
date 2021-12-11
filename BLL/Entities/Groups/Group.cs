using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.RegEx;

namespace BLL
{
    public class Group : IAddable, IRemovable, ITransferable
    {
        #region data
        private string name;
        private const int CAPACITY_OF_A_GROUP = 30;
        public List<Student> studentGroup;
        #endregion

        #region properties
        public string Name
        {
            get { return name; }
            set
            {
                if (MyRegEx.GroupName.IsMatch(value))
                {
                    name = value;
                }
                else throw new MyRegException("GroupName");
            }
        }
        public int NumberOfStudents { get; set; }
        #endregion

        public Group() { }
        public Group(string groupName)
        {
            Name = groupName;
            studentGroup = new List<Student>(CAPACITY_OF_A_GROUP);
            NumberOfStudents = 0;
        }

        public void AddStudentToTheGroup(Student student)
        {
            if (studentGroup.Count == CAPACITY_OF_A_GROUP) { throw new Exception("Група заповнена! Створіть нову або звільніть місце."); }
            studentGroup.Add(student);
            student.GroupName = this.Name;
            NumberOfStudents++;
        }
        public void RemoveStudent(Student student)
        {
            studentGroup.Remove(student);
            student.GroupName = "Без групи";
            NumberOfStudents--;
        }
        public void TransferToAnotherGroup(Group newGroup, Student student)
        {
            newGroup.AddStudentToTheGroup(student);
            studentGroup.Remove(student);
            this.NumberOfStudents--;
            student.GroupName = newGroup.Name;
        }
    }
}
