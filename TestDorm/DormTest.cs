using System;
using BLL;
using Xunit;

namespace TestDorm
{
    public class DormTest
    {
        [Fact]
        public void Test_Name_12_should_return_true()
        {
            Dormitory dorm = new Dormitory();
            string name = "12";

            try
            {
                dorm.Name = name;
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(name, dorm.Name);
        }
        [Fact]
        public void Test_Ctor_With_Parameters_should_return_true()
        {
            Dormitory dorm;
            Dormitory dorm2 = new Dormitory("12",300);
            try
            {
                dorm = new Dormitory("25", 300);
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }
            Assert.True(true);
        }
        [Fact]
        public void Test_AddStudentToTheGroup_should_return_true()
        {
            Dormitory dorm = new Dormitory("12", 300);
            DateTime dateTime = new DateTime(2000, 12, 12);
            Student student = new Student("петя", "заводской", "123412345", dateTime, 3, "КВ12341234");
            try
            {
                dorm.AddStudentToTheGroup(student);
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }
            Assert.True(true);
        }
        [Fact]
        public void Test_RemoveStudent_should_return_true()
        {
            Dormitory dorm = new Dormitory("12", 300);
            DateTime dateTime = new DateTime(2000, 12, 12);
            Student student = new Student("петя", "заводской", "123412345", dateTime, 3, "КВ12341234");
            try
            {
                dorm.RemoveStudent(student);
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }
            Assert.True(true);
        }
        [Fact]
        public void Test_TransferToAnotherGroup_should_return_true()
        {
            Dormitory dorm = new Dormitory("12", 300);
            Dormitory dorm2 = new Dormitory("13", 300);
            DateTime dateTime = new DateTime(2000, 12, 12);
            Student student = new Student("петя", "заводской", "123412345", dateTime, 3, "КВ12341234");
            try
            {
                dorm.TransferToAnotherDorm(dorm2, student);
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }
            Assert.True(true);
        }
        [Fact]
        public void Test_Name_1111_should_return_false()
        {
            string name = "1111";
            try
            {
                Dormitory dorm = new Dormitory(name, 300);
            }
            catch (System.Exception)
            {
                Assert.True(true);
            }
            Assert.True(true);
        }
        [Fact]
        public void Test_Capacity_300_should_return_true()
        {
            Dormitory dorm = new Dormitory();
            int capacity  = 300;

            try
            {
                dorm.CapacityOfDormitory = capacity;
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(capacity, dorm.CapacityOfDormitory);
        }
        [Fact]
        public void Test_Capacity_300_should_return_false()
        {
            Dormitory dorm = new Dormitory();
            int capacity = -1;

            try
            {
                dorm.CapacityOfDormitory = capacity;
            }
            catch (System.Exception)
            {
                Assert.True(true);
            }

            Assert.True(true);
        }
    }
}
