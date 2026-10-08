using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using GHPC.Mission;
using GHPC.Vehicle;
using UnityEngine.AddressableAssets;

namespace VanillaAssetUtil.AssetUtil
{   
    public class AssetUtilApi
    {
        private static UnitPrefabLookupScriptable.UnitPrefabMetadata[] lookup_all_units;
        private static List<GameObject> cloned_vanilla_assets = new List<GameObject>();

        public static Vehicle LoadVanillaVehicle(string unique_name, bool temp = false)
        {
            if (lookup_all_units == null)
            {
                lookup_all_units = Resources.FindObjectsOfTypeAll<UnitPrefabLookupScriptable>().FirstOrDefault().AllUnits;
            }

            AssetReference prefab_ref = lookup_all_units.Where(o => o.Name == unique_name).FirstOrDefault().PrefabReference;

            if (prefab_ref.Asset == null)
            {
                AssetPrefabReferenceDatabase.Instance.AddReference(prefab_ref, temp);
                return prefab_ref.LoadAssetAsync<GameObject>().WaitForCompletion().GetComponent<Vehicle>();
            }

            return (prefab_ref.Asset as GameObject).GetComponent<Vehicle>();
        }

        public static bool VehicleInMission(string prefab_name)
        {
            foreach (var unit in UnitSpawner.Instance._loadedUnits)
            {
                if (unit.Asset.name == prefab_name)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool VehicleInMission(string[] prefab_names)
        {
            foreach (var unit in UnitSpawner.Instance._loadedUnits)
            {
                if (prefab_names.Contains(unit.Asset.name))
                {
                    return true;
                }
            }

            return false;
        }

        public static GameObject CloneVanillaGameObject(string unique_name, string path)
        {
            GameObject from = LoadVanillaVehicle(unique_name, true).gameObject;
            GameObject to_clone = from.transform.Find(path).gameObject;

            to_clone.SetActive(false);
            GameObject clone = GameObject.Instantiate(to_clone);
            clone.name = clone.name.Substring(0, clone.name.Length - "(Clone)".Length);
            //cloned_vanilla_assets.Add(clone);
            to_clone.SetActive(true);

            return clone;
        }

        private static void CloneVanillaMaterial(ref Material dest, Material source)
        {
            dest = new Material(source);
        }
    }
}