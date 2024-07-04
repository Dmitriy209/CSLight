using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = -15;
            long b = long.MaxValue;

            a = (int)b;

            Console.WriteLine(a);
            
            ComputerClub computerClub = new ComputerClub(8);
            computerClub.Work();
        }
    }

    class ComputerClub
    {
        private int _money = 0;
        private List<Computer> _computers = new List<Computer>();
        private Queue<Client> _clients = new Queue<Client>();

        public ComputerClub(int computersCount)
        {
            Random random = new Random();

            for (int i = 0; i < computersCount; i++)
            {
                _computers.Add(new Computer(random.Next(5, 15)));
            }

            CreateNewClients(25, random);
        }

        public void CreateNewClients(int count, Random random)
        {
            for (int i = 0; i < count; i++)
            {
                _clients.Enqueue(new Client(random.Next(100, 251), random));
            }
        }

        public void Work()
        {
            while (_clients.Count > 0)
            {
                Client newClient = _clients.Dequeue();
                Console.WriteLine($"������ ������������� ����� {_money} ���. ���� ������ �������.");
                Console.WriteLine($"� ��� ����� ������, � �� ����� ������ {newClient.DesiredMinutes} �����.");
                ShowAllComputersState();

                Console.Write("\n�� ����������� ��� ��������� ��� �������: ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int computerNumber))
                {
                    computerNumber -= 1;

                    if (computerNumber >= 0 && computerNumber < _computers.Count)
                    {
                        if (_computers[computerNumber].IsTaken)
                        {
                            Console.WriteLine("�� ��������� �������� ������� �� ���������, ������� ��� �����. ������ ���������� � ����.");
                        }
                        else
                        {
                            if (newClient.CheckSolvency(_computers[computerNumber]))
                            {
                                Console.WriteLine("������ ���������� ������ � ��� �� ��������� " + (computerNumber + 1));
                                _money += newClient.Pay();
                                _computers[computerNumber].BecomeTaken(newClient);
                            }
                            else
                            {
                                Console.WriteLine("� ������� �� ������� ����� � �� ����.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("�� ���� �� ������ �� ����� ��������� �������� �������. �� ���������� � ����.");
                    }
                }
                else
                {
                    CreateNewClients(1, new Random());
                    Console.WriteLine("�������� ����! ��������� �����.");
                }

                Console.WriteLine("����� ������� � ���������� �������, ������� ����� �������.");
                Console.ReadKey();
                Console.Clear();
                SpendOneMinutes();
            }
        }

        private void ShowAllComputersState()
        {
            Console.WriteLine("\n������ ���� �����������:");
            for (int i = 0; i < _computers.Count; i++)
            {
                Console.Write(i + 1 + " - ");
                _computers[i].ShowState();
            }
        }

        private void SpendOneMinutes()
        {
            foreach (var computer in _computers)
            {
                computer.SpendOneMinute(); 
            }
        }
    }

    class Computer
    {
        private Client _client;
        private int _minutesRemaining;
        public bool IsTaken
        {
            get
            {
                return _minutesRemaining > 0;
            }
        }

        public int PricePerMinute { get; private set; }

        public Computer(int pricePerMinute)
        {
            PricePerMinute = pricePerMinute;
        }

        public void BecomeTaken(Client client)
        {
            _client = client;
            _minutesRemaining = _client.DesiredMinutes;
        }

        public void BecomeEmpty()
        {
            _client = null;
        }

        public void SpendOneMinute()
        {
            _minutesRemaining--;
        }

        public void ShowState()
        {
            if (IsTaken)
                Console.WriteLine($"��������� �����, �������� �����: {_minutesRemaining}");
            else
                Console.WriteLine($"��������� ��������. ���� �� ������: {PricePerMinute}");
        }
    }

    class Client
    {
        private int _money;
        private int _moneyToPay;

        public int DesiredMinutes { get; private set; }

        public Client(int money, Random random)
        {
            _money = money;
            DesiredMinutes = random.Next(10, 30);
        }

        public bool CheckSolvency(Computer computer)
        {
            _moneyToPay = DesiredMinutes * computer.PricePerMinute;
            if (_money >= _moneyToPay)
            {
                return true;
            }
            else
            {
                _moneyToPay = 0;
                return false;
            }
        }

        public int Pay()
        {
            _money -= _moneyToPay;
            return _moneyToPay;
        }
    }
}