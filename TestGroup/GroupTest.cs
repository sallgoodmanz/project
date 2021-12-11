using System;
using Xunit;
using BLL;

namespace TestGroup
{
    public class GroupTest
    {
        [Fact]
        public void Test_Name_PI123_should_return_true()
        {
            Group group = new Group();
            string name = "ПЫ125";

            try
            {
                group.Name = name;
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.Equal(name, group.Name);
        }
        [Fact]
        public void Test_Ctor_With_Parameters_should_return_true()
        {
            DateTime dateTime = new DateTime(2000, 12, 12);
            Group group1;
            Group group2 = new Group("ПІ125");
            try
            {
                group1 = new Group("ПІ125");
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
            Group group = new Group("ПІ125");
            DateTime dateTime = new DateTime(2000, 12, 12);
            Student student = new Student("петя", "заводской", "123412345", dateTime, 3, "КВ12341234");
            try
            {
                group.AddStudentToTheGroup(student);
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
            Group group = new Group("ПІ125");
            DateTime dateTime = new DateTime(2000, 12, 12);
            Student student = new Student("петя", "заводской", "123412345", dateTime, 3, "КВ12341234");
            try
            {
                group.RemoveStudent(student);
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
            Group group = new Group("ПІ125");
            Group group2 = new Group("ПІ126");
            DateTime dateTime = new DateTime(2000, 12, 12);
            Student student = new Student("петя", "заводской", "123412345", dateTime, 3, "КВ12341234");
            try
            {
                group.TransferToAnotherGroup(group2, student);
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
                Group group = new Group(name);
            }
            catch (System.Exception)
            {
                Assert.True(true);
            }
            Assert.True(true);
        }
    }
}
