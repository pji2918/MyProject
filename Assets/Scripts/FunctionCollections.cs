namespace pji2918.Functions
{
    public static class FunctionCollections
    {
        public static bool isFull(object[] array)
        {
            foreach (var i in array)
            {
                if (i == null)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool AddItem(object[] array, object item)
        {
            if (isFull(array))
            {
                return false;
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == null)
                    {
                        array[i] = item;
                        return true;
                    }
                }

                return false;
            }

        }
    }
}
