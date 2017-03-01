using System;

namespace Chapter2_Objective1
{
    public struct Nullable<T> where T : struct
    {
        private bool hasValue;
        private T value;

        public Nullable(T value)
        {
            hasValue = true;
            this.value = value;
        }

        public T Value
        {
            get
            {
                if (!hasValue)
                {
                    throw new ArgumentException();
                }

                return value;
            }
        }

        public T GetValueOrDefault()
        {
            if (hasValue)
            {
                return value;
            }
            else
            {
                return default(T);
            }
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
