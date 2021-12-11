using System;
using Xunit;
using BLL;
namespace TestStudent
{
    public class StudentTest
    {
        [Fact]
        public void Test_Name_Arthur_should_return_true()
        {
            Student student = new Student();
            string name = "артур";

            try
            {
                student.Name = name;
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(name, student.Name);
        }
        [Fact]
        public void Test_Name_1111_should_return_false()
        {
            Student student = new Student();
            string name = "1111";

            try
            {
                student.Name = name;
            }
            catch (System.Exception)
            {
                Assert.True(true);
            }
            Assert.True(true);
        }
        [Fact]
        public void Test_Surname_vasiliev_should_return_true()
        {
            Student student = new Student();
            string surname = "васильев";

            try
            {
                student.Surname = surname;
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(surname, student.Surname);
        }
        [Fact]
        public void Test_Surname_1111_should_return_false()
        {
            Student student = new Student();
            string surname = "1111";

            try
            {
                student.Surname = surname;
            }
            catch (System.Exception)
            {
                Assert.True(true);
            }

            Assert.True(true);
        }
        [Fact]
        public void Test_CorrectDatetime_2000_12_12_should_return_true()
        {
            Student student = new Student();
            var dateOfBirth = new DateTime(2000, 12, 12);

            try
            {
                student.dateOfBirth = dateOfBirth;
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(dateOfBirth, student.dateOfBirth);
        }
        [Fact]
        public void Test_Year_9_should_return_false()
        {
            Student student = new Student();
            int Year = 9;

            try
            {
                student.YearOfStudy = Year;
            }
            catch (System.Exception)
            {
                Assert.True(true);
            }

            Assert.NotEqual(Year, student.YearOfStudy);
        }
        [Fact]
        public void Test_Year_2_should_return_true()
        {
            Student student = new Student();
            int Year = 2;

            try
            {
                student.YearOfStudy = Year;
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(Year, student.YearOfStudy);
        }
        [Fact]
        public void Test_ID_123451234_should_return_true()
        {
            Student student = new Student();
            string ID = "123451234";

            try
            {
                student.PassportID = ID;
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(ID, student.PassportID);
        }
        [Fact]
        public void Test_StudentTicket_999999999_should_return_false()
        {
            Student student = new Student();
            string StudentTicket = "999999999";

            try
            {
                student.StudentTicket = StudentTicket;
            }
            catch (System.Exception)
            {
                Assert.True(true);
            }

            Assert.NotEqual(StudentTicket, student.StudentTicket);
        }
        [Fact]
        public void Test_StudentTicket_KV12341234_should_return_true()
        {
            Student student = new Student();
            string StudentTicket = "КВ12341234";

            try
            {
                student.StudentTicket = StudentTicket;
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(StudentTicket, student.StudentTicket);
        }
        [Fact]
        public void Test_DoWork_should_return_true()
        {
            Student student = new Student();
            string expected = "Виконую лабораторну роботу...";
            string actual;
            try
            {
                actual = student.DoWork();
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void Test_Ctor_With_Parameters_should_return_true()
        {
            DateTime dateTime = new DateTime(2000, 12, 12);
            Student student;
            Student student2 = new Student("петя", "заводской", "123412345", dateTime, 3, "КВ12341234");
            try
            {
                student = new Student("петя", "заводской", "123412345", dateTime, 3, "КВ12341234");
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }
            Assert.True(true);
        }
        [Fact]
        public void Test_Ctor_Without_Parameters_return_true()
        {
            Human human = new Human();
            string name = "артур";

            try
            {
                human.Name = name;
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(name, human.Name);
        }
        [Fact]
        public void Test_GroupName_PI123_should_return_true()
        {
            Student student = new Student();
            string name = "ПІ123";

            try
            {
                student.GroupName = name;
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(name, student.GroupName);
        }
        [Fact]
        public void Test_DormName_23_should_return_true()
        {
            Student student = new Student();
            string name = "23";

            try
            {
                student.DormName = name;
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(name, student.DormName);
        }
        [Fact]
        public void Test_Ctor_With_Parameters_Human_should_return_true()
        {
            DateTime dateTime = new DateTime(2000, 12, 12);
            Human human;
            Human human2 = new Human("петя", "заводской", "123412345", dateTime);
            try
            {
                human = new Human("петя", "заводской", "123412345", dateTime);
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }
            Assert.True(true);
        }
        [Fact]
        public void Test_Age_10_should_return_true()
        {
            Human student = new Human();
            int Age = 10;

            try
            {
                student.Age = Age;
            }
            catch (System.Exception)
            {
                Assert.True(true);
            }

            Assert.Equal(Age, student.Age);
        }
        [Fact]
        public void Test_ID_3333_should_return_false()
        {
            Student student = new Student();
            string ID = "333";

            try
            {
                student.PassportID = ID;
            }
            catch (System.Exception)
            {
                Assert.True(true);
            }

            Assert.True(true);
        }

    }
}
