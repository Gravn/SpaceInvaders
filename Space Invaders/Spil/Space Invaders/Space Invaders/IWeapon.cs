using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Invaders
{
    interface IWeapon
    {
        IWeapon Clone();

        void Fire();   
    }

    
}
