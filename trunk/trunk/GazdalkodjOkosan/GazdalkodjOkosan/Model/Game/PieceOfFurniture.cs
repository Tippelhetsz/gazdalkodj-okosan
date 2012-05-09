using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Game
{
    public enum EFurnitureType { Bicycle, VacuumCleaner, Bathroom, TableTennis, Radio, Television, Livingroom, Kitchen, Fridge };
    class PieceOfFurniture
    {
        private int price;
        private EFurnitureType type;
    }
}
