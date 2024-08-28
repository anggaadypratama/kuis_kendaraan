using UnityEngine.UI;
using UnityEngine;

public class BelajarListController : MonoBehaviour
{
    public Sprite[] belajarPrefabs;
    public Image belajarImage;

    public int currentBelajarIndex = 0;


    public void OnLeftButtonClick()
    {
        currentBelajarIndex--;
        if (currentBelajarIndex < 0)
        {
            currentBelajarIndex = belajarPrefabs.Length - 1;
        }
        belajarImage.sprite = belajarPrefabs[currentBelajarIndex];
    }

    public void OnRightButtonClick()
    {
        currentBelajarIndex++;
        if (currentBelajarIndex >= belajarPrefabs.Length)
        {
            currentBelajarIndex = 0;
        }
        belajarImage.sprite = belajarPrefabs[currentBelajarIndex];
    }
}