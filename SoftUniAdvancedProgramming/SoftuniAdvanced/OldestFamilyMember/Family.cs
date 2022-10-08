using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefiningClasses
{
    public class Family
    {
        private List<Person> members;

        public Family()
        {
            members = new List<Person>();
        }

        public List<Person> Members { get { return members; } set { members = value; } }

        public void AddMember(string name, int age)
        {
            this.members.Add(new Person(name, age));
        }

        public Person GetOldest()
        {
            Person max = new Person(null, -1);
            foreach (var item in this.members)
            {
                if(item.Age > max.Age)
                {
                    max = item;
                }
            }
            return max;
        }
    }
}
