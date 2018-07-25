using httpListener.БД;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpListener.Classes
{
    class ProfileDataMov
    {
        public ProfileMov Prof;
        public PositionMov Pos;
        public ProfileToPositionMov profToPos;
        public List<ExperienceMov> Exp;

        public ProfileDataMov()
        {
            Exp = new List<ExperienceMov>();
            this.Prof = new ProfileMov();
            this.Pos = new PositionMov();
            this.profToPos = new ProfileToPositionMov();
        }
    }
}
