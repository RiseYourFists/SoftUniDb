using System;
using System.Collections.Generic;
using System.Text;

namespace GenericBoxOfString
{
    public class Box<T>
    {
        public Box(T value)
        {
            this.value = value;
        }
        public T value;

        public string ToString()
        {
            return $"{value.GetType()}: {value}";
        }
    }
}
