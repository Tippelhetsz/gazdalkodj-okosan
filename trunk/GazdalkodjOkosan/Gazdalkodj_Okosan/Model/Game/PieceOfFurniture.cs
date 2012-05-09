using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Game
{
    public enum EFurnitureType { Bicycle, VacuumCleaner, Bathroom, TableTennis, Radio, Television, Livingroom, Kitchen, Fridge };
    public class PieceOfFurniture
    {
        Dictionary<EFurnitureType, string> names = new Dictionary<EFurnitureType, string>()
        {
            {EFurnitureType.Bathroom, "Mosógép"},
            {EFurnitureType.Bicycle, "Kerékpár"},
            {EFurnitureType.Fridge, "Hűtőgép"},
            {EFurnitureType.Kitchen, "Konyhabútor"},
            {EFurnitureType.Livingroom, "Szobabútor"},
            {EFurnitureType.Radio, "Rádió"},
            {EFurnitureType.TableTennis, "Pingpongasztal"},
            {EFurnitureType.Television, "Televízió"},
            {EFurnitureType.VacuumCleaner, "Porszívó"}
        };
        public PieceOfFurniture(int price, EFurnitureType type)
        {
            this.price = price;
            this.type = type;
        }

        public int Price { get { return price; } }
        public EFurnitureType Type { get { return type; } }
        public string Name { get { return names[type]; } }

        private int price;
        private EFurnitureType type;
    }
}
