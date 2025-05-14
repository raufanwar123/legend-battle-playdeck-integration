using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelection : MonoBehaviour
{
    [Serializable]
    public class ItemsList
    {
        public string memberName;
        public GameObject teamMembers;
        public int memberPrice;
        public int memberIndex;
        public float healthValue;
        public float damageValue;
        public float fireRateValue;
        public float accuracyValue;
        public GameObject equippedImage;
    }
    public ItemsList[] itemsList;
    
    public Text _TextCoinsTotal;
    public Text memberName;
    public Text memberPrice;
    public Transform pointB;
    public float speed;
    public Image healthFiller;
    public Image damageFiller;
    public Image fireRateFiller;
    public Image accuracyFiller;

    public GameObject[] selectedIndicator;

    public Button selectBtn;
    public Button purchaseBtn;
    int selectedMember;
    void OnEnable()
    {
        UpdateCash();
        
        for (int i = 0; i < itemsList.Length; i++)
        {
            selectedIndicator[i].SetActive(false);
            itemsList[i].teamMembers.SetActive(false);
        }
        GameConfiguration.SetIntegerKeyValue("Phoniex", 1);

        selectBtn.onClick.AddListener(OnSelectBtnClick);
        purchaseBtn.onClick.AddListener(OnPurchaseBtnClick);

        selectedMember = GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_TEAM_MEMBER);
        itemsList[counter].teamMembers.SetActive(true);
        selectBtn.gameObject.SetActive(false);
        memberName.text = itemsList[counter].memberName;
        //selectedIndicator[selectedMember].SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            itemsList[i].equippedImage.SetActive(false);
        }
        itemsList[selectedMember].equippedImage.SetActive(true);
        InitItem();
    }

    void UpdateCash()
    {
        _TextCoinsTotal.text = GameConfiguration.getTotalCash().ToString();
    }
    void Start()
    {
        Transform pointA = itemsList[0].teamMembers.transform;
    }

    IEnumerator MoveObject(Transform thisTransform, Transform startPos, Transform endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos.transform.position, endPos.transform.position, i);
            yield return null;
        }
    }
    int counter = 0;
    public void SelectTeamMember(int index)
    {
        counter = index;
        for (int i = 0; i < itemsList.Length; i++)
        {
            selectedIndicator[i].SetActive(false);
            itemsList[i].teamMembers.SetActive(false);
        }
        itemsList[index].teamMembers.SetActive(true);
        selectedIndicator[index].SetActive(true);
        memberName.text = itemsList[index].memberName;

        if (!itemsList[counter].equippedImage.activeInHierarchy)
        {
            selectBtn.gameObject.SetActive(true);
        }
        InitItem();

        if (itemsList[counter].equippedImage.activeInHierarchy)

        {
            selectBtn.gameObject.SetActive(false);
        }
    }
    public void OnSelectBtnClick()
    {
        if (GameConfiguration.GetIntegerKeyValue(itemsList[counter].memberName) == 1)
        {
            print("Item Selected : " + itemsList[counter].memberName);
            GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_TEAM_MEMBER, counter);
            selectedMember = GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_TEAM_MEMBER);
            for (int i = 0; i < 3; i++)
            {
                itemsList[i].equippedImage.SetActive(false);
            }
            selectBtn.gameObject.SetActive(false);
            itemsList[counter].equippedImage.SetActive(true);
        }
    }
    public void OnPurchaseBtnClick()
    {
        int cashVal = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey);
        if (itemsList[counter].memberPrice <= cashVal)
        {
            GameConfiguration.SetIntegerKeyValue(itemsList[counter].memberName, 1);
            
            cashVal = cashVal - itemsList[counter].memberPrice;
            GameConfiguration.setTotalCash(cashVal);
            if (TopBarGameplay.Instance)
            {
                TopBarGameplay.Instance.RefreshTotalCoinsTxt();
            }
            UpdateCash();
            InitItem();
        }
    }
    private void FixedUpdate()
    {
        healthFiller.fillAmount = Mathf.Lerp(healthFiller.fillAmount, itemsList[counter].healthValue, 0.05f);
        damageFiller.fillAmount = Mathf.Lerp(damageFiller.fillAmount, itemsList[counter].damageValue, 0.05f);
        fireRateFiller.fillAmount = Mathf.Lerp(fireRateFiller.fillAmount, itemsList[counter].fireRateValue, 0.05f);
        accuracyFiller.fillAmount = Mathf.Lerp(accuracyFiller.fillAmount, itemsList[counter].accuracyValue, 0.05f);
    }
    public void InitItem()
    {
        for (int i = 0; i < itemsList.Length; i++)
        {
            Debug.Log("Counter Value " + counter);
            if (counter == i)
            {
                print("this is Initial Method"); //if (counter == 0 && GameConfiguration.GetIntegerKeyValue(itemsList[i].itemName) == 0)// default purchase of first weapon
                itemsList[i].teamMembers.SetActive(true);
                memberName.text = itemsList[i].memberName;
                memberPrice.text = itemsList[i].memberPrice.ToString();
                selectedIndicator[i].SetActive(true);

                if (GameConfiguration.GetIntegerKeyValue(itemsList[i].memberName) == 1 )
                {
                    if (!itemsList[counter].equippedImage.activeSelf)
                        selectBtn.gameObject.SetActive(true);
                    else if (itemsList[counter].equippedImage.activeSelf)
                        selectBtn.gameObject.SetActive(false);
                    
                    purchaseBtn.gameObject.SetActive(false);
                }
                else
                {
                    purchaseBtn.gameObject.SetActive(true);
                    selectBtn.gameObject.SetActive(false);
                }
            }
            else
            {
                itemsList[i].teamMembers.SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        
    }
}
