﻿using GazdalkodjOkosan.Model.Actions;
using GazdalkodjOkosan.Model.Game;

namespace GazdalkodjOkosan.Control
{
    interface IController
    {
        int Roll();
        IAction Step(int fields);
        IAction DoAction(IAction action);
        void NextPlayer();
        Player CurrentPlayer { get; }
        Table Table { get; }
    }
}
