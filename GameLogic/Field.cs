using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Field 
    {
        public bool IsMine { get; set; }
        public bool IsExploded { get; private set; }

        public Field(bool isMine)
        {
            IsMine = isMine;
        }

        public void Explode()
        {
            IsExploded = true;
        }
    }
}
