using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    public interface IPlayer
    {
        string Name { get; set; }
        Board Board { get; set; }
        PlaceShipRequest GetPlaceShipRequest(ShipType ship);
    }
}