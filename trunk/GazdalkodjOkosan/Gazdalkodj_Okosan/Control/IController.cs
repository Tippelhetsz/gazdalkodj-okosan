using GazdalkodjOkosan.Model.Actions;
using GazdalkodjOkosan.Model.Game;

namespace GazdalkodjOkosan.Control
{
    public interface IController
    {
        IAction Step(int fields);
        IAction DoAction(IAction action);
        void NextPlayer(int id);
        Player CurrentPlayer { get; }
        Table Table { get; }
    }
}
