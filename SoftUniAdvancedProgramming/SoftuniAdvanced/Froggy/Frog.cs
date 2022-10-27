using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Froggy
{
    public class Frog<T> : IEnumerable<T>
    {
        public Frog(List<T> pond)
        {
            Pond = new List<T>(pond);
        }

        private List<T> Pond{ get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Pond.Count; i++)
            {
                if ((i + 1) % 2 == 1)
                {
                    yield return Pond[i];
                }
            }

            for (int i = Pond.Count - 1; i >= 0; i--)
            {
                if((i + 1) % 2 == 0)
                {
                    yield return Pond[i];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
