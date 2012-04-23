using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazdalkodjOkosan.Model.Actions;

namespace GazdalkodjOkosan.Model.Game
{
    class Field
    {
        public Field(int id, IAction action)
        {
            this.action = action;
        }
        public IAction Action
        {
            get;
        }

        private int FieldID;
        private IAction action;
    }
}
