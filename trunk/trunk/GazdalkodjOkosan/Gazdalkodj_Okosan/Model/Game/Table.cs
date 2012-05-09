using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazdalkodjOkosan.Model.Actions;
using GazdalkodjOkosan.Control;

namespace GazdalkodjOkosan.Model.Game
{
    public class Table
    {
        public Table() {
            CreateFields();
            CreateLuckyCards();
        }

        private void CreateFields()
        {
            fields = new Field[40];
            fields[0] = new Field(0, new StartField(true));
            fields[1] = new Field(1, new Fine(50, "Szemeteltél!"));
            fields[2] = new Field(2, new DrawLuckyCard());
            fields[3] = new Field(3, new SavingBookInterest(5, "Takarékoskodj!"));
            fields[4] = new Field(4, new FurnitureShop(new PieceOfFurniture[] {
                new PieceOfFurniture(6000, EFurnitureType.Television),
                new PieceOfFurniture(2000, EFurnitureType.Radio),
                new PieceOfFurniture(4000, EFurnitureType.Fridge),
                new PieceOfFurniture(5000, EFurnitureType.Bathroom),
                new PieceOfFurniture(1000, EFurnitureType.VacuumCleaner)
            }));
            fields[5] = new Field(5, new Nothing("Zárd el jól a csapot, hogy ne folyjon feleslegesen!"));
            fields[6] = new Field(6, new Fine(100, "A dohányzás káros! Vésd jobban az eszedbe!"));
            fields[7] = new Field(7, new FurnitureShop(new PieceOfFurniture[] { 
                new PieceOfFurniture(6000, EFurnitureType.Television),
                new PieceOfFurniture(2000, EFurnitureType.Radio),
                new PieceOfFurniture(4000, EFurnitureType.Fridge),
                new PieceOfFurniture(5000, EFurnitureType.Bathroom),
                new PieceOfFurniture(1000, EFurnitureType.VacuumCleaner),
                new PieceOfFurniture(25000, EFurnitureType.Livingroom),
                new PieceOfFurniture(15000, EFurnitureType.Kitchen),
                new PieceOfFurniture(2000, EFurnitureType.TableTennis),
                new PieceOfFurniture(1500, EFurnitureType.Bicycle)
            }));
            fields[8] = new Field(8, new InsuranceShop());
            fields[9] = new Field(9, new DrawLuckyCard());
            fields[10] = new Field(10, new GoTo(15));
            fields[11] = new Field(11, new PlusRolls(1));
            fields[12] = new Field(12, new Go(2));
            fields[13] = new Field(13, new FurnitureShop(new PieceOfFurniture[] { 
                new PieceOfFurniture(6000, EFurnitureType.Television),
                new PieceOfFurniture(2000, EFurnitureType.Radio),
                new PieceOfFurniture(4000, EFurnitureType.Fridge),
                new PieceOfFurniture(5000, EFurnitureType.Bathroom),
                new PieceOfFurniture(1000, EFurnitureType.VacuumCleaner),
                new PieceOfFurniture(25000, EFurnitureType.Livingroom),
                new PieceOfFurniture(15000, EFurnitureType.Kitchen),
                new PieceOfFurniture(2000, EFurnitureType.TableTennis),
                new PieceOfFurniture(1500, EFurnitureType.Bicycle)
            }, "Lakásodat berendezheted."));
            fields[14] = new Field(14, new HouseShop(30000, 40000, "Szövetkezeti lakásépítés!"));
            fields[15] = new Field(15, new DrawLuckyCard());
            fields[16] = new Field(16, new Fee(5000, "Állami Biztosító. Ha kötöttél CSÉB-biztosítást, ", (FCond)delegate(IController engine) { return engine.CurrentPlayer.Insurance == (EInsurance.Cseb | EInsurance.HomeAndCseb); }));
            fields[17] = new Field(17, new FurnitureShop(new PieceOfFurniture[] {
                new PieceOfFurniture(500, EFurnitureType.Television),
                new PieceOfFurniture(200, EFurnitureType.Radio),
                new PieceOfFurniture(500, EFurnitureType.Bathroom)
            }, "Kölcsönzési díjak:"));
            fields[18] = new Field(18, new Fine(150, "Vásárolj 100.- Ft-ért színház-, 50 Ft.- Ft-ért mozijegyeket!"));
            fields[19] = new Field(19, new Go(3, "Minden reggel tornázol!"));
            fields[20] = new Field(20, new GoTo(22));
            fields[21] = new Field(21, new PlusRolls(2, "Rendszeresen tisztálkodsz!"));
            fields[22] = new Field(22, new DrawLuckyCard());
            fields[23] = new Field(23, new PlusRolls(2, "Segíted az idős embereket!"));
            fields[24] = new Field(24, new BookShop(200, "Az olvasás hasznos időtöltés! Vegyél könyvutalványt:"));
            fields[25] = new Field(25, new Fine(200, "Szórakoztál!"));
            fields[26] = new Field(26, new HouseShop(40000, 40000, "Társasház-építés!"));
            fields[27] = new Field(27, new Go(1));
            fields[28] = new Field(28, new PlusRolls(1, "Jól megtanultad a KRESZ által előírt kötelező kerékpár-tartozékokat!"));
            fields[29] = new Field(29, new FurnitureShop(new PieceOfFurniture[] { 
                new PieceOfFurniture(25000, EFurnitureType.Livingroom),
                new PieceOfFurniture(15000, EFurnitureType.Kitchen),
                new PieceOfFurniture(2000, EFurnitureType.TableTennis),
                new PieceOfFurniture(1500, EFurnitureType.Bicycle)
            }));
            fields[30] = new Field(30, new Ban(new int[] { 1, 6 }, "Nyári táborban pihensz."));
            fields[31] = new Field(31, new DrawLuckyCard());
            fields[32] = new Field(32, new Fee(1000, "Jó helyezést értél el."));
            fields[33] = new Field(33, new GoTo(2, "Figyelmesen körülnéztél!"));
            fields[34] = new Field(34, new FurnitureShop(new PieceOfFurniture[] { 
                new PieceOfFurniture(6000, EFurnitureType.Television),
                new PieceOfFurniture(2000, EFurnitureType.Radio),
                new PieceOfFurniture(4000, EFurnitureType.Fridge),
                new PieceOfFurniture(5000, EFurnitureType.Bathroom),
                new PieceOfFurniture(1000, EFurnitureType.VacuumCleaner)
            }));
            fields[35] = new Field(35, new DrawLuckyCard());
            fields[36] = new Field(36, new Fine(50, "Múzeumi katalógust és képeslapokat vásároltál."));
            fields[37] = new Field(37, new GoTo(39, "Rejtvénypályázaton országjáró utazást nyertél."));
            fields[38] = new Field(38, new Fine(800, "Élelmiszert vásároltál."));
            fields[39] = new Field(39, new PlusRolls(1, "Hétvégi pihenésedet túrázással töltötted a szabadban."));
        }

        private void CreateLuckyCards()
        {
            luckyCards = new List<LuckyCard>();

            // todo: kártyák hozzáadása

            Random rand = new Random();
            luckyCards.OrderBy(card => rand.NextDouble());
        }

        public Field[] Fields { get { return fields; } }
        public List<LuckyCard> LuckyCards
        { 
            get {
                if (luckyCards.Count == 0) CreateLuckyCards();
                return luckyCards;
            }
        }

        private Field[] fields;
        private List<LuckyCard> luckyCards;
    }
}
