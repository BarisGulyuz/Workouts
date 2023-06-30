namespace Workouts.DataStructures.Queue
{
    public class MyQueue<T>
    {  
        private const int DEFAULT_SIZE = 50;

        private T[] items;

        private int count;
        private int fist;
        private int last = -1;

        public MyQueue(int size = DEFAULT_SIZE)
        {
            items = new T[size];
        }

        /// <summary>
        /// Add Last
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            if(count == items.Length)
            {
                Extend();
            }

            last++;
            items[last] = item;
            count++;
        }
        /// <summary>
        /// Get First 
        /// </summary>
        /// <param name="item"></param>
        public T Dequeue()
        {
            if(count > 0 && count == items.Length / 4)
            {
                Shrink();
            }

            T item = items[fist];
            items[fist] = default;
            fist++;
            count--;

            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        private void Extend()
        {
            Array.Resize(ref items, items.Length *2);
            fist = 0;
            last = count - 1;
        }
        /// <summary>
        /// 
        /// </summary>
        private void Shrink()
        {
            int capacity = items.Length / 2;

            T[] newItems = new T[capacity];
            Array.Copy(items, fist, newItems, 0, capacity);

            items = newItems;
            fist = 0;
            last = count - 1;
        }
    }
}
