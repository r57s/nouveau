:new: nouveau
=======

- Serialise ScriptableObject classes as files.
- Only single file less than 100 lines of code.
- No external assets required.
- Easy to integrate.
- nouveau is MIT licensed.

### Example Usage

Copy `Editor/nouveau.cs` to your Plugins directory. A new menu option `Assets/Create/Scriptable Object...` will appear, from there you can create any ScriptableObject from your source code. 

### Notes

- Nouveau will only scan for ScriptableObjects in your project folder.
- Nouveau will not scan for ScriptableObjects that are in the Editor, or Unity assemblies.

## Releases
- v1.0.0 (11/01/2016)
  - Initial Release