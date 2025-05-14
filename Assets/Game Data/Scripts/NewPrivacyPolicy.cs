using UnityEngine;

public class NewPrivacyPolicy : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("OnlyOnce") == 1)
        {
            gameObject.SetActive(false);
        }
        PlayerPrefs.SetInt("OnlyOnce", 1);
    }
    public void OpenPrivacyLink()
    {
        Application.OpenURL(MainMenuController.instance.privacyLink);
        gameObject.SetActive(false);
    }
}
