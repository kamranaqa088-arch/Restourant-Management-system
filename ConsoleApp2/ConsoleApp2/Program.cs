using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace RestourantManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int TotalBenefit = 0;
            bool isOpen = true;

            Restaurant restaurant = new Restaurant("MyRestaurant");


            while (isOpen)
            {
                Console.WriteLine("\n--- Restourant Management System ---\n" +
                    "\nОбщий прибыль: " + TotalBenefit + "\n" +
                    "\n[AddOrder] добавить заказ" +
                    "\n[AddEmloyee] добавить рабочего" +
                    "\n[AddMenuItem] добавить элемент для меню" +
                    "\n\n[ShowEmloyees] показать всех сотрудников" +
                    "\n\n[Exit] завершение работы");
                Console.Write("\n> ");
                
                
                string action = Console.ReadLine();
                Console.Write("\n");
                action = action.ToLower();
                switch (action)
                {
                    case "addorder":
                        {
                            if (restaurant.Menu.Count == 0 && restaurant.Orders.Count == 0)
                            {
                                Console.WriteLine("Меню пусто. Пожалуйста, добавьте блюда в меню или наймите персонал перед созданием заказа.");
                                Console.ReadLine();
                                break;
                            }
                            else
                            {
                                Order order = new Order();
                                bool anythingElse = true;
                                while (anythingElse)
                                {
                                    MenuItem itemToAdd = null;
                                    Console.WriteLine("Введите название блюда:");
                                    foreach (var item in restaurant.Menu)
                                    {
                                        Console.WriteLine($"- {item.Name} : {item.Price}");
                                    }

                                    string itemName = Console.ReadLine();
                                    foreach (var item in restaurant.Menu)
                                    {
                                        if (item.Name.ToLower() == itemName.ToLower())
                                        {
                                            order.AddItem(item);
                                            Console.WriteLine($"Блюдо {itemName} добавлено в заказ.");
                                            itemToAdd = item;
                                            Console.Clear();
                                        }
                                    }
                                    Console.WriteLine("Хотите добавить еще блюдо? (yes/not)");
                                    string response = Console.ReadLine();
                                    if (response.ToLower() == "not")
                                    {
                                        anythingElse = false;
                                    }
                                }
                                restaurant.PlaceOrder(order);
                                TotalBenefit += order.TotalPrice;
                                Console.WriteLine($"Заказ добавлен. Общая стоимость заказа: {order.TotalPrice}");
                                Console.ReadLine();
                                break;
                            }
                            
                        }
                    case "addemloyee":
                        {
                            Console.WriteLine("Введите имя сотрудника:");
                            string empName = Console.ReadLine();

                            Console.WriteLine("Введите номер сотрудника:");
                            int empNumber = Convert.ToInt32(Console.ReadLine());

                            Console.WriteLine("Введите зарплату сотрудника:");
                            int empSalary = Convert.ToInt32(Console.ReadLine());

                            TotalBenefit -= empSalary;

                            Employee employee = new Employee(empName, empNumber, empSalary);
                            restaurant.HireEmployee(employee);
                            Console.WriteLine($"Сотрудник {empName} добавлен.");
                            Console.ReadLine();
                            break;
                        }
                    case "addmenuitem":
                        {
                            Console.WriteLine("Введите название блюда:");
                            string itemName = Console.ReadLine();

                            Console.WriteLine("Введите цену блюда:");
                            int itemPrice = Convert.ToInt32(Console.ReadLine());

                            MenuItem menuItem = new MenuItem(itemName, itemPrice);
                            restaurant.AddMenuItem(menuItem);
                            Console.WriteLine($"Блюдо {itemName} добавлено в меню.");
                            Console.ReadLine();
                            break;
                        }
                    case "showemloyees":
                        {
                            if (restaurant.Employees.Count == 0)
                            {
                                Console.WriteLine("Сотрудников нет.");
                                Console.ReadLine();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("--- Список сотрудников ---");
                                foreach (var emp in restaurant.Employees)
                                {
                                    Console.WriteLine($"Имя: {emp.Name}, Номер: {emp.Number}, Зарплата: {emp.Salary}");
                                }
                                Console.ReadLine();
                                break;
                            }
                        }
                    case "exit":
                        {
                            Console.WriteLine($"Общая прибыль ресторана: {TotalBenefit}");
                            Console.ReadLine();
                            isOpen = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Неизвестная команда. Пожалуйста, попробуйте снова.");
                            Console.ReadLine();
                            break;
                        }
                }
                Console.Clear();
            }
        }
        public class Person
        {
            public string Name;
            public int Number;

            public Person(string name, int number)
            {
                Name = name;
                Number = number;
            }
        }

        public class Employee : Person
        {
            public int Salary;

            public Employee(string name, int number, int salary) : base(name, number)
            {
                Salary = salary;
            }
        }

        public class MenuItem
        {
            public string Name;
            public int Price;

            public MenuItem(string name, int price)
            {
                Name = name;
                Price = price;
            }
        }

        public class Order
        {
            public List<MenuItem> Items = new List<MenuItem>();
            public int TotalPrice = 0;

            public void AddItem(MenuItem item)
            {
                Items.Add(item);
                TotalPrice += item.Price;
            }
        }

        public class Restaurant
        {
            public string Name;

            public List<Employee> Employees = new List<Employee>();
            public List<MenuItem> Menu = new List<MenuItem>();
            public List<Order> Orders = new List<Order>();

            public Restaurant(string name)
            {
                Name = name;
            }

            public void AddMenuItem(MenuItem item)
            {
                Menu.Add(item);
            }

            public void PlaceOrder(Order order)
            {
                Orders.Add(order);
            }

            public void HireEmployee(Employee employee)
            {
                Employees.Add(employee);
            }
        }
    }
}
