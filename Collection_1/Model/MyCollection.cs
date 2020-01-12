using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Collection_1.Model
{
    /// <summary>
    /// Class represent five collections of students.
    /// Those collesctions could be switched.
    /// </summary>
    class MyCollection
    {
        //collections
        private List<Student> list;
        private Student[] array;
        private Dictionary<int, Student> dictionary;
        private Queue<Student> queue;
        private Stack<Student> stack;

        //dictionary index
        private int dicIndex;

        //collection switch
        private int activeCollection;
        public int ActiveCollection
        {
            get { return activeCollection; }
            set
            {
                if (0 <= value && value < 5) { activeCollection = value; }
            }
        }

        /// <summary>
        /// Class constructor instantiate all five collections.
        /// </summary>
        /// <param name="length">length of array</param>
        public MyCollection(int length = 100)
        {
            list = new List<Student>();
            array = new Student[length];
            dictionary = new Dictionary<int, Student>();
            queue = new Queue<Student>();
            stack = new Stack<Student>();
            dicIndex = 0;
        }

        /// <summary>
        /// Method add student in active collection and return result of succes.
        /// </summary>
        /// <param name="name">Name of student</param>
        /// <param name="schoolClass">class, that studetn is visiting</param>
        /// <param name="day_of_bornDate">day of born date</param>
        /// <param name="month_of_bornDate">month of born date</param>
        /// <param name="year_of_bornDate">year of born date</param>
        /// <param name="gender">gender of student</param>
        /// <returns>true of false according to adding succes</returns>
        public bool Add(string name, string schoolClass, int day_of_bornDate, int month_of_bornDate, int year_of_bornDate, string gender)
        {
            //creating student
            Student student = new Student(name, schoolClass, new BornDate(day_of_bornDate, month_of_bornDate, year_of_bornDate), ("žena" == gender ? false : true));

            switch (activeCollection)
            {
                case 0:
                    list.Add(student);
                    break;
                case 1:
                    for (int i = 0; i < array.Length; i++)
                    {
                        //fill student into blank space
                        if (array[i] is null) { array[i] = student; break; }
                    }
                    return false;
                case 2:
                    dictionary.Add(dicIndex++, student);
                    break;
                case 3:
                    queue.Enqueue(student);
                    break;
                case 4:
                    stack.Push(student);
                    break;
            }
            return true;
        }

        /// <summary>
        /// Method delete Student in active collection accoring to parametrs of student.
        /// </summary>
        /// <param name="name">Name of student</param>
        /// <param name="schoolClass">class, that studetn is visiting</param>
        /// <param name="day_of_bornDate">day of born date</param>
        /// <param name="month_of_bornDate">month of born date</param>
        /// <param name="year_of_bornDate">year of born date</param>
        /// <param name="gender">gender of student</param>
        /// <returns>true of false according to deleting succes</returns>
        public bool Remove(string name, string schoolClass, int day_of_bornDate, int month_of_bornDate, int year_of_bornDate, string gender)
        {
            Student student = new Student(name, schoolClass, new BornDate(day_of_bornDate, month_of_bornDate, year_of_bornDate), ("žena" == gender ? false : true));
            switch (activeCollection)
            {
                //list
                case 0:
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (student.ToString() == list[i].ToString())
                        {
                            list.RemoveAt(i);
                            return true;
                        }
                    }
                    break;

                //array
                case 1:
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (student.ToString() == array[i].ToString())
                        {
                            array[i] = null;
                            return true;
                        }
                    }
                    return false;

                case 2:
                    foreach (var item in dictionary)
                    {
                        if (student.ToString() == item.Value.ToString())
                        {
                            dictionary.Remove(item.Key);
                            return true;
                        }
                    }
                    return false;

                //queue
                case 3:
                    //if first student is student to remove
                    Student firstStudent = queue.Peek();
                    if (student.ToString() == firstStudent.ToString())
                    {
                        queue.Dequeue();
                        return true;
                    }

                    //compare student to remove with each student
                    //if collection contains student, remove student and reorganize collection to original state
                    Student lastCheckedStudent;
                    while (true)
                    {
                        lastCheckedStudent = queue.Peek();
                        //if queue is in original state
                        if (firstStudent.ToString() == lastCheckedStudent.ToString()) { return false; }
                        //if student was found
                        if (student.ToString() == lastCheckedStudent.ToString())
                        {
                            //remove student
                            queue.Dequeue();
                            //reorganize to original state
                            while (true)
                            {
                                if (firstStudent.ToString() == queue.Peek().ToString()) { return true; }
                                queue.Enqueue(queue.Dequeue());
                            }
                        }
                        else { queue.Enqueue(queue.Dequeue()); }
                    }

                case 4:
                    //pop all students to dupicit stack
                    Stack<Student> duplicitStack = new Stack<Student>();
                    while (true)
                    {
                        //if student to remove was found,
                        //other students will be poped back in original stack
                        if (student.ToString() == stack.Peek().ToString())
                        {
                            stack.Pop();
                            while (stack.Count > 0)
                            {
                                stack.Push(duplicitStack.Pop());
                            }
                            return true;
                        }

                        duplicitStack.Push(stack.Pop());

                        //if original stack do not contains student to remove,
                        //all students will be poped back in original stack
                        if (stack.Count == 0)
                        {
                            while (stack.Count != 0)
                            {
                                stack.Push(duplicitStack.Pop());
                            }
                            break;
                        }
                    }
                    break;
            }
            return false;
        }

        //TODO:
        // dictionary collection and add it in all methods
        // methods:
        //  ToString
        //  ToList - will return list with strings (student.ToString())
        //  SaveToFile
        //  LoadFromFile
        //  Search

        /// <summary>
        /// Uloží aktivní kolekci do zvoleného souboru.
        /// </summary>
        /// <param name="path">cesta k souboru</param>
        /// <returns></returns>
        public bool SaveToFile(string path)
        {
            string collectionData = "";           
            if (activeCollection == 2)
            {
                foreach (var item in dictionary)
                {
                    collectionData += String.Format("{0};{1}\n", item.Key, item.Value.ToString());
                }
            }
                
            else { collectionData += ToString() +"\n"; }
            File.WriteAllText(path, collectionData);
            return true;
            //return false;
        }

        /// <summary>
        /// Načte ze zvoleného souboru data do aktivní kolekce.
        /// </summary>
        /// <param name="path">cesta k souboru</param>
        public bool LoadFromFile(string path)
        {
            if (File.Exists(path))
            {
                string collectionData = File.ReadAllText(path);

                switch (activeCollection)
                {
                    case 0: list = new List<Student>(); break;
                    case 1: array = new Student[array.Length]; break;
                    case 2: dictionary = new Dictionary<int, Student>(); break;
                    case 3: queue = new Queue<Student>(); break;
                    case 4: stack = new Stack<Student>(); break;
                }

                foreach (string studentData in collectionData.Split('\n'))
                {
                    string[] studentSeparetedData = studentData.Split(';');
                    
                    //if data were saved from dictionary and are supposed to load into dictionary
                    if (studentData.Length == 5 && activeCollection == 2)
                    {
                        string[] separateDate = studentSeparetedData[3].Split('.');
                        dictionary.Add(int.Parse(studentSeparetedData[0]), new Student(studentSeparetedData[1], studentSeparetedData[2], new BornDate(int.Parse(separateDate[0]), int.Parse(separateDate[1]), int.Parse(separateDate[2])), ("muž" == studentSeparetedData[4] ? true : false)));
                    }
                    //if data were saved from dictionary and are supposed to load into other collection
                    else if (studentData.Length == 5)
                    {
                        string[] separateDate = studentSeparetedData[3].Split('.');
                        Add(studentSeparetedData[1], studentSeparetedData[2], int.Parse(separateDate[0]), int.Parse(separateDate[1]), int.Parse(separateDate[2]), studentSeparetedData[4]);
                    }
                    //if data were saved from other collection (than dictionary) and are supposed to load into dictionary
                    else if (activeCollection == 2)
                    {
                        string[] separateDate = studentSeparetedData[3].Split('.');
                        dictionary.Add(dicIndex++, new Student(studentSeparetedData[0], studentSeparetedData[1], new BornDate(int.Parse(separateDate[0]), int.Parse(separateDate[1]), int.Parse(separateDate[2])), ("muž" == studentSeparetedData[3] ? true : false)));
                    }
                    else
                    {
                        string[] separateDate = studentSeparetedData[2].Split('.');
                        Add(studentSeparetedData[0], studentSeparetedData[1], int.Parse(separateDate[0]), int.Parse(separateDate[1]), int.Parse(separateDate[2]), studentSeparetedData[3]);
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Save the students data into list collection.
        /// </summary>
        /// <returns>list of students data</returns>
        public List<string> ToList()
        {
            List<string> students = new List<string>();
            switch (activeCollection)
            {
                case 0:
                    foreach (Student student in list)
                    {
                        students.Add(student.ToString());
                    }
                    break;
                case 1:
                    foreach (Student student in array)
                    {
                        if (!(student is null)) { students.Add(student.ToString()); }
                    }
                    break;
                case 2:
                    foreach (Student student in dictionary.Values)
                    {
                        students.Add(student.ToString());
                    }
                    break;
                case 3:
                    foreach (Student student in queue)
                    {
                        students.Add(student.ToString());
                    }
                    break;
                case 4:
                    foreach (Student student in stack)
                    {
                        students.Add(student.ToString());
                    }
                    break;
            }
            return students;
        }

        public string Search(string name, string schoolClass, int day_of_bornDate, int month_of_bornDate, int year_of_bornDate, string gender)
        {
            Student student = new Student(name, schoolClass, new BornDate(day_of_bornDate, month_of_bornDate, year_of_bornDate), ("žena" == gender ? false : true));
            List<string> students = ToList();
            return (students.Contains(student.ToString()) ? student.ToString() : null);
        }

        /// <summary>
        /// Převede aktivní kolekci na textový řetězec.
        /// </summary>
        /// <returns>vrátí řetězec</returns>
        public override string ToString()
        {
            string studentInfo = "";
            switch (activeCollection)
            {
                case 0:
                    foreach (Student s in list)
                    {
                        studentInfo += s.ToString();
                    }
                    break;
                case 1:
                    foreach (Student s in array)
                    {
                        if (!(s is null)) { studentInfo += s.ToString(); }
                    }
                    break;
                case 2:
                    foreach (var item in dictionary.Values)
                    {
                        studentInfo += item.ToString();
                    }
                    break;
                case 3:
                    foreach (Student s in queue)
                    {
                        studentInfo += s.ToString();
                    }
                    break;
                case 4:
                    foreach (Student s in stack)
                    {
                        studentInfo += s.ToString();
                    }
                    break;
            }
            return studentInfo;
        }
    }
}
