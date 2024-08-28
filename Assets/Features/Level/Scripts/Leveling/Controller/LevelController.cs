using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] TypeLevelScriptable gameTypeLevel;
    [SerializeField] MoveScene moveScene;

    private void Awake()
    {
        var filterGameMode = gameTypeLevel.gameMode.Find((value) => value.gameName == GameModeData.gameName);
        filterGameMode.levelItemModel.ForEach((value) =>
        {
            var instantiateGO = Instantiate(gameTypeLevel.levelGO, transform);
            instantiateGO.image.sprite = value.image;
            instantiateGO.levelText.text = value.level.ToString();
            instantiateGO.button.onClick.AddListener(() =>
            {
                GameModeData.level = value.level;
                moveScene.MoveToScene(value.sceneName);
            });
        });
    }
}