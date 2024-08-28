using System.Collections.Generic;

public enum GameName
{
    bayangan,
    bagian
}

[System.Serializable]
public class GameModeModel
{
    public GameName gameName;
    public List<LevelItemModel> levelItemModel;
}