using System;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

class StackNode
{
    public int data;
    public StackNode next;

    public StackNode(int value)
    {
        data = value;
        next = null;
    }
}

class MyStack
{
    public StackNode top;

    public bool IsEmpty()
    {
        return top == null;
    }

    public void Push(int value)
    {
        StackNode newNode = new StackNode(value);
        newNode.next = top;
        top = newNode;
    }

    public void Pop()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Стек порожній.");
            return;
        }

        top = top.next;
    }

    public int Peek()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Стек порожній.");
            return -1;
        }

        return top.data;
    }

    public void Print()
    {
        StackNode temp = top;

        while (temp != null)
        {
            Console.Write(temp.data + " -> ");
            temp = temp.next;
        }

        Console.WriteLine("null");
    }
}

class QueueNode
{
    public int data;
    public QueueNode next;

    public QueueNode(int value)
    {
        data = value;
        next = null;
    }
}

class MyQueue
{
    public QueueNode front;
    public QueueNode rear;

    public bool IsEmpty()
    {
        return front == null;
    }

    public void Enqueue(int value)
    {
        QueueNode newNode = new QueueNode(value);

        if (rear == null)
        {
            front = newNode;
            rear = newNode;
            return;
        }

        rear.next = newNode;
        rear = newNode;
    }

    public void Dequeue()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Черга порожня.");
            return;
        }

        front = front.next;

        if (front == null)
        {
            rear = null;
        }
    }

    public int Peek()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Черга порожня.");
            return -1;
        }

        return front.data;
    }

    public void Print()
    {
        QueueNode temp = front;

        while (temp != null)
        {
            Console.Write(temp.data + " -> ");
            temp = temp.next;
        }

        Console.WriteLine("null");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("Демонстрація стеку:");
        MyStack stack = new MyStack();

        stack.Push(10);
        stack.Push(20);
        stack.Push(30);

        stack.Print();

        Console.WriteLine("Верхній елемент стеку: " + stack.Peek());

        stack.Pop();
        Console.WriteLine("Після Pop:");
        stack.Print();

        Console.WriteLine("Стек порожній: " + stack.IsEmpty());

        Console.WriteLine();
        Console.WriteLine("Демонстрація черги:");

        MyQueue queue = new MyQueue();

        queue.Enqueue(10);
        queue.Enqueue(20);
        queue.Enqueue(30);

        queue.Print();

        Console.WriteLine("Перший елемент черги: " + queue.Peek());

        queue.Dequeue();
        Console.WriteLine("Після Dequeue:");
        queue.Print();

        Console.WriteLine("Черга порожня: " + queue.IsEmpty());

        Console.WriteLine();
        Console.WriteLine("Експеримент:");
        Experiment();
    }

    static void Experiment()
    {
        int[] sizes = { 100, 1000, 5000, 10000 };

        foreach (int size in sizes)
        {
            double myStackTime = 0;
            double standardStackTime = 0;

            double myQueueTime = 0;
            double standardQueueTime = 0;

            for (int i = 0; i < 5; i++)
            {
                myStackTime += TestMyStack(size);
                standardStackTime += TestStandardStack(size);

                myQueueTime += TestMyQueue(size);
                standardQueueTime += TestStandardQueue(size);
            }

            Console.WriteLine("Розмір: " + size);

            Console.WriteLine("Мій стек: " + (myStackTime / 5) + " мс");
            Console.WriteLine("Stack<T>: " + (standardStackTime / 5) + " мс");

            Console.WriteLine("Моя черга: " + (myQueueTime / 5) + " мс");
            Console.WriteLine("Queue<T>: " + (standardQueueTime / 5) + " мс");

            Console.WriteLine();
        }
    }

    static double TestMyStack(int size)
    {
        MyStack stack = new MyStack();

        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < size; i++)
        {
            stack.Push(i);
        }

        stack.Peek();

        for (int i = 0; i < size; i++)
        {
            stack.Pop();
        }

        sw.Stop();

        return sw.Elapsed.TotalMilliseconds;
    }

    static double TestStandardStack(int size)
    {
        Stack<int> stack = new Stack<int>();

        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < size; i++)
        {
            stack.Push(i);
        }

        stack.Peek();

        for (int i = 0; i < size; i++)
        {
            stack.Pop();
        }

        sw.Stop();

        return sw.Elapsed.TotalMilliseconds;
    }

    static double TestMyQueue(int size)
    {
        MyQueue queue = new MyQueue();

        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < size; i++)
        {
            queue.Enqueue(i);
        }

        queue.Peek();

        for (int i = 0; i < size; i++)
        {
            queue.Dequeue();
        }

        sw.Stop();

        return sw.Elapsed.TotalMilliseconds;
    }

    static double TestStandardQueue(int size)
    {
        Queue<int> queue = new Queue<int>();

        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < size; i++)
        {
            queue.Enqueue(i);
        }

        queue.Peek();

        for (int i = 0; i < size; i++)
        {
            queue.Dequeue();
        }

        sw.Stop();

        return sw.Elapsed.TotalMilliseconds;
    }
}