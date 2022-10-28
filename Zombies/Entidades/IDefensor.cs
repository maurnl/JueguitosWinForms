using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombies.Entidades
{
    public interface IDefensor
    {
        bool EstaDefendiendo { get; }
        void ToggleEscudo();
    }
}
