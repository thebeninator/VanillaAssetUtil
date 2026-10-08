using System.Linq;
using GHPC.State;
using MelonLoader;
using VanillaAssetUtil;
using VanillaAssetUtil.ModUtil;
using VanillaAssetUtil.AssetUtil;

[assembly: MelonPriority(-500)]
[assembly: MelonInfo(typeof(Mod), "Vanilla Asset Util", "1.0.0", "ATLAS")]
[assembly: MelonGame("Radian Simulations LLC", "GHPC")]

namespace VanillaAssetUtil
{
    public class Mod : MelonMod
    {
        private int valid_scene_count = 0;

        public override void OnSceneWasLoaded(int build_idx, string scene_name)
        {
            if (Util.menu_screens.Contains(scene_name)) return;

            valid_scene_count++;

            if (valid_scene_count == 1)
            {
                AssetPrefabReferenceDatabase.Create(build_idx);
            }

            if (valid_scene_count == 2)
            {
                AssetPrefabReferenceDatabase ref_db = AssetPrefabReferenceDatabase.Instance;

                StateController.RunOrDefer(GameState.GameReady, new GameStateEventHandler(ref_db.ReleaseTempVanillaAssetsDeferred), GameStatePriority.Low);

                valid_scene_count = 0;
            }
        }
    }
}
