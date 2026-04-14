using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

class DoubleNode
{
    public int data;
    public DoubleNode previous;
    public DoubleNode next;

    public DoubleNode(int value)
    {
        data = value;
        previous = null;
        next = null;
    }
}

class MyDoubleList
{
    public DoubleNode head;
    public DoubleNode tail;
    public int count = 0;

    public void AddFirst(int value)
    {
        DoubleNode newNode = new DoubleNode(value);

        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            newNode.next = head;
            head.previous = newNode;
            head = newNode;
        }

        count++;
    }

    public void AddLast(int value)
    {
        DoubleNode newNode = new DoubleNode(value);

        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail.next = newNode;
            newNode.previous = tail;
            tail = newNode;
        }

        count++;
    }

    public void AddAfter(int afterValue, int newValue)
    {
        DoubleNode temp = head;

        while (temp != null)
        {
            if (temp.data == afterValue)
            {
                DoubleNode newNode = new DoubleNode(newValue);

                newNode.next = temp.next;
                newNode.previous = temp;

                if (temp.next != null)
                {
                    temp.next.previous = newNode;
                }
                else
                {
                    tail = newNode;
                }

                temp.next = newNode;
                count++;
                return;
            }

            temp = temp.next;
        }

        Console.WriteLine("Елемент не знайдено.");
    }

    public void AddBefore(int beforeValue, int newValue)
    {
        DoubleNode temp = head;

        while (temp != null)
        {
            if (temp.data == beforeValue)
            {
                DoubleNode newNode = new DoubleNode(newValue);

                newNode.next = temp;
                newNode.previous = temp.previous;

                if (temp.previous != null)
                {
                    temp.previous.next = newNode;
                }
                else
                {
                    head = newNode;
                }

                temp.previous = newNode;
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

        if (head == tail)
        {
            head = null;
            tail = null;
        }
        else
        {
            head = head.next;
            head.previous = null;
        }

        count--;
    }

    public void RemoveLast()
    {
        if (head == null)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        if (head == tail)
        {
            head = null;
            tail = null;
        }
        else
        {
            tail = tail.previous;
            tail.next = null;
        }

        count--;
    }

    public void RemoveValue(int value)
    {
        if (head == null)
        {
            Console.WriteLine("Список порожній.");
            return;
        }

        DoubleNode temp = head;

        while (temp != null)
        {
            if (temp.data == value)
            {
                if (temp == head)
                {
                    RemoveFirst();
                    return;
                }

                if (temp == tail)
                {
                    RemoveLast();
                    return;
                }

                temp.previous.next = temp.next;
                temp.next.previous = temp.previous;
                count--;
                return;
            }

            temp = temp.next;
        }

        Console.WriteLine("Такого значення немає у списку.");
    }

    public bool Search(int value)
    {
        DoubleNode temp = head;

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

    public void PrintLeftToRight()
    {
        DoubleNode temp = head;

        while (temp != null)
        {
            Console.Write(temp.data + " <-> ");
            temp = temp.next;
        }

        Console.WriteLine("null");
    }

    public void PrintRightToLeft()
    {
        DoubleNode temp = tail;

        while (temp != null)
        {
            Console.Write(temp.data + " <-> ");
            temp = temp.previous;
        }

        Console.WriteLine("null");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        MyDoubleList list = new MyDoubleList();

        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        list.AddLast(4);

        Console.WriteLine("Початковий список зліва направо:");
        list.PrintLeftToRight();

        Console.WriteLine("Початковий список справа наліво:");
        list.PrintRightToLeft();

        list.AddFirst(10);
        Console.WriteLine("Після додавання 10 на початок:");
        list.PrintLeftToRight();

        list.AddLast(20);
        Console.WriteLine("Після додавання 20 в кінець:");
        list.PrintLeftToRight();

        list.AddAfter(2, 99);
        Console.WriteLine("Після додавання 99 після числа 2:");
        list.PrintLeftToRight();

        list.AddBefore(3, 88);
        Console.WriteLine("Після додавання 88 перед числом 3:");
        list.PrintLeftToRight();

        list.RemoveFirst();
        Console.WriteLine("Після видалення першого елемента:");
        list.PrintLeftToRight();

        list.RemoveLast();
        Console.WriteLine("Після видалення останнього елемента:");
        list.PrintLeftToRight();

        list.RemoveValue(99);
        Console.WriteLine("Після видалення числа 99:");
        list.PrintLeftToRight();

        Console.WriteLine("Пошук числа 3:");
        Console.WriteLine(list.Search(3));

        Console.WriteLine("Кількість елементів:");
        Console.WriteLine(list.count);

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
            double standardTime = 0;

            for (int i = 0; i < 5; i++)
            {
                myTime += TestMyDoubleList(size);
                standardTime += TestStandardLinkedList(size);
            }

            Console.WriteLine("Розмір: " + size);
            Console.WriteLine("Мій двозв’язний список: " + (myTime / 5) + " мс");
            Console.WriteLine("LinkedList<T>: " + (standardTime / 5) + " мс");
            Console.WriteLine();
        }
    }

    static double TestMyDoubleList(int size)
    {
        MyDoubleList list = new MyDoubleList();

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

    static double TestStandardLinkedList(int size)
    {
        LinkedList<int> list = new LinkedList<int>();

        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < size; i++)
        {
            list.AddLast(i);
        }

        list.Contains(size - 1);
        list.Remove(size - 1);

        sw.Stop();

        return sw.Elapsed.TotalMilliseconds;
    }
}