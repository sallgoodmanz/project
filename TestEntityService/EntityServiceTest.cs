using System;
using Xunit;
using BLL;
using System.Collections.Generic;
using DAL;

namespace TestEntityService
{
    public class EntityServiceTest
    {
        [Fact]
        public void Test_ClearFileData_should_return_true()
        {
            IDataProvider<List<Student>> studentProvider = new XMLProvider<List<Student>>("test1.xml");
            EntityService<List<Student>> es = new EntityService<List<Student>>(studentProvider);
            try
            {
                es.ClearFileData();
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.True(true);
        }
        [Fact]
        public void Test_AddNewData_should_return_true()
        {
            IDataProvider<List<Student>> studentProvider = new XMLProvider<List<Student>>("test.xml");
            EntityService<List<Student>> es = new EntityService<List<Student>>(studentProvider);
            List<Student> students = new List<Student>();
            students.Add(new Student());
            try
            {
                es.AddNewData(students);
            }
            catch (System.Exception)
            {
                throw new System.NotImplementedException();
            }

            Assert.True(true);
        }
    }
}
