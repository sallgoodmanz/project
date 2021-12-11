using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.RegEx;
using DAL;

namespace PL
{

    public static class ConsoleMenu
    {
        #region provider and services
        private static IDataProvider<List<Student>> studentDataProvider;
        private static IEntityService<List<Student>> studentDataService;

        private static IDataProvider<List<Group>> groupDataProvider;
        private static IEntityService<List<Group>> groupDataService;

        private static IDataProvider<List<Dormitory>> dormDataProvider;
        private static IEntityService<List<Dormitory>> dormDataService;

        static List<Student> AllStudents = new List<Student>();
        static List<Group> AllGroups = new List<Group>();
        static List<Dormitory> AllDorms = new List<Dormitory>();
        #endregion

        #region service functions
        private static string SetFileName()
        {
            Console.Clear();
            Console.WriteLine("Введіть назву файлу:"); string inputPath = Console.ReadLine();
            return inputPath + ".xml";
        }
        private static bool HasSimilarTicket(string studentTicket)
        {
            foreach (var student in AllStudents)
            {
                if (studentTicket == student.StudentTicket)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool HasSimilarID(string ID)
        {
            foreach (var student in AllStudents)
            {
                if (ID == student.PassportID)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool HasSimilarGroupName(string groupName)
        {
            foreach (var group in AllGroups)
            {
                if (groupName == group.Name)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool HasSimilarDormName(string dormName)
        {
            foreach (var dorm in AllDorms)
            {
                if (dormName == dorm.Name)
                {
                    return true;
                }
            }
            return false;
        }
        private static void SaveDataToDB()
        {
            Console.Clear();
            string choice = "N";
            Console.WriteLine("Усі дані в базі данх будуть перезаписані поточними!");
            Console.WriteLine("\nВи бажаєте продовжити?\nY - yes, N  - no");
            choice = Console.ReadLine();
            choice = choice.ToUpper();
            if (choice != "Y" && choice != "N") throw new Exception("Перевірте коректність вводу даних!");

            if (choice == "Y")
            {
                studentDataProvider = new XMLProvider<List<Student>>("students.xml");
                EntityService<List<Student>> entityServiceS = new EntityService<List<Student>>(studentDataProvider);
                entityServiceS.ClearFileData();
                entityServiceS.SerializeData(AllStudents);

                groupDataProvider = new XMLProvider<List<Group>>("groups.xml");
                EntityService<List<Group>> entityServiceG = new EntityService<List<Group>>(groupDataProvider);
                entityServiceG.ClearFileData();
                entityServiceG.SerializeData(AllGroups);

                dormDataProvider = new XMLProvider<List<Dormitory>>("dorms.xml");
                EntityService<List<Dormitory>> entityServiceD = new EntityService<List<Dormitory>>(dormDataProvider);
                entityServiceD.ClearFileData();
                entityServiceD.SerializeData(AllDorms);

                Console.Clear();
                Console.WriteLine("Дані було успішно збережено!"); Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Повретаємось до MAIN MENU..."); Console.ReadKey();
                MainMenu();
            }
           
        }
        private static void GetDataFromDB()
        {
            Console.Clear();
            studentDataProvider = new XMLProvider<List<Student>>("students.xml");
            studentDataService = new EntityService<List<Student>>(studentDataProvider);
            Console.WriteLine("Завантажуємо студентів з \'students.xml\'..."); Console.ReadKey();
            AllStudents = studentDataService.GetData();

            groupDataProvider = new XMLProvider<List<Group>>("groups.xml");
            Console.WriteLine("Завантажуємо групи з \'groups.xml\'..."); Console.ReadKey();
            groupDataService = new EntityService<List<Group>>(groupDataProvider);
            AllGroups = groupDataService.GetData();

            dormDataProvider = new XMLProvider<List<Dormitory>>("dorms.xml");
            Console.WriteLine("Завантажуємо дані про гуртожитки з \'dorms.xml\'..."); Console.ReadKey();
            dormDataService = new EntityService<List<Dormitory>>(dormDataProvider);
            AllDorms = dormDataService.GetData();

            Console.Clear();
            Console.WriteLine("Дані було успішно завантажено!"); Console.ReadKey();

        }
        private static void SearchForStudentsByTheirName()
        {
            Console.Clear();
            Console.Write("Ім'я: "); string inputName = Console.ReadLine();
            if (!MyRegEx.Name.IsMatch(inputName)) throw new MyRegException("Ім'я");

            Console.Write("Прізвище: "); string inputSurname = Console.ReadLine();
            if (!MyRegEx.Surname.IsMatch(inputSurname)) throw new MyRegException("Прізвище");
            Console.Clear();
            bool foundStudents = false;
            foreach (var student in AllStudents)
            {
                if (student.Name == inputName && student.Surname == inputSurname)
                {
                    OutputStudent(student);
                    foundStudents = true;
                }
            }
            if (!foundStudents) { throw new Exception("Не знайдено жодного студента за заданими даними!"); }
            Console.ReadKey();
        }
        private static void SearchForStudentsByTheirGroup()
        {
            Console.Clear();
            Console.Write("Назва групи: "); string inputGroupName = Console.ReadLine();
            if (!MyRegEx.GroupName.IsMatch(inputGroupName)) throw new MyRegException("Назва групи");
            Console.Clear();
            bool foundStudents = false;
            foreach (var student in AllStudents)
            {
                if (student.GroupName == inputGroupName)
                {
                    OutputStudent(student);
                    foundStudents = true;
                }
            }
            if (!foundStudents) { throw new Exception("Не знайдено жодного студента за заданими даними!"); }
            Console.ReadKey();
        }
        private static void SearchForStudentsByTheirDorm()
        {
            Console.Clear();
            Console.Write("Назва гуртожитка: "); string inputDormName = Console.ReadLine();
            if (!MyRegEx.DormName.IsMatch(inputDormName)) throw new MyRegException("Назва гуртожитка");
            Console.Clear();
            bool foundStudents = false;
            foreach (var student in AllStudents)
            {
                if (student.DormName == inputDormName)
                {
                    OutputStudent(student);
                    foundStudents = true;
                }
            }
            if (!foundStudents) { throw new Exception("Не знайдено жодного студента за заданими даними!"); }
            Console.ReadKey();
        }

        #endregion

        #region student service methods
        private static void OutputStudent(Student student)
        {
            Console.WriteLine("\nІм'я: " + student.Name);
            Console.WriteLine("Прізвище: " + student.Surname);
            Console.WriteLine("Вік: " + student.Age);
            Console.WriteLine("Дата народження: " + student.dateOfBirth.ToShortDateString());
            Console.WriteLine("ID: " + student.PassportID);
            Console.WriteLine("Курс: " + student.YearOfStudy);
            Console.WriteLine("Студентський квиток: " + student.StudentTicket);
            Console.WriteLine("Група: " + student.GroupName);
            Console.WriteLine("Номер гуртожитка: " + student.DormName);
        }
        private static Student AddStudent()
        {
            Console.Write("Ім'я: "); string inputName = Console.ReadLine();
            if (!MyRegEx.Name.IsMatch(inputName)) throw new MyRegException("Ім'я");

            Console.Write("Прізвище: "); string inputSurname = Console.ReadLine();
            if (!MyRegEx.Surname.IsMatch(inputSurname)) throw new MyRegException("Прізвище");

            Console.WriteLine("Дата народження у форматі ----/--/--");
            Console.Write("Рік: "); string _DATEyear = Console.ReadLine(); int DATEyear = int.Parse(_DATEyear);
            Console.Write("Місяць: "); string _DATEmonth = Console.ReadLine(); int DATEmonth = int.Parse(_DATEmonth);
            Console.Write("Дата: "); string _DATEday = Console.ReadLine(); int DATEday = int.Parse(_DATEday);
            DateTime dateTime = new DateTime(DATEyear, DATEmonth, DATEday);

            Console.Write("ID: "); string inputID = Console.ReadLine();
            if (!MyRegEx.PassportID.IsMatch(inputID)) throw new MyRegException("ID");
            if (HasSimilarID(inputID)) { throw new Exception("Заданий ID вже присутній у іншого студента!"); }

            Console.Write("Курс: "); string _inputYear = Console.ReadLine(); int inputYear = int.Parse(_inputYear);
            if (!MyRegEx.YearOfStudy.IsMatch(inputYear.ToString())) throw new MyRegException("Курс");

            Console.Write("Номер квитка: "); string inputTicket = Console.ReadLine();
            if (!MyRegEx.StudentTicket.IsMatch(inputTicket)) throw new MyRegException("Номер квитка");
            if (HasSimilarTicket(inputTicket)) { throw new Exception("Заданий номер квитка вже присутній у іншого студента!"); }

            return new Student(inputName, inputSurname, inputID, dateTime, inputYear, inputTicket);
        }
        private static Student GetStudentByID(string ID)
        {
            if (!MyRegEx.PassportID.IsMatch(ID)) { throw new MyRegException("ID"); }
            foreach (var student in AllStudents)
            {
                if (student.PassportID == ID)
                {
                    return student;
                }
            }
            throw new Exception("Не знайдено жодного студента за заданим ID!");
        }

        private static void DeleteStudent()
        {
            Console.Write("ID: "); string inputID = Console.ReadLine();
            if (!MyRegEx.PassportID.IsMatch(inputID)) throw new MyRegException("ID");

            for (int i = 0; i < AllStudents.Count; i++)
            {
                if (inputID == AllStudents[i].PassportID)
                {
                    AllStudents.Remove(GetStudentByID(inputID));
                    return;
                }
            }
            throw new Exception("Не знайдено жодного студента за заданим ID!");
        }
        private static void OutputAllStudents()
        {
            if (AllStudents.Count == 0) { throw new Exception("БД студентів пуста!"); }

            foreach (var item in AllStudents)
            {
                OutputStudent(item);
            }
            Console.ReadKey();
            Console.Clear();
        }
        private static void GetStudentDataByID()
        {
            Console.Clear();
            Console.Write("Введіть ID студента, дані якого хочете отримати: "); string inputID = Console.ReadLine();
            if (!MyRegEx.PassportID.IsMatch(inputID)) throw new MyRegException("ID");

            var student = GetStudentByID(inputID);
            OutputStudent(student);
            Console.ReadKey();
        }
        private static void ChangeStudentData()
        {
            Console.Clear(); 
            Console.Write("Введіть ID студента, дані якого хочете змінити: "); string inputID = Console.ReadLine();
            if (!MyRegEx.PassportID.IsMatch(inputID)) throw new MyRegException("ID");

            var oldStudent = GetStudentByID(inputID);
            int index = AllStudents.IndexOf(oldStudent);
            Console.WriteLine("Введіть нові дані:");
            Console.Write("Ім'я: "); string inputName = Console.ReadLine();
            if (!MyRegEx.Name.IsMatch(inputName)) throw new MyRegException("Ім'я");

            Console.Write("Прізвище: "); string inputSurname = Console.ReadLine();
            if (!MyRegEx.Surname.IsMatch(inputSurname)) throw new MyRegException("Прізвище");

            Console.WriteLine("Дата народження у форматі ----/--/--");
            Console.Write("Рік: "); string _DATEyear = Console.ReadLine(); int DATEyear = int.Parse(_DATEyear);
            Console.Write("Місяць: "); string _DATEmonth = Console.ReadLine(); int DATEmonth = int.Parse(_DATEmonth);
            Console.Write("Дата: "); string _DATEday = Console.ReadLine(); int DATEday = int.Parse(_DATEday);
            DateTime dateTime = new DateTime(DATEyear, DATEmonth, DATEday);

            Console.Write("Курс: "); string _inputYear = Console.ReadLine(); int inputYear = int.Parse(_inputYear);
            if (!MyRegEx.YearOfStudy.IsMatch(inputYear.ToString())) throw new MyRegException("Курс");

            Console.Write("Номер квитка: "); string inputTicket = Console.ReadLine();
            if (!MyRegEx.StudentTicket.IsMatch(inputTicket)) throw new MyRegException("Номер квитка");
            if (HasSimilarTicket(inputTicket)) { throw new Exception("Заданий номер квитка вже присутній у іншого студента!"); }

            var newStudent = new Student(inputName, inputSurname, inputID, dateTime, inputYear, inputTicket);
            newStudent.GroupName = oldStudent.GroupName;
            AllStudents[index] = newStudent;
           
            foreach (var group in AllGroups)
            {
                foreach (var student in group.studentGroup)
                {
                    if (student.PassportID == inputID)
                    {
                        int groupIndex = group.studentGroup.IndexOf(student);
                        group.studentGroup[groupIndex] = newStudent;
                    }
                }
            }
        }
      
        #endregion

        #region group service methods
        private static void OutputGroup(Group group)
        {
            Console.Write("\nНазва групи: " + group.Name);
            Console.WriteLine("\nКількість студентів у групі: " + group.NumberOfStudents);
        }
        private static void OutputGroupInfo()
        {
            Console.Clear();
            Console.Write("Введіть назву групи: "); string groupName = Console.ReadLine();
            var group = GetGroupByName(groupName);
            OutputGroupInfo(group);
            Console.ReadLine();
        }
        private static void OutputGroupInfo(Group group)
        {
            if (group.studentGroup.Count != 0)
            {
                foreach (var student in group.studentGroup)
                {
                    OutputStudent(student);
                }
            }
            else { throw new Exception("Задана група пуста!"); }
        }

        private static Group AddNewGroup()
        {
            Console.Clear();
            Console.Write("Назва групи: "); string inputName = Console.ReadLine();
            if (!MyRegEx.GroupName.IsMatch(inputName)) throw new MyRegException("Назва групи");

            return new Group(inputName);
        }
        private static Group GetGroupByName(string groupName)
        {
            if (!MyRegEx.GroupName.IsMatch(groupName)) throw new MyRegException("Назва групи");
            foreach (var group in AllGroups)
            {
                if (group.Name == groupName)
                {
                    return group;
                }
            }
            throw new Exception("Не знайдено жодної групи за заданою назвою!");
        }
        private static void SetStudentGroup()
        {
            Console.Clear();
            Console.Write("Введіть ID студента якого хочете додати: "); string passportID = Console.ReadLine();
            if (!(GetStudentByID(passportID).GroupName == "Без групи")) { throw new Exception("Студент вже знаходиться в групі!"); }
            Console.Write("Введіть назву групи до якої хочете перевести студента: "); string newGroupName = Console.ReadLine();
            var newGroup = GetGroupByName(newGroupName);
            newGroup.AddStudentToTheGroup(GetStudentByID(passportID));
        }
        private static void DeleteStudentFromTheGroup()
        {
            Console.Clear();
            Console.Write("Введіть назву групи з якої хочете видалити студента: "); string currGroupName = Console.ReadLine();
            var currGroup = GetGroupByName(currGroupName);
            Console.WriteLine("---------------------------");
            OutputGroupInfo(currGroup);
            Console.WriteLine("---------------------------");
            Console.Write("Введіть ID студента якого хочете видалити з групи: "); string passportID = Console.ReadLine();
            if (!(GetStudentByID(passportID).GroupName == currGroup.Name)) { throw new Exception("Студент знаходиться в іншій групі!"); }
            currGroup.RemoveStudent(GetStudentByID(passportID));
        }
        private static void TransferStudent()
        {
            Console.Clear();
            Console.Write("Введіть назву групи з якої хочете перевести студента: "); string oldGroupName = Console.ReadLine();
            var oldGroup = GetGroupByName(oldGroupName);
            Console.Write("Введіть ID студента якого хочете перевести: "); string passportID = Console.ReadLine();
            var student = GetStudentByID(passportID);
            Console.Write("Введіть назву групи до якої хочете перевести студента: "); string newGroupName = Console.ReadLine();
            var newGroup = GetGroupByName(newGroupName);
            oldGroup.TransferToAnotherGroup(newGroup, student);
        }
        private static void DeleteGroup()
        {
            Console.Clear();
            Console.Write("Введіть назву групи, яку хочете видалити: "); string currGroupName = Console.ReadLine();
            var currGroup = GetGroupByName(currGroupName);
            AllGroups.Remove(currGroup);
            foreach (var student in AllStudents)
            {
                if (student.GroupName == currGroupName)
                {
                    student.GroupName = "Без групи";
                }
            }
        }
        private static void DeleteALLGroups()
        {
            AllGroups.Clear();
            foreach (var student in AllStudents)
            {
                student.GroupName = "Без групи";
            }
        }
        private static void ChangeGroupName()
        {
            Console.Clear();
            Console.Write("Введіть назву групи, назву якої хочете змінити: "); string currGroupName = Console.ReadLine();
            var currGroup = GetGroupByName(currGroupName);
            Console.Write("Введіть нову назву групи, назву якої хочете змінити: "); string newGroupName = Console.ReadLine();
            if (!HasSimilarGroupName(newGroupName))
            {
                currGroup.Name = newGroupName;
                foreach (var student in AllStudents)
                {
                    if (student.GroupName == currGroupName)
                    {
                        student.GroupName = newGroupName;
                    }
                }
                foreach (var group in AllGroups)
                {
                    foreach (var student in group.studentGroup)
                    {
                        if (student.GroupName == currGroupName)
                        {
                            student.GroupName = newGroupName;
                        }
                    }
                }
                foreach (var dorm in AllDorms)
                {
                    foreach (var student in dorm.studentDorm)
                    {
                        if (student.GroupName == currGroupName)
                        {
                            student.GroupName = newGroupName;
                        }
                    }
                }
            }
        }
        #endregion

        #region dorm service methods
        private static void OutputDorm(Dormitory dorm)
        {
            Console.Write("\nНомер гуртожитка: " + dorm.Name);
            Console.WriteLine("\nКількість заселених місць : " + dorm.NumberOfStudents);
            Console.WriteLine("Кількість вільних місць: " + (dorm.CapacityOfDormitory - dorm.NumberOfStudents));
        }
        private static void OutputDormInfo()
        {
            Console.Clear();
            Console.Write("Введіть назву групи: "); string dormName = Console.ReadLine();
            var dorm = GetDormByName(dormName);
            OutputDormInfo(dorm);
        }
        private static void OutputDormInfo(Dormitory dorm)
        {
            if (dorm.studentDorm.Count != 0)
            {
                foreach (var student in dorm.studentDorm)
                {
                    OutputStudent(student);
                }
            }
            else { throw new Exception("Заданий гуртожиток пустує!"); }
            Console.ReadLine();
        }
        private static Dormitory AddNewDorm()
        {
            Console.Clear();
            Console.Write("Назва гуртожитка: "); string inputName = Console.ReadLine();
            if (!MyRegEx.DormName.IsMatch(inputName)) throw new MyRegException("Назва гуртожитка");

            Console.Write("Кількість місць у гуртожитку: "); string _inputCapacity = Console.ReadLine(); int inputCapacity = int.Parse(_inputCapacity);

            if (!MyRegEx.DormCapacity.IsMatch(inputCapacity.ToString())) throw new MyRegException("Кількість місць");

            return new Dormitory(inputName, inputCapacity);
        }
        private static Dormitory GetDormByName(string dormName)
        {
            if (!MyRegEx.DormName.IsMatch(dormName)) throw new MyRegException("Назва гуртожитка");
            foreach (var dorm in AllDorms)
            {
                if (dorm.Name == dormName)
                {
                    return dorm;
                }
            }
            throw new Exception("Не знайдено жодного гуртожитка за заданою назвою!");
        }
        private static void SetStudentDorm()
        {
            Console.Clear();
            Console.Write("Введіть ID студента якого хочете додати: "); string passportID = Console.ReadLine();
            if (!(GetStudentByID(passportID).DormName == "Без гуртожитка")) { throw new Exception("Студент вже знаходиться в гуртожитку!"); }
            Console.Write("Введіть назву гуртожитка до якого хочете переселити студента: "); string newDormName = Console.ReadLine();
            var newDorm = GetDormByName(newDormName);
            newDorm.AddStudentToTheGroup(GetStudentByID(passportID));
        }
        private static void DeleteStudentFromTheDorm()
        {
            Console.Clear();
            Console.Write("Введіть назву гуртожитка з якого хочете виселити студента: "); string currDormName = Console.ReadLine();
            var currDorm = GetDormByName(currDormName);
            Console.WriteLine("---------------------------");
            OutputDormInfo(currDorm);
            Console.WriteLine("---------------------------");
            Console.Write("Введіть ID студента якого хочете виселити з гуртожитка: "); string passportID = Console.ReadLine();
            if (!(GetStudentByID(passportID).DormName == currDorm.Name)) { throw new Exception("Студент знаходиться в іншому гуртожитку!"); }
            currDorm.RemoveStudent(GetStudentByID(passportID));
        }
        private static void TransferStudentToAnotherDorm()
        {
            Console.Clear();
            Console.Write("Введіть назву гуртожитку з якого хочете перевести студента: "); string oldDormName = Console.ReadLine();
            var oldDorm = GetDormByName(oldDormName);
            Console.Write("Введіть ID студента якого хочете перевести: "); string passportID = Console.ReadLine();
            var student = GetStudentByID(passportID);
            Console.Write("Введіть назву гуртожитка до якого хочете перевести студента: "); string newDormName = Console.ReadLine();
            var newDorm = GetDormByName(newDormName);
            oldDorm.TransferToAnotherDorm(newDorm, student);
        }
        private static void DeleteDorm()
        {
            Console.Clear();
            Console.Write("Введіть назву гуртожитка, який хочете видалити: "); string currGroupName = Console.ReadLine();
            var currDorm = GetDormByName(currGroupName);
            AllDorms.Remove(currDorm);
            foreach (var student in AllStudents)
            {
                if (student.DormName == currGroupName)
                {
                    student.DormName = "Без гуртожитка";
                }
            }
        }
        private static void DeleteALLDorms()
        {
            AllDorms.Clear();
            foreach (var student in AllStudents)
            {
                student.DormName = "Без гуртожитка";
            }
        }
        private static void ChangeDormName()
        {
            Console.Clear();
            Console.Write("Введіть назву гуртожитка, назву якого хочете змінити: "); string currDormName = Console.ReadLine();
            var currDorm = GetDormByName(currDormName);
            Console.Write("Введіть нову назву гуртожитка: "); string newDormName = Console.ReadLine();
            if (!HasSimilarDormName(newDormName))
            {
                currDorm.Name = newDormName;
                foreach (var student in AllStudents)
                {
                    if (student.DormName == currDormName)
                    {
                        student.DormName = newDormName;
                    }
                }
                foreach (var group in AllGroups)
                {
                    foreach (var student in group.studentGroup)
                    {
                        if (student.DormName == currDormName)
                        {
                            student.DormName = newDormName;
                        }
                    }
                }
                foreach (var dorm in AllDorms)
                {
                    foreach (var student in dorm.studentDorm)
                    {
                        if (student.DormName == currDormName)
                        {
                            student.DormName = newDormName;
                        }
                    }
                }
            }
        }
        #endregion
       
        public static void MainMenu()
        {
            try
            {
                bool status = true;
                while (status)
                {
                    Console.Clear();
                    Console.WriteLine("\t\tMAIN MENU");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("1 - Управління студентами");
                    Console.WriteLine("2 - Управління групами");
                    Console.WriteLine("3 - Управління гуртожитками");
                    Console.WriteLine("4 - Меню видалення БД");
                    Console.WriteLine("5 - Зберегти БД");
                    Console.WriteLine("6 - Завантажити уснуючу БД");
                    Console.WriteLine("7 - Меню пошуку");
                    Console.WriteLine("8 - Закінчити роботу");
                    Console.WriteLine("-----------------------------------------");

                    string str = Console.ReadLine();
                    int _str = int.Parse(str);

                    switch (_str)
                    {
                        case 1:
                            {
                                StudentMenu();
                                break;
                            }
                        case 2:
                            {
                                GroupMenu();
                                break;
                            }
                        case 3:
                            {
                                DormMenu();
                                break;
                            }
                        case 4:
                            {
                                DeleteMenu();
                                break;
                            }
                        case 5:
                            {
                                SaveDataToDB();
                                break;
                            }
                        case 6:
                            {
                                GetDataFromDB();
                                break;
                            }
                        case 7:
                            {
                                SearchMenu();
                                break;
                            }
                        case 8:
                            {
                                Console.Clear();
                                Environment.Exit(0);
                                break;
                            }


                        default:
                            Console.Clear();
                            Console.Write("Перевірте коректність вводу даних!\n\nНатисніть ENTER щоб повернутись до MAIN MENU");
                            Console.ReadKey();
                            status = true;
                            break;
                    }
                }

            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine($@"Exception: {e.Message}");
                Console.Write("\nНатисніть ENTER щоб повернутись до MAIN MENU");
                Console.ReadKey();
                MainMenu();
            }
        }

        #region service menus
        private static void StudentMenu()
        {
            try
            {
                bool status = true;
                while (status)
                {
                    Console.Clear();
                    Console.WriteLine("\t\tSTUDENT MENU");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("1 - Переглянути всіх студентів");
                    Console.WriteLine("2 - Додати студента");
                    Console.WriteLine("3 - Видалити студента");
                    Console.WriteLine("4 - Змінити дані студента");
                    Console.WriteLine("5 - Пошук студента за його ID");
                    Console.WriteLine("6 - Повернутись до MAIN MENU");
                    Console.WriteLine("-----------------------------------------");

                    string str = Console.ReadLine();
                    int _str = int.Parse(str);
                    Console.Clear();
                    switch (_str)
                    {
                        case 1:
                            {
                                OutputAllStudents();
                                break;
                            }
                        case 2:
                            {
                                AllStudents.Add(AddStudent());
                                break;
                            }
                        case 3:
                            {
                                DeleteStudent();
                                break;
                            }
                        case 4:
                            {
                                ChangeStudentData();
                                break;
                            }
                        case 5:
                            {
                                GetStudentDataByID();
                                break;
                            }
                        case 6:
                            {
                                MainMenu();
                                break;
                            }
                        default:
                            Console.Clear();
                            Console.Write("Перевірте коректність вводу даних!\n\nНатисніть ENTER щоб повернутись до STUDENT MENU");
                            Console.ReadKey();
                            Console.Clear();
                            status = true;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine($@"Exception: {e.Message}");
                Console.Write("\nНатисніть ENTER щоб повернутись до STUDENT MENU");
                Console.ReadKey();
                Console.Clear();
                StudentMenu();
            }
        }
        private static void GroupMenu()
        {
            try
            {
                bool status = true;
                while (status)
                {
                    Console.Clear();
                    Console.WriteLine("\t\tGROUP MENU");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("1 - Переглянути всі групи");
                    Console.WriteLine("2 - Створити групу");
                    Console.WriteLine("3 - Додати студента до групи");
                    Console.WriteLine("4 - Перевести студента до іншої групи");
                    Console.WriteLine("5 - Видалити студента із групи");
                    Console.WriteLine("6 - Переглянути список студентів певної групи");
                    Console.WriteLine("7 - Змінити назву групи");
                    Console.WriteLine("8 - Видалити групу");
                    Console.WriteLine("9 - Видалити всі групи");
                    Console.WriteLine("10 - Повернутись до MAIN MENU");
                    Console.WriteLine("-----------------------------------------");

                    string str = Console.ReadLine();
                    int _str = int.Parse(str);

                    switch (_str)
                    {
                        case 1:
                            {
                                Console.Clear();
                                if (AllGroups.Count == 0) { Console.WriteLine("Не існує жодної групи!"); Console.ReadLine(); break; }

                                foreach (var group in AllGroups)
                                {
                                    OutputGroup(group);
                                    Console.WriteLine("---------------------------------------");
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 2:
                            {

                                AllGroups.Add(AddNewGroup());
                                break;
                            }
                        case 3:
                            {
                                SetStudentGroup();
                                break;
                            }
                        case 4:
                            {
                                TransferStudent();
                                break;
                            }
                        case 5:
                            {
                                DeleteStudentFromTheGroup();
                                break;
                            }
                        case 6:
                            {
                                OutputGroupInfo();
                                break;
                            }
                        case 7:
                            {
                                ChangeGroupName();
                                break;
                            }
                        case 8:
                            {
                                DeleteGroup();
                                break;
                            }
                        case 9:
                            {
                                DeleteALLGroups();
                                break;
                            }
                        case 10:
                            {
                                MainMenu();
                                break;
                            }

                        default:
                            Console.Clear();
                            Console.Write("Перевірте коректність вводу даних!\n\nНатисніть ENTER щоб повернутись до GROUP MENU");
                            Console.ReadKey();
                            status = true;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine($@"Exception: {e.Message}");
                Console.Write("\nНатисніть ENTER щоб повернутись до GROUP MENU");
                Console.ReadKey();
                GroupMenu();
            }
        }
        private static void DormMenu()
        {
            try
            {
                bool status = true;
                while (status)
                {
                    Console.Clear();
                    Console.WriteLine("\t\tDORMITORY MENU");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("1 - Переглянути всі гуртожитки");
                    Console.WriteLine("2 - Створити гуртожиток");
                    Console.WriteLine("3 - Додати студента до гуртожитка");
                    Console.WriteLine("4 - Перевести студента до іншого гуртожитка");
                    Console.WriteLine("5 - Виселити студента з гуртожитка");
                    Console.WriteLine("6 - Переглянути список студентів певного гуртожитка");
                    Console.WriteLine("7 - Змінити назву гуртожитка");
                    Console.WriteLine("8 - Видалити гуртожиток з БД");
                    Console.WriteLine("9 - Видалити всі гуртожитки");
                    Console.WriteLine("10 - Повернутись до MAIN MENU");
                    Console.WriteLine("-----------------------------------------");

                    string str = Console.ReadLine();
                    int _str = int.Parse(str);

                    switch (_str)
                    {
                        case 1:
                            {
                                Console.Clear();
                                if (AllDorms.Count == 0) { Console.WriteLine("Не існує жодного гуртожитка!"); Console.ReadLine(); break; }

                                foreach (var dorm in AllDorms)
                                {
                                    OutputDorm(dorm);
                                    Console.WriteLine("---------------------------------------");
                                }
                                Console.ReadKey();
                                break;
                            }
                        case 2:
                            {
                                AllDorms.Add(AddNewDorm());
                                break;
                            }
                        case 3:
                            {
                                SetStudentDorm();
                                break;
                            }
                        case 4:
                            {
                                TransferStudentToAnotherDorm();
                                break;
                            }
                        case 5:
                            {
                                DeleteStudentFromTheDorm();
                                break;
                            }
                        case 6:
                            {
                                OutputDormInfo();
                                break;
                            }
                        case 7:
                            {
                                ChangeDormName();
                                break;
                            }
                        case 8:
                            {
                                DeleteDorm();
                                break;
                            }
                        case 9:
                            {
                                DeleteALLDorms();
                                break;
                            }
                        case 10:
                            {
                                MainMenu();
                                break;
                            }

                        default:
                            Console.Clear();
                            Console.Write("Перевірте коректність вводу даних!\n\nНатисніть ENTER щоб повернутись до DORMITORY MENU");
                            Console.ReadKey();
                            status = true;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine($@"Exception: {e.Message}");
                Console.Write("\nНатисніть ENTER щоб повернутись до DORMITORY MENU");
                Console.ReadKey();
                DormMenu();
            }
        }
        private static void DeleteMenu()
        {
            try
            {
                bool status = true;
                while (status)
                {
                    Console.Clear();
                    Console.WriteLine("\t\tDELETE MENU");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("1 - Видалити дані з БД");
                    Console.WriteLine("2 - Видалити БД");
                    Console.WriteLine("3 - Повернутись до MAIN MENU");
                    Console.WriteLine("4 - Закінчити роботу");
                    Console.WriteLine("-----------------------------------------");

                    string str = Console.ReadLine();
                    int _str = int.Parse(str);
                    switch (_str)
                    {
                        case 1:
                            {
                                Console.Clear();
                                XMLProvider<object> provider = new XMLProvider<object>(SetFileName());
                                if (provider.FileExists())
                                {
                                    provider.DeleteFileData();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Exception: Заданий файл не існує");
                                    Console.Write("\nНатисніть ENTER щоб повернутись до DELETE MENU");
                                    Console.ReadKey();
                                    Console.Clear();
                                    DeleteMenu();
                                }
                                break;
                            }
                        case 2:
                            {
                                Console.Clear();
                                XMLProvider<object> provider = new XMLProvider<object>(SetFileName());
                                if (provider.FileExists())
                                {
                                    provider.DeleteFile();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Exception: Заданий файл не існує");
                                    Console.Write("\nНатисніть ENTER щоб повернутись до DELETE MENU");
                                    Console.ReadKey();
                                    Console.Clear();
                                    DeleteMenu();
                                }
                                break;
                            }
                        case 3:
                            {
                                Console.Clear();
                                MainMenu();
                                break;
                            }
                        case 4:
                            {
                                Console.Clear();
                                Environment.Exit(0);
                                break;
                            }
                        default:
                            Console.Clear();
                            Console.Write("Перевірте коректність вводу даних!\n\nНатисніть ENTER щоб повернутись до DELETE MENU");
                            Console.ReadKey();
                            Console.Clear();
                            status = true;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine($@"Exception: {e.Message}");
                Console.Write("\nНатисніть ENTER щоб повернутись до DELETE MENU");
                Console.ReadKey();
                Console.Clear();
                DeleteMenu();
            }
        }
        private static void SearchMenu()
        {
            try
            {
                bool status = true;
                while (status)
                {
                    Console.Clear();
                    Console.WriteLine("\t\tSEARCH MENU");
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("1 - Пошук студентів по імені");
                    Console.WriteLine("2 - Пошук студентів за групою");
                    Console.WriteLine("3 - Пошук студентів за номером гуртожитку");
                    Console.WriteLine("4 - Повернутись до MAIN MENU");
                    Console.WriteLine("-----------------------------------------");

                    string str = Console.ReadLine();
                    int _str = int.Parse(str);
                    switch (_str)
                    {
                        case 1:
                            {
                                SearchForStudentsByTheirName();
                                break;
                            }
                        case 2:
                            {
                                SearchForStudentsByTheirGroup();
                                break;
                            }
                        case 3:
                            {
                                SearchForStudentsByTheirDorm();
                                break;
                            }
                        case 4:
                            {
                                Console.Clear();
                                MainMenu();
                                break;
                            }
                        default:
                            Console.Clear();
                            Console.Write("Перевірте коректність вводу даних!\n\nНатисніть ENTER щоб повернутись до SEARCH MENU");
                            Console.ReadKey();
                            Console.Clear();
                            status = true;
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine($@"Exception: {e.Message}");
                Console.Write("\nНатисніть ENTER щоб повернутись до SEARCH MENU");
                Console.ReadKey();
                Console.Clear();
                SearchMenu();            }
        }
    }
    #endregion
}

