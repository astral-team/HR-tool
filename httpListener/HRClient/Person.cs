using System.Collections.Generic;

namespace HRClient
{
    public class Person
    {
        public Profile Prof;
        public Position Pos;

        public List<Experience> Exp;

        public Person()
        {
            this.Prof = new Profile();
            this.Exp = new List<Experience>();
            this.Pos = new Position();
    }
    }
}
