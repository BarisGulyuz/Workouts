namespace Workouts.DataStructures.Stack;
public class MyStack<T>
{
    private const int DEFAULT_SIZE = 50;
    private T[] items;
    private int top = -1;

    public MyStack(int size = DEFAULT_SIZE)
    {
        items = new T[size];
    }

    public void Push(T item)
    {
        if (top == items.Length - 1)
        {
            Extend();
        }

        items[++top] = item;
    }
    public T Pop()
    {
        T item = items[top];
        items[top--] = default;

        if (top > 0 && top == items.Length / 4)
        {
            Shrink();
        }

        return item;
    }
    private void Extend()
    {
        T[] newItems = new T[items.Length * 2];
        Array.Copy(items, newItems, items.Length);
        items = newItems;
    }
    private void Shrink()
    {
        T[] newItems = new T[items.Length / 2];
        Array.Copy(items, 0, newItems, 0, top + 1);
        items = newItems;
    }
}

