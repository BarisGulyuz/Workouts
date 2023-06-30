using Workouts.DataStructures.Queue;
using Workouts.DataStructures.Stack;

#region Queue

MyQueue<string> queue = new MyQueue<string>(10);

queue.Enqueue("Baris");
queue.Enqueue("Zeynep");

string next = queue.Dequeue();

Console.WriteLine(next); //Baris


#endregion


#region Stack

MyStack<int> stack = new MyStack<int>(10);

stack.Push(1);
stack.Push(2);

int next2 = stack.Pop();

Console.WriteLine(next2); //2


#endregion

Console.ReadLine();