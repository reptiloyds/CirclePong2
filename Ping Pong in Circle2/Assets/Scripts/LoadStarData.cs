using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadStarData : MonoBehaviour
{
    [SerializeField] private string numberOfLevel;
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite filledStar;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Level" + numberOfLevel + "StarCount"))
        {
            for (int i = 0; i < PlayerPrefs.GetInt("Level" + numberOfLevel + "StarCount"); i++)
            {
                stars[i].sprite = filledStar;
            }
        }
    }
}
