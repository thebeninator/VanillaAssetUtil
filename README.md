# VanillaAssetUtil
```C#
// VanillaAssetUtil.AssetUtil.AssetUtilApi
Vehicle LoadVanillaVehicle(string unique_name, bool temp = false)
```
Loads a vanilla vehicle's prefab, returning its Vehicle component 

`unique_name` is the unique name of the vehicle (as seen in `Unit.UniqueName` or `UnitPrefabMetadata.Name`)

If `temp` is set to true, then the prefab will be unloaded as soon as the mission finishes loading

___

```C#
bool VehicleInMission(string prefab_name)
```
Returns true if the specified vanilla vehicle has been loaded, false otherwise

`prefab_name` is the name of the vehicle's prefab

___

```C#
bool VehicleInMission(string[] prefab_names)
```
Returns true if at least one of the specified vanilla vehicles has been loaded, false otherwise

`prefab_names` are the names of the vehicles' prefabs

___

```C#
GameObject CloneVanillaGameObject(string unique_name, string path)
```
Temporarily loads the specified vanilla vehicle and returns the cloned GameObject at the specified path

`unique_name` is the unique name of the vehicle (as seen in `Unit.UniqueName` or `UnitPrefabMetadata.Name`)

`path` is the prefab-relative path to the GameObject that is to be cloned
