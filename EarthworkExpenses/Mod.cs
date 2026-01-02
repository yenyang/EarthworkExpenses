using Colossal.Logging;
using EarthworkExpenses.Systems;
using Game;
using Game.Modding;
using Game.SceneFlow;

namespace EarthworkExpenses
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger($"{nameof(EarthworkExpenses)}.{nameof(Mod)}").SetShowsErrorsInUI(false);

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info(nameof(OnLoad));

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");

            updateSystem.UpdateAt<TempBrushSystem>(SystemUpdatePhase.Modification4);
        }

        public void OnDispose()
        {
            log.Info(nameof(OnDispose));
        }
    }
}
