namespace GenericScale
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class EqualityScale<T>
    {
        public EqualityScale(T left, T right)
        {
            this.left = left;
            this.right = right;
        }

        public T left;
        public T right;

        public bool AreEqual()
        {
            return left.Equals(right);
        }
    }
}
