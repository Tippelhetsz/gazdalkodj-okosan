using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazdalkodjOkosan.Model.Game;

namespace GazdalkodjOkosan.Model.Actions
{
    class FurnitureShop : IAction
    {
        public FurnitureShop(PieceOfFurniture[] furniture, string message = "") {
            this.furniture = furniture;
            this.message = message;
        }

        public string Message
        {
            get {
                string ret = message;
                if (ret != "") ret += "\n";

                for (int i = 0; i < furniture.Length; i++) {
                    ret += furniture[i].Name + "\t" + furniture[i].Price + ".- Ft\n";
                }

                return ret;
            }
        }

        public bool Cond(Control.IController engine)
        {
            return true;
        }

        public IAction Do(Control.IController engine)
        {
            return new Nothing();
        }

        //
        private PieceOfFurniture[] furniture;
        private string message;
    }
}
