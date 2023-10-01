namespace Workouts.RandomQuestions
{
    internal class SnailArray
    {
        enum Direction
        {
            Up,
            Rigt,
            Down,
            Left
        }
        public int[] Convert(int[,] data)
        {
            int targetArrLength = data.GetLength(0) * data.GetLength(1); // row * cols equals to new array lenght
            int[] newArr = new int[targetArrLength]; //create a new array with target lenght

            List<string> addedIndexes = new List<string>(); //create string list for check if target indexs already added the new array

            int x = 0, y = -1, index = 0, currentValue;
            void FindNext(Direction direction)
            {
                //defense
                if (addedIndexes.Count == targetArrLength)
                    return;

                SetupXandY(direction, isRollback: false);
                try
                {
                    currentValue = data[x, y]; //will throw exception if index out of range
                    if (!addedIndexes.Exists(i => i == $"{x},{y}")) //check is this index already added to newArr
                    {
                        newArr[index] = data[x, y];
                        index++;

                        addedIndexes.Add($"{x},{y}");
                        FindNext(direction);
                    }
                    else
                    {
                        SetupXandY(direction, isRollback: true);
                        //to do: should i return here?
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    SetupXandY(direction, isRollback: true);
                    return;
                }
            }

            void SetupXandY(Direction direction, bool isRollback)
            {
                if (!isRollback)
                {
                    if (direction == Direction.Up) x--;  // If the next movement is up, the x axis should decrease
                    else if (direction == Direction.Down) x++; // If the next movement is down, the x axis should increase
                    else if (direction == Direction.Rigt) y++;  //If the next movement is right, the y axis should increase
                    else if (direction == Direction.Left) y--; // If the next movement is left, the y axis should decrease
                }
                else
                {
                    /* fix x and y values ​​for failed or existing added values */
                    if (direction == Direction.Up) x++;
                    else if (direction == Direction.Rigt) y--;
                    else if (direction == Direction.Down) x--;
                    else if (direction == Direction.Left) y++;
                }
            }

            while (addedIndexes.Count < targetArrLength)
            {
                FindNext(Direction.Rigt); // try to find right 
                FindNext(Direction.Down); // try to find down 
                FindNext(Direction.Left); // try to find left 
                FindNext(Direction.Up); // try to find upper
            }

            return newArr;
        }
    }
}
