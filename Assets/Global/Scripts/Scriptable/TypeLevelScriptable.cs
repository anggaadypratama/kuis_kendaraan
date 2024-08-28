using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TypeLevelScriptable", menuName = "TypeLevelScriptable", order = 0)]
public class TypeLevelScriptable : ScriptableObject
{
    public List<GameModeModel> gameMode;
    public LevelGameObject levelGO;
}