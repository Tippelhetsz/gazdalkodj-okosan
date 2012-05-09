using GazdalkodjOkosan.Model.Actions;
using GazdalkodjOkosan.Model.Game;

namespace GazdalkodjOkosan.Control
{
    public interface IController
    {
        /// <summary>
        /// Inicializálja a játék logikáját.
        /// </summary>
        /// <param name="players">Játékosok tömbje</param>
        void CreateGame(Player[] players);

        /// <summary>
        /// Meghatározza a következő játékost.
        /// Ha van paraméter, akkor a megadott sorszámú játékos lesz a következő, ha nincs, akkor maga határozza meg.
        /// </summary>
        /// <param name="id">A következő játékos sorszáma</param>
        void NextPlayer(int id = -1);

        /// <summary>
        /// Dobás a kockával
        /// </summary>
        /// <returns>Visszaadja a dobás után elvégezhető akciót</returns>
        IAction Roll();

        /// <summary>
        /// Elvégzi a megadott paraméter szerinti lépést.
        /// </summary>
        /// <param name="fields">Dobás eredménye (lépések száma)</param>
        /// <returns>A célmező akciója</returns>
        IAction Step(int fields);

        /// <summary>
        /// Egy akció végrehajtása
        /// </summary>
        /// <param name="action">A végrehajtandó akció</param>
        /// <returns>A végrehajtás eredményéül kapott akció</returns>
        IAction DoAction(IAction action);

        /// <summary>
        /// Megadja az aktív játékost
        /// </summary>
        Player CurrentPlayer { get; }

        /// <summary>
        /// A (logikai) játéktábla lekérése
        /// </summary>
        Table Table { get; }
    }
}
