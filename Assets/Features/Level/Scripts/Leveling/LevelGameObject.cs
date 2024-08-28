using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelGameObject : MonoBehaviour
{
    [HideInInspector] public GameObject levelGo;
    public TextMeshProUGUI levelText;
    public Image image;
    public MoveScene moveScene;
    public Button button;

    private void Awake()
    {
        levelGo = gameObject;
    }

}