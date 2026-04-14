using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

class Node
{
    public int data;
    public Node next;

    public Node(int value)
    {
        data = value;
        next = null;
    }
}

class MyList
{
    public Node head;
    public int count = 0;

    public void AddFirst(int value)
    {
        Node newNode = new Node(value);
        newNode.next = head;
        head = newNode;
        count++;
    }

    public void AddLast(int value)
    {
        Node newNode = new Node(value);

        if (head == null)
        {
            head = newNode;
            count++;
            return;
        }

        Node temp = head;

        while (temp.next != null)
        {
            temp = temp.next;
        }

        temp.next = newNode;
        count++;
    }

    public void AddAfter(int afterValue, int newValue)
    {
        Node temp = head;

        while (temp != null)
        {
            if (temp.data == afterValue)
            {
                Node newNode = new Node(newValue);
                newNode.next = temp.next;
                temp.next = newNode;
                count++;
                return;
            }

            temp = temp.next;
        }

        Console.WriteLine("Елемент не знайдено.");
    }

    public void RemoveFirst()
    {
        if (head == null)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        head = head.next;
        count--;
    }

    public void RemoveLast()
    {
        if (head == null)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        if (head.next == null)
        {
            head = null;
            count--;
            return;
        }

        Node temp = head;

        while (temp.next.next != null)
        {
            temp = temp.next;
        }

        temp.next = null;
        count--;
    }

    public void RemoveValue(int value)
    {
        if (head == null)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        if (head.data == value)
        {
            head = head.next;
            count--;
            return;
        }

        Node temp = head;

        while (temp.next != null)
        {
            if (temp.next.data == value)
            {
                temp.next = temp.next.next;
                count--;
                return;
            }

            temp = temp.next;
        }

        Console.WriteLine("Такого значення немає у списку.");
    }

    public bool Search(int value)
    {
        Node temp = head;

        while (temp != null)
        {
            if (temp.data == value)
            {
                return true;
            }

            temp = temp.next;
        }

        return false;
    }

    public void Print()
    {
        Node temp = head;

        while (temp != null)
        {
            Console.Write(temp.data + " -> ");
            temp = temp.next;
        }

        Console.WriteLine("null");
    }

    public void ReplaceNegativeWithZero()
    {
        Node temp = head;

        while (temp != null)
        {
            if (temp.data < 0)
            {
                temp.data = 0;
            }

            temp = temp.next;
        }
    }

    public void AddAfterPositive(int value)
    {
        Node temp = head;

        while (temp != null)
        {
            if (temp.data > 0)
            {
                Node newNode = new Node(value);
                newNode.next = temp.next;
                temp.next = newNode;
                count++;

                temp = newNode.next;
            }
            else
            {
                temp = temp.next;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        MyList list = new MyList();

        list.AddLast(1);
        list.AddLast(-2);
        list.AddLast(3);
        list.AddLast(-4);
        list.AddLast(5);

        Console.WriteLine("Початковий список:");
        list.Print();

        list.AddFirst(10);
        Console.WriteLine("Після додавання на початок:");
        list.Print();

        list.AddAfter(3, 99);
        Console.WriteLine("Після додавання 99 після числа 3:");
        list.Print();

        list.RemoveFirst();
        Console.WriteLine("Після видалення першого елемента:");
        list.Print();

        list.RemoveLast();
        Console.WriteLine("Після видалення останнього елемента:");
        list.Print();

        list.RemoveValue(99);
        Console.WriteLine("Після видалення числа 99:");
        list.Print();

        Console.WriteLine("Пошук числа 3:");
        Console.WriteLine(list.Search(3));

        Console.WriteLine("Кількість елементів:");
        Console.WriteLine(list.count);

        list.ReplaceNegativeWithZero();
        Console.WriteLine("Після заміни від’ємних чисел на 0:");
        list.Print();

        list.AddAfterPositive(100);
        Console.WriteLine("Після додавання 100 після кожного додатного:");
        list.Print();

        Console.WriteLine();
        Console.WriteLine("Експеримент:");
        Experiment();
    }

    static void Experiment()
    {
        int[] sizes = { 100, 1000, 5000, 10000 };

        foreach (int size in sizes)
        {
            double myTime = 0;
            double listTime = 0;

            for (int i = 0; i < 5; i++)
            {
                myTime += TestMyList(size);
                listTime += TestStandardList(size);
            }

            Console.WriteLine("Розмір: " + size);
            Console.WriteLine("Мій список: " + (myTime / 5) + " мс");
            Console.WriteLine("List<T>: " + (listTime / 5) + " мс");
            Console.WriteLine();
        }
    }

    static double TestMyList(int size)
    {
        MyList list = new MyList();

        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < size; i++)
        {
            list.AddLast(i);
        }

        list.Search(size - 1);
        list.RemoveValue(size - 1);

        sw.Stop();

        return sw.Elapsed.TotalMilliseconds;
    }

    static double TestStandardList(int size)
    {
        List<int> list = new List<int>();

        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < size; i++)
        {
            list.Add(i);
        }

        list.Contains(size - 1);
        list.Remove(size - 1);

        sw.Stop();

        return sw.Elapsed.TotalMilliseconds;
    }
}