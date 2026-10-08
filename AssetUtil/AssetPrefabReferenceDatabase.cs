using System.Collections;
using System.Collections.Generic;
using GHPC.State;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace VanillaAssetUtil.AssetUtil
{
    public class AssetPrefabReferenceDatabase : MonoBehaviour
    {
        private List<AssetReference> LoadedAssetReferences = new List<AssetReference>();
        private List<AssetReference> TempAssetReferences = new List<AssetReference>();
        public static AssetPrefabReferenceDatabase Instance;

        void OnDestroy()
        {
            ReleaseVanillaAssets();
            ReleaseTempVanillaAssets();
        }

        public static void Create(int build_idx)
        {
            if (Instance != null) return;

            GameObject maybe_db = GameObject.Find("ASSET REF DATABASE");

            if (maybe_db != null)
            {
                Instance = maybe_db.GetComponent<AssetPrefabReferenceDatabase>();
                return;
            }

            GameObject db = new GameObject("ASSET REF DATABASE");
            Instance = db.AddComponent<AssetPrefabReferenceDatabase>();
            SceneManager.MoveGameObjectToScene(db, SceneManager.GetSceneByBuildIndex(build_idx));
        }

        public void AddReference(AssetReference prefab_ref, bool temp = false)
        {
            if (temp && !LoadedAssetReferences.Contains(prefab_ref))
            {
                TempAssetReferences.Add(prefab_ref);
            }

            if (!temp)
            {
                LoadedAssetReferences.Add(prefab_ref);

                if (TempAssetReferences.Contains(prefab_ref))
                {
                    TempAssetReferences.Remove(prefab_ref);
                }
            }
        }

        public void ReleaseTempVanillaAssets()
        {
            ReleaseAssets(TempAssetReferences, true);
        }

        public void ReleaseVanillaAssets()
        {
            ReleaseAssets(LoadedAssetReferences);
        }

        public IEnumerator ReleaseTempVanillaAssetsDeferred(GameState _)
        {
            ReleaseTempVanillaAssets();
            yield break;
        }

        private void ReleaseAssets(List<AssetReference> asset_references, bool hard_destroy = false)
        {
            foreach (AssetReference prefab in asset_references)
            {
                if (hard_destroy)
                {
                    GameObject.DestroyImmediate(prefab.Asset);
                }
                prefab.ReleaseAsset();
            }

            asset_references.Clear();
        }
    }
}
