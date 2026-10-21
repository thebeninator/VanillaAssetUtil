# VanillaAssetUtil
```C#
Vehicle LoadVanillaVehicle(string unique_name, bool temp = false)
```
Loads a vanilla vehicle's prefab, returning its Vehicle component 

`unique_name` is the unique name of the vehicle (as seen in `Unit.UniqueName` or `UnitPrefabMetadata.Name`)

If `temp` is set to true, then the prefab will be unloaded as soon as the mission finishes loading, i.e. when `GameState.GameReady` is reached (lowest priority)

Otherwise, prefabs are mission persistent and will be unloaded when the mission is exited 
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
\
\
\
This method is meant to be used like this:

https://github.com/thebeninator/Pact-Increased-Lethality/blob/5e24f438ed447d5458af99cf1bb2bc6f9cc986b1/Vics/BMP3.cs#L746-L751

https://github.com/thebeninator/Pact-Increased-Lethality/blob/5e24f438ed447d5458af99cf1bb2bc6f9cc986b1/Vics/BMP3.cs#L253-L254
