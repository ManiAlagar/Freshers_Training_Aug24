namespace TaskOne
{
    public class StringOperation
    {
        public int StringLength(string stringValue)
        {
            return stringValue.Length;
        }

        public string UpperCase(string stringValue)
        {
            return stringValue.ToUpper();
        }

        public string LowerCase(string stringValue)
        {
            return stringValue.ToLower();
        }

        public char[] TraverseString(string stringValue)
        {
            int Length = StringLength(stringValue);
            char[] characterArray = new char[Length];
            for (int i = 0; i < Length; i++)
            {
                characterArray[i] = stringValue[i];
            }
            return characterArray;
        }

        public char[] ReverseString(string stringValue)
        {          
            int Length = StringLength(stringValue);
            char[] characterArray = new char[ Length ];
            foreach (char character in stringValue)
            {
                characterArray[ Length - 1 ] = character;
                Length--;
            }
            return characterArray;
        }
    }
}

