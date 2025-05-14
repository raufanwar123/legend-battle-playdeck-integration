using UnityEngine;
using UnityEngine.Networking;

public class RateUsPanelNew : MonoBehaviour
{
	 public string email;
	 public string subject;
	 public string body;

	 public GameObject thankYouPanel, panelBG;

	 public GameObject[] hollowStars, fillStars;
	 public GameObject yesBTN, noBTN;

	 [Header("ReadOnly")]
	 public int totalStars;

	 private void Awake()
	 {
		  //yesBTN.SetActive(false);
		  //noBTN.SetActive(false);
		  Disble_FillStars();
	 }

	 private void Disble_FillStars()
	 {
		  for (int i = 0; i < fillStars.Length; i++)
		  {
			   fillStars[i].SetActive(false);
		  }
	 }

	 public void SendEmail()
	 {
		  Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
	 }
	 string MyEscapeURL(string url)
	 {
		  return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
	 }

	 private void Enable_Star_Fill_Image(int num)
	 {
		  totalStars = num + 1;
		  yesBTN.SetActive(true);
		  noBTN.SetActive(true);
		  for (int i = 0; i < num + 1; i++)
		  {
			   fillStars[i].SetActive(true);
		  }
	 }

	 public void Check_Star_Count(int num)
	 {
		  Disble_FillStars();
		  switch (num)
		  {
			   case 0:
					Enable_Star_Fill_Image(0);
					gameObject.SetActive(false);

					break;
			   case 1:
					Enable_Star_Fill_Image(1);
					gameObject.SetActive(false);

					break;
			   case 2:
					Enable_Star_Fill_Image(2);
					gameObject.SetActive(false);

					break;
			   case 3:
					Enable_Star_Fill_Image(3);
					gameObject.SetActive(false);

					Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);

					break;
			   case 4:
					Enable_Star_Fill_Image(4);
					gameObject.SetActive(false);

					Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);

					break;
		  }
	 }
	 public bool OffParent;
	 public void YesBtn()
	 {
		  //Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
		  Application.OpenURL("market://details?id=" + Application.identifier);
		  PlayerPrefs.SetInt("rated", 1);
		  if (panelBG)
			   panelBG.SetActive(true);
		  gameObject.SetActive(false);
	 }

	 public void NoBtn()
	 {

	 }

	 public void SendEmail_Or_Open_PlayStore()
	 {
		  if (totalStars == 1 || totalStars == 2 || totalStars == 3)
		  {
			   //SendEmail();
			   if (thankYouPanel)
					thankYouPanel.SetActive(true);
		  }
		  else if (totalStars == 4 || totalStars == 5)
		  {
			   Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
			   if (!OffParent)
			   {
					gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
					gameObject.transform.parent.gameObject.SetActive(false);
			   }
			   else
			   {
					gameObject.SetActive(false);
			   }
			   PlayerPrefs.SetInt("rated", 1);
			   if (panelBG)
					panelBG.SetActive(true);

		  }
	 }
}
