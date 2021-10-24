using System;
using System.Data.SqlClient;
using System.Threading;

namespace ADO_NET_Practice
{
    class Program
    {
        enum Product
        {
            Fruit = 1,
            Vegetable = 2
        }
        static void DataReader(string query, SqlConnection connection)
        {
            SqlDataReader sqlDataReader = new SqlCommand(query, connection).ExecuteReader();
            do
            {
                for (int i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    Console.Write(sqlDataReader.GetName(i) + " ");
                }
                Console.WriteLine();
                while (sqlDataReader.Read())
                {
                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {
                        if (sqlDataReader.GetName(i) == "Type")
                        {
                            switch (Int32.Parse(sqlDataReader[i].ToString()))
                            {
                                case (int)Product.Fruit:
                                    Console.Write("fruit ");
                                    break;
                                case (int)Product.Vegetable:
                                    Console.Write("vegetable ");
                                    break;
                            }
                        }
                        else
                        {
                            Console.Write(sqlDataReader[i] + " ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            } while (sqlDataReader.NextResult());
            sqlDataReader.Close();
            Console.Write("Нажмите для продолжения..");
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VegetablesFruits;Integrated Security=true;";
            string query;

            int write;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"Статус: {connection.State}");
                        Console.WriteLine("Выберите действие:" +
                    "\n1. Подключиться к базе данных" +
                    "\n2. Отключиться от базы данных" +
                    "\n3. Далее" +
                    "\n4. Выход");
                        write = Int32.Parse(Console.ReadLine());
                        switch (write)
                        {
                            case 1:
                                if (connection.State == System.Data.ConnectionState.Closed)
                                {
                                    connection.Open();
                                    Console.WriteLine("Connection successful!");
                                    Thread.Sleep(1000);
                                    continue;
                                }
                                Console.WriteLine("There is already a connection");
                                Thread.Sleep(1000);
                                continue;
                            case 2:
                                if (connection.State == System.Data.ConnectionState.Open)
                                {
                                    connection.Close();
                                    Console.WriteLine("The connection was successfully dropped");
                                    Thread.Sleep(1000);
                                    continue;
                                }
                                Console.WriteLine("There was no connection");
                                Thread.Sleep(1000);
                                continue;
                            case 3:
                                if (connection.State == System.Data.ConnectionState.Closed)
                                {
                                    Console.WriteLine("Отсутствует соединение!");
                                    Thread.Sleep(1000);
                                    continue;
                                }
                                break;
                            case 4:
                                return;
                            default:
                                continue;

                        }
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Выберите действие:" +
                                "\n1. Отображение всей информации из таблицы с овощами и фруктами." +
                                "\n2. Отображение всех названий овощей и фруктов." +
                                "\n3. Отображение всех цветов." +
                                "\n4. Показать максимальную калорийность." +
                                "\n5. Показать минимальную калорийность." +
                                "\n6. Показать среднюю калорийность." +
                                "\n7. Показать количество овощей." +
                                "\n8. Показать количество фруктов." +
                                "\n9. Показать количество овощей и фруктов заданного цвета." +
                                "\n10. Показать количество овощей фруктов каждого цвета." +
                                "\n11. Показать овощи и фрукты с калорийностью ниже указанной." +
                                "\n12. Показать овощи и фрукты с калорийностью выше указанной." +
                                "\n13. Показать овощи и фрукты с калорийностью в указанном диапазоне." +
                                "\n14. Показать все овощи и фрукты, у которых цвет желтый или красный.");
                            write = Int32.Parse(Console.ReadLine());
                            switch (write)
                            {
                                case 1:
                                    query = "SELECT Products.Name, Products.Type, Colors.Name, Colories.Count[Colories] FROM Products, Colors, Colories, ColorsColoriesProducts" +
                                        " WHERE ColorsColoriesProducts.ColorId = Colors.Id AND ColorsColoriesProducts.ColorieId = Colories.Id AND ColorsColoriesProducts.ProductId = Products.Id";
                                    DataReader(query, connection);
                                    continue;
                                case 2:
                                    query = "SELECT Products.Name, Products.Type FROM Products" +
                                    " ORDER BY Products.Type";
                                    DataReader(query, connection);
                                    continue;
                                case 3:
                                    query = "SELECT * FROM Colors";
                                    DataReader(query, connection);
                                    continue;
                                case 4:
                                    query = "SELECT Max(Colories.Count)[MaxColorie] FROM Colories";
                                    DataReader(query, connection);
                                    continue;
                                case 5:
                                    query = "SELECT Min(Colories.Count)[MinColorie] FROM Colories";
                                    DataReader(query, connection);
                                    continue;
                                case 6:
                                    query = "SELECT TOP 1 result.Count[MediumColories]" +
                                        " FROM(SELECT TOP(SELECT CAST((SELECT ROUND((CAST((SELECT COUNT(Colories.Id) FROM Colories) as float) / 2.0), 0)) as INTEGER)) *FROM Colories" +
                                        " Order BY Colories.Count) as result" +
                                        " ORDER BY result.Count DESC";
                                    DataReader(query, connection);
                                    continue;
                                case 7:
                                    query = "SELECT COUNT(Products.Id)[CountVegetables] FROM Products, ColorsColoriesProducts" +
                                        " WHERE Products.Type = 2 AND ColorsColoriesProducts.ProductId = Products.Id";
                                    DataReader(query, connection);
                                    continue;
                                case 8:
                                    query = "SELECT COUNT(Products.Id)[CountFruits] FROM Products, ColorsColoriesProducts" +
                                        " WHERE Products.Type = 1 AND ColorsColoriesProducts.ProductId = Products.Id";
                                    DataReader(query, connection);
                                    continue;
                                case 9:
                                    {
                                        string str;
                                        Console.WriteLine("Enter name of color => ");
                                        str = Console.ReadLine();
                                        query = $"SELECT COUNT(Colors.Id)[CountOf{str}Products] FROM Products, Colors, ColorsColoriesProducts" +
                                            $" WHERE Colors.Name = '{str}' AND ColorsColoriesProducts.ColorId = Colors.Id AND ColorsColoriesProducts.ProductId = Products.Id";
                                    }
                                    DataReader(query, connection);
                                    continue;
                                case 10:
                                    query = "SELECT COUNT(Colors.Id)[CountOfProducts], Colors.Name[ColorName] FROM Products, Colors, ColorsColoriesProducts" +
                                        " WHERE ColorsColoriesProducts.ColorId = Colors.Id AND ColorsColoriesProducts.ProductId = Products.Id" +
                                        " GROUP BY Colors.Name";
                                    DataReader(query, connection);
                                    continue;
                                case 11:
                                    Console.Write("Enter colorie => ");
                                    query = "SELECT Products.Name, Products.Type, Colors.Name, Colories.Count FROM ColorsColoriesProducts, Colories, Products, Colors " +
                                        $"WHERE Colories.Count < {Int32.Parse(Console.ReadLine())} AND ColorsColoriesProducts.ColorieId = Colories.Id AND ColorsColoriesProducts.ProductId = Products.Id" +
                                        " AND ColorsColoriesProducts.ColorId = Colors.Id";
                                    DataReader(query, connection);
                                    continue;
                                case 12:
                                    Console.Write("Enter colorie => ");
                                    query = "SELECT Products.Name, Products.Type, Colors.Name, Colories.Count FROM ColorsColoriesProducts, Colories, Products, Colors " +
                                        $"WHERE Colories.Count > {Int32.Parse(Console.ReadLine())} AND ColorsColoriesProducts.ColorieId = Colories.Id AND ColorsColoriesProducts.ProductId = Products.Id" +
                                        " AND ColorsColoriesProducts.ColorId = Colors.Id";
                                    DataReader(query, connection);
                                    continue;
                                case 13:
                                    {
                                        Console.Write("Enter min colorie => ");
                                        int min = Int32.Parse(Console.ReadLine());
                                        Console.Write("Enter max colorie => ");
                                        int max = Int32.Parse(Console.ReadLine());
                                        query = "SELECT Products.Name, Products.Type, Colors.Name, Colories.Count FROM ColorsColoriesProducts, Colories, Products, Colors" +
                                            $" WHERE {min} < Colories.Count AND Colories.Count < {max} AND ColorsColoriesProducts.ColorieId = Colories.Id AND ColorsColoriesProducts.ProductId = Products.Id" +
                                            " AND ColorsColoriesProducts.ColorId = Colors.Id";
                                    }
                                    DataReader(query, connection);
                                    continue;
                                case 14:
                                    query = "SELECT Products.Name, Products.Type, Colors.Name FROM Products, ColorsColoriesProducts, Colors" +
                                        " WHERE(Colors.Name = 'Yellow' OR Colors.Name = 'Red') AND ColorsColoriesProducts.ColorId = Colors.Id AND ColorsColoriesProducts.ProductId = Products.Id";
                                    DataReader(query, connection);
                                    continue;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error!");
            }
        }
    }
}
