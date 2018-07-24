using httpListener.Interface;
using httpListener.БД;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpListener.Classes
{
    class ProfileData
    {
        public Profile Prof;
        public Position Pos;

        public List<Experience> Exp;

        public ProfileData()
        {
            Exp = new List<Experience>();
            this.Prof = new Profile();
            this.Pos = new Position();
            
        }
        public static implicit operator Profile(ProfileData p)
        {
         
            return p.Prof;
        }

        public static implicit operator Position(ProfileData p)
        {
            return p.Pos;
        }
    }
}
