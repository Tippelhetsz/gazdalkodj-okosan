using GazdalkodjOkosan.Model.Actions;

namespace GazdalkodjOkosan.Control
{
    interface IController
    {
        int Roll();
        IAction Step(int fields);
        IAction DoAction(IAction action);
        void NextPlayer();
    }
}
