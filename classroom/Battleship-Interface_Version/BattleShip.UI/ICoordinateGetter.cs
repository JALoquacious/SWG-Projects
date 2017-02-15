using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;

namespace BattleShip.UI
{
    public interface ICoordinateGetter
    {
        Coordinate GetCoordinate();
    }
}
