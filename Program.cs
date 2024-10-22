using System;
using System.Data.SQLite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Data Source=example.db;Version=3;";
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            Console.WriteLine(@"Выберите действие (введите цифру от 1 до 20):
1. Добавить нового студента.
2. Добавить нового преподавателя.
3. Добавить новый курс.
4. Добавить новый экзамен.
5. Добавить новую оценку.
6. Изменить информацию о студенте (факультет).
7. Изменить информацию о преподавателе (кафедру).
8. Изменить информацию о курсе (описание).
9. Удалить студента.
10. Удалить преподавателя.
11. Удалить курс.
12. Удалить экзамен.
13. Получить список студентов по факультету.
14. Получить список курсов, читаемых определенным преподавателем.
15. Получить список студентов, зачисленных на конкретный курс.
16. Получить оценки студентов по определенному курсу.
17. Узнать средний балл студента по определенному курсу.
18. Узнать средний балл студента в целом.
19. Узнать средний балл по факультету
20. Выйти.");


            int n = 0;

            CreateTables(connection);

            while (n != 20)
            {
                Console.WriteLine("Выберите действие");
                n = Input();

                switch (n)
                {
                    case 1:
                        string name = GetValue<string>("Введите имя студента");

                        string surname = GetValue<string>("Введите фамилию студента");

                        string department = GetValue<string>("Введите факультет");

                        DateOnly Date = GetInputDate("Введите дату рождения (YYYY-MM-DD)");

                        InsertStudent(connection, name, surname, department, Date);
                        break;
                    case 2:
                        name = GetValue<string>("Введите имя преподавателя");

                        surname = GetValue<string>("Введите фамилию преподавателя ");

                        department = GetValue<string>("Введите факультет");

                        InsertTeacher(connection, name, surname, department);
                        break;
                    case 3:
                        Console.WriteLine("Введите имя и фамилию преподавателя, ведущего курс, через пробел");
                        NS = Console.ReadLine().Split();

                        Console.WriteLine("Введите название курса");
                        string course = Console.ReadLine();

                        Console.WriteLine("Введите описание курса");
                        string description = Console.ReadLine();

                        InsertCourse(connection, NS[0], NS[1], course, description);
                        break;
                    case 4:
                        Console.WriteLine("Введите название курса, на котором будет проводиться экзамен");
                        course = Console.ReadLine();

                        Date = GetInputDate("Введите дату проведения экзамена (YYYY-MM-DD)");

                        Console.WriteLine("Введите максимальный балл за экзамен");
                        int grade = int.Parse(Console.ReadLine());

                        InsertExam(connection, course, Date, grade);
                        break;
                    case 5:
                        Console.WriteLine("Введите имя и фамилию студента, получившего оценку, через пробел");
                        NS = Console.ReadLine().Split();

                        Date = GetInputDate("Введите дату экзамена (YYYY-MM-DD), за который была получена оценка");

                        Console.WriteLine("Введите оценку");
                        int score = int.Parse(Console.ReadLine());

                        InsertGrade(connection, NS[0], NS[1], Date, score);
                        break;
                    case 6:
                        Console.WriteLine("Введите имя и фамилию студента через пробел");
                        NS = Console.ReadLine().Split();

                        Console.WriteLine("Введите новый факультет");
                        department = Console.ReadLine();

                        UpdateStudent(connection, NS[0], NS[1], department);
                        break;
                    case 7:
                        Console.WriteLine("Введите имя и фамилию преподавателя через пробел");
                        NS = Console.ReadLine().Split();

                        Console.WriteLine("Введите новый факультет");
                        department = Console.ReadLine();
                        UpdateTeacher(connection, NS[0], NS[1], department);
                        break;
                    case 8:
                        Console.WriteLine("Введите название курса");
                        course = Console.ReadLine();

                        Console.WriteLine("Введите новое описание");
                        string newDescription = Console.ReadLine();

                        UpdateCourse(connection, course, newDescription);
                        break;
                    case 9:
                        Console.WriteLine("Введите имя и фамилию студента через пробел");
                        NS = Console.ReadLine().Split();

                        DeleteStudent(connection, NS[0], NS[1]);
                        break;
                    case 10:
                        Console.WriteLine("Введите имя и фамилию преподавателя через пробел");
                        NS = Console.ReadLine().Split();

                        DeleteTeacher(connection, NS[0], NS[1]);
                        break;
                    case 11: 
                        Console.WriteLine("Введите название курса");
                        course = Console.ReadLine();

                        DeleteCourse(connection, course);
                        break;
                    case 12:
                        Date = GetInputDate("Введите дату проведения экзамена (YYYY-MM-DD)");

                        DeleteExam(connection, Date);
                        break;
                    case 13:
                        Console.WriteLine("Введите факультет");
                        department = Console.ReadLine();

                        GetStudentsDepartment(connection, department);
                        break;
                    case 14:
                        Console.WriteLine("Введите имя и фамилию преподавателя через пробел");
                        NS = Console.ReadLine().Split();

                        GetCoursesTeacher(connection, NS[0], NS[1]);
                        break;
                    case 15:
                        Console.WriteLine("Введите название курса");
                        course = Console.ReadLine();

                        GetStudentsCourse(connection, course);
                        break;
                    case 16:
                        Console.WriteLine("Введите название курса");
                        course = Console.ReadLine();

                        GetStudentsGrades(connection, course);
                        break;
                    case 17:
                        Console.WriteLine("Введите имя и фамилию студента через пробел");
                        NS = Console.ReadLine().Split();

                        Console.WriteLine("Введите факультет");
                        department = Console.ReadLine();

                        GetAverageStudentCourse(connection, NS[0], NS[1], department);
                        break;
                    case 18:
                        Console.WriteLine("Введите имя и фамилию студента через пробел");
                        NS = Console.ReadLine().Split();

                        GetAverageStudent(connection, NS[0], NS[1]);
                        break;
                    case 19:
                        Console.WriteLine("Введите факультет");
                        department = Console.ReadLine();

                        GetAverageDepartment(connection, department);
                        break;
                    default:
                        if (n != 20)
                        {
                            Console.WriteLine("Несуществующее дествие. попробуйте ещё раз");
                        }
                        break;
                }
            }

            connection.Close();
            Console.WriteLine("Соединение с базой данных закрыто.");
        }
    }

    static void CreateTables(SQLiteConnection connection)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Students (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                surname TEXT NOT NULL,
                department TEXT NOT NULL,
                date DATE NOT NULL
            );";
            command.ExecuteNonQuery();

            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Teachers (
                id INT PRIMARY KEY AUTOINCREMENT,
                name TEXT NOT NULL,
                surname TEXT NOT NULL,
                department TEXT NOT NULL
            );";
            command.ExecuteNonQuery();

            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Courses (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                title TEXT NOT NULL,
                description TEXT NOT NULL,
                teacher_id INTEGER,
                FOREIGN KEY (teacher_id) REFERENCES Teachers(id)
            );";
            command.ExecuteNonQuery();

            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Exams (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                date TEXT NOT NULL,
                max_score INTEGER NOT NULL,
                course_id INTEGER,
                FOREIGN KEY (course_id) REFERENCES Courses(id)
            );";
            command.ExecuteNonQuery();

            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Grades (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                score INTEGER NOT NULL,
                student_id INTEGER,
                exam_id INTEGER,
                FOREIGN KEY (student_id) REFERENCES Students(id),
                FOREIGN KEY (exam_id) REFERENCES Exams(id)
            );";
            command.ExecuteNonQuery();
        }
    }

    static void InsertStudent(SQLiteConnection connection, string name, string surname, string department, DateOnly date)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "INSERT INTO Students (name, surname, department, date) VALUES (@name, @surname, @department, @date)";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@department", department);
            command.Parameters.AddWithValue("@date", date);
            command.ExecuteNonQuery();
            Console.WriteLine($"Ученик {name} {surname} добавлен.");
        }
    }

    static void InsertTeacher(SQLiteConnection connection, string name, string surname, string department)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "INSERT INTO Teachers (name, surname, department) VALUES (@name, @surname, @department)";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@department", department);
            command.ExecuteNonQuery();
            Console.WriteLine($"Преподаватель {name} {surname} добавлен.");
        }
    }

    static void InsertCourse(SQLiteConnection connection, string name, string surname, string title, string description)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "INSERT INTO Courses (title, description, teacher_id) VALUES (@title, @description, (SELECT id FROM Teachers WHERE name = @name AND surname = @surname))";
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.ExecuteNonQuery();
            Console.WriteLine($"Курс {title} добавлен.");
        }
    }

    static void InsertExam(SQLiteConnection connection, string title, DateOnly date, int max_score)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "INSERT INTO Exams (date, max_score, course_id) VALUES (@date, @max_score, (SELECT id FROM Courses Where title = @title))";
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@max_score", max_score);
            command.Parameters.AddWithValue("@title", title);
            command.ExecuteNonQuery();
            Console.WriteLine($"Экзамен на {date} добавлен.");
        }
    }

    static void InsertGrade(SQLiteConnection connection, string name, string surname, DateOnly date, int score)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "INSERT INTO Grades (score, student_id, exam_id) VALUES (@score, (SELECT id FROM Students WHERE name = @name AND surname = @surname), ((SELECT id FROM Exams WHERE date = @date)))";
            command.Parameters.AddWithValue("@score", score);
            command.Parameters.AddWithValue("@date", date);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.ExecuteNonQuery();
            Console.WriteLine($"Оценка добавлена.");
        }
    }

    static void UpdateStudent(SQLiteConnection connection, string name, string surname, string newDepartment)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "UPDATE Students SET department = @department WHERE name = @name AND surname = @surname";
            command.Parameters.AddWithValue("@department", newDepartment);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.ExecuteNonQuery();
            Console.WriteLine($"Факультет студента '{name} {surname}' успешно обновлен.");
        }
    }

    static void UpdateTeacher(SQLiteConnection connection, string name, string surname, string newDepartment)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "UPDATE Teachers SET department = @department WHERE name = @name AND surname = @surname";
            command.Parameters.AddWithValue("@department", newDepartment);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.ExecuteNonQuery();
            Console.WriteLine($"Кафедра преподавателя '{name} {surname}' успешно обновлена.");
        }
    }

    static void UpdateCourse(SQLiteConnection connection, string title, string newDescription)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "UPDATE Courses SET description = @description WHERE title = @title";
            command.Parameters.AddWithValue("@description", newDescription);
            command.Parameters.AddWithValue("@title", title);
            command.ExecuteNonQuery();
            Console.WriteLine($"Кафедра преподавателя '{title}' успешно обновлено.");
        }
    }

    static void DeleteStudent(SQLiteConnection connection, string name, string surname)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "DELETE FROM Students WHERE name = @name AND surname = @surname";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.ExecuteNonQuery();
            Console.WriteLine($"Студент '{name} {surname}' успешно удален.");
        }
    }

    static void DeleteTeacher(SQLiteConnection connection, string name, string surname)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "DELETE FROM Teachers WHERE name = @name AND surname = @surname";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.ExecuteNonQuery();
            Console.WriteLine($"Учитель '{name} {surname}' успешно удален.");
        }
    }

    static void DeleteCourse(SQLiteConnection connection, string title)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "DELETE FROM Courses WHERE title = @title";
            command.Parameters.AddWithValue("@title", title);
            command.ExecuteNonQuery();
            Console.WriteLine($"Курс '{title}' успешно удален.");
        }
    }

    static void DeleteExam(SQLiteConnection connection, DateOnly date)
    {
        using (var command = new SQLiteCommand(connection))
        {
            command.CommandText = "DELETE FROM Exams WHERE date = @date";
            command.Parameters.AddWithValue("@date", date);
            command.ExecuteNonQuery();
            Console.WriteLine($"Экзамен '{date}' успешно удален.");
        }
    }

    static void GetStudentsDepartment(SQLiteConnection connection, string department)
    {
        using (var command = new SQLiteCommand("SELECT * FROM Students WHERE department = @department", connection))
        {
            command.Parameters.AddWithValue("@department", department); 
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]}, Имя: {reader["name"]} {reader["surname"]}, Дата рождения: {reader["date"]}");
                }
            }
        }
    }

    static void GetCoursesTeacher(SQLiteConnection connection, string name, string surname)
    {
        using (var command = new SQLiteCommand("SELECT c.title FROM Courses c JOIN Teachers t ON c.teacher_id = t.id WHERE t.name = @name AND t.surname = @surname", connection))
        {
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["title"]}");
                }
            }
        }
    }

    static void GetStudentsCourse(SQLiteConnection connection, string course)
    {
        using (var command = new SQLiteCommand("SELECT s.name, s.surname FROM Students s JOIN Teachers t ON t.department = s.department JOIN Courses c ON t.id = c.teacher_id WHERE c.title = @course", connection))
        {
            command.Parameters.AddWithValue("@title", course);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]}, Имя: {reader["name"]} {reader["surname"]}, Дата рождения: {reader["date"]}");
                }
            }
        }
    }

    static void GetStudentsGrades(SQLiteConnection connection, string course)
    {
        using (var command = new SQLiteCommand("SELECT s.name, s.surname, g.grade FROM Students s JOIN Teachers t ON t.department = s.department JOIN Courses c ON t.id = c.teacher_id JOIN Grades g ON s.id = g.student_id WHERE c.title = @course", connection))
        {
            command.Parameters.AddWithValue("@title", course);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Имя: {reader["name"]} {reader["surname"]}, Оценка: {reader["score"]}");
                }
            }
        }
    }

    static void GetAverageStudentCourse(SQLiteConnection connection, string name, string surname, string course)
    {
        using (var command = new SQLiteCommand("SELECT AVG(g.score) AS average_grade FROM Grades AS g JOIN Exams AS e ON g.exam_id = e.id JOIN Courses AS c ON e.course_id = c.id JOIN Students AS s ON g.student_id = s.id WHERE c.title = @course AND s.name = @name AND s.surname = surname", connection))
        {
            command.Parameters.AddWithValue("@title", course);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Ср. оценка студента по курсу: {reader[0]}");
                }
            }
        }
    }

    static void GetAverageStudent(SQLiteConnection connection, string name, string surname)
    {
        using (var command = new SQLiteCommand("SELECT AVG(g.score) AS average_grade FROM Grades AS g JOIN Students AS s ON g.student_id = s.id WHERE s.name = @name AND s.surname = surname", connection))
        {
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Ср. оценка студента: {reader[0]}");
                }
            }
        }
    }

    static void GetAverageDepartment(SQLiteConnection connection, string department)
    {
        using (var command = new SQLiteCommand("SELECT AVG(g.score) AS average_grade FROM Grades AS g JOIN Students AS s ON g.student_id = s.id WHERE s.department = @department", connection))
        {
            command.Parameters.AddWithValue("@department", department);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Ср. оценка по факультету: {reader[0]}");
                }
            }
        }
    }

    static int Input()
    {
        int n;
        string input = Console.ReadLine();
        int.TryParse(string.Join("", input.Where(c => char.IsDigit(c))), out n);

        return n;
    }

    public static T GetValue<T>(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            if (TryParse<T>(input, out T result))
            {
                return result;
            }
            else
            {
                Console.WriteLine($"Некорректный ввод. Пожалуйста, введите значение типа {typeof(T).Name}.");
            }
        }
    }

    private static bool TryParse<T>(string input, out T result)
    {
        try
        {
            result = (T)Convert.ChangeType(input, typeof(T));
            return true;
        }
        catch (FormatException)
        {
            result = default(T);
            return false;
        }
        catch (InvalidCastException)
        {
            result = default(T);
            return false;
        }
    }

    static DateOnly GetInputDate(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            if (DateOnly.TryParse(input, out DateOnly result))
            {
                return result;
            }
            else
            {
                Console.WriteLine($"Некорректный формат даты. Пожалуйста, введите дату в формате YYYY-MM-DD.");
            }
        }
    }
}
