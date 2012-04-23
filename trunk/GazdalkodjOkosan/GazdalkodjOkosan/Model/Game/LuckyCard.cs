using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GazdalkodjOkosan.Model.Actions;

namespace GazdalkodjOkosan.Model.Game
{
    class LuckyCard
    {
        public LuckyCard(IAction action)
        {
            this.action = action;
        }
        public IAction Action
        {
            get { return action; }
        }

        private IAction action;
    }
}
