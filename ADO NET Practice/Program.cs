using System;
using System.Data.SqlClient;
using System.Threading;

namespace ADO_NET_Practice
{
    class Program
    {
        enum Product
        {
            Vegetable = 1,
            Fruit = 2
        }
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Course;Integrated Security=true;";
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
