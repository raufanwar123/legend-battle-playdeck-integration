/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
//using Unity.Notifications.Android;

namespace NiobiumStudios
{
    /**
     * The UI Logic Representation of the Daily Rewards
     **/
    public class DailyRewardsInterface : MonoBehaviour
    {

        public Reward[] NewRewards;

        public Canvas canvas;
        public GameObject[] dailyRewardPrefab;        // Prefab containing each daily reward

        [Header("Panel Debug")]
		public bool isDebug;
        public GameObject panelDebug;
		public Button buttonAdvanceDay;
		public Button buttonAdvanceHour;
		public Button buttonReset;
		public Button buttonReloadScene;

        [Header("Panel Reward Message")]
        public GameObject panelReward;              // Rewards panel
        public Text textReward;                     // Reward Text to show an explanatory message to the player
        public Button buttonCloseReward;            // The Button to close the Rewards Panel
        public Image imageReward;                   // The image of the reward
        public Text RewardPanelText;

        [Header("Panel Reward")]
        public Button buttonClaim;                  // Claim Button
        public Button buttonClose;                  // Close Button
        public Button buttonCloseWindow;            // Close Button on the upper right corner
        public Text textTimeDue;                    // Text showing how long until the next claim
        public GridLayoutGroup dailyRewardsGroup;   // The Grid that contains the rewards
        public ScrollRect scrollRect;               // The Scroll Rect

        [HideInInspector]
        public bool readyToClaim;                  // Update flag
        private List<DailyRewardUI> dailyRewardsUI = new List<DailyRewardUI>();

		private DailyRewards dailyRewards;			// DailyReward Instance      

        void Awake()
        {
            canvas.gameObject.SetActive(false);
			dailyRewards = GetComponent<DailyRewards>();

            dailyRewards.rewards.Clear();

            for (int i = 0; i < NewRewards.Length; i++)
            {
                dailyRewards.rewards.Add(NewRewards[i]);
            }
        }

        void Start()
        {
            InitializeDailyRewardsUI();
            SnapToReward();
            if (panelDebug)
                panelDebug.SetActive(isDebug);

            buttonClose.gameObject.SetActive(false);
            buttonClaim.gameObject.SetActive(false);
            buttonClaim.onClick.AddListener(() =>
            {
                //dailyRewards.ClaimPrize();
                //readyToClaim = false;
                //UpdateUI();
            });

            buttonCloseReward.onClick.AddListener(() =>
            {
				var keepOpen = dailyRewards.keepOpen;
                panelReward.SetActive(false);
              //  canvas.gameObject.SetActive(false);
                //my addition
                
               // GameConfiguration.SetIntegerKeyValue(GameConfiguration.CashKey, GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey) + PlayerPrefs.GetInt("MyReward"));
                //Debug.Log("Cash Added: " + GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey));
               // canvas.transform.parent.gameObject.SetActive(false);
                //my addition
            });

            buttonClose.onClick.AddListener(() =>
            {
                canvas.gameObject.SetActive(false);
                
            });

            buttonCloseWindow.onClick.AddListener(() =>
            {
                canvas.gameObject.SetActive(false);
                
            });

            // Simulates the next Day
            if (buttonAdvanceDay)
				buttonAdvanceDay.onClick.AddListener(() =>
				{
                    dailyRewards.debugTime = dailyRewards.debugTime.Add(new TimeSpan(1, 0, 0, 0));
                    UpdateUI();
				});

			// Simulates the next hour
			if(buttonAdvanceHour)
				buttonAdvanceHour.onClick.AddListener(() =>
              	{
                      dailyRewards.debugTime = dailyRewards.debugTime.Add(new TimeSpan(1, 0, 0));
                      UpdateUI();
				});

			if(buttonReset)
				// Resets Daily Rewards from Player Preferences
				buttonReset.onClick.AddListener(() =>
				{
					dailyRewards.Reset();
                    dailyRewards.debugTime = new TimeSpan();
                    dailyRewards.lastRewardTime = System.DateTime.MinValue;
					readyToClaim = false;
				});
			if(buttonReloadScene)
				// Reloads the same scene
				buttonReloadScene.onClick.AddListener(() =>
				{
					Application.LoadLevel(Application.loadedLevelName);
				});

            RegisterNotifications();
  
            UpdateUI();
        }

        void RegisterNotifications()
        {
            //var c = new AndroidNotificationChannel()
            //{
            //    Id = "channel_id",
            //    Name = "Default Channel",
            //    Importance = Importance.High,
            //    Description = "Generic notifications",
            //};
            //AndroidNotificationCenter.RegisterNotificationChannel(c);
        }

        RectTransform rectTransform;
        void OnEnable()
        {
            rectTransform = dailyRewardsGroup.GetComponent<RectTransform>();
            dailyRewards.onClaimPrize += OnClaimPrize;
            dailyRewards.onInitialize += OnInitialize;
        }

        void OnDisable()
        {
            if (dailyRewards != null)
            {
                dailyRewards.onClaimPrize -= OnClaimPrize;
                dailyRewards.onInitialize -= OnInitialize;
            }
        }

        // Initializes the UI List based on the rewards size
        private void InitializeDailyRewardsUI()
        {
            for (int i = 0; i < dailyRewards.rewards.Count; i++)
            {
                int day = i + 1;
                var reward = dailyRewards.GetReward(day);

                GameObject dailyRewardGo = dailyRewardPrefab[i]; //GameObject.Instantiate(dailyRewardPrefab[i]) as GameObject;

                DailyRewardUI dailyRewardUI = dailyRewardGo.GetComponent<DailyRewardUI>();
               // dailyRewardUI.transform.SetParent(dailyRewardsGroup.transform);
                //dailyRewardGo.transform.localScale = Vector2.one;

                dailyRewardUI.day = day;
                dailyRewardUI.reward = reward;

                dailyRewardUI.Initialize();
               
                dailyRewardsUI.Add(dailyRewardUI);
            }
        }

        public void UpdateUI()
        {
            dailyRewards.CheckRewards();

            bool isRewardAvailableNow = false;

            var lastReward = dailyRewards.lastReward;
            var availableReward = dailyRewards.availableReward;

            foreach (var dailyRewardUI in dailyRewardsUI)
            {
                var day = dailyRewardUI.day;

                if (day == availableReward)
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_AVAILABLE;
                    dailyRewardUI.GetComponent<Button>().interactable = true;
                    dailyRewardUI.GetComponent<Button>().onClick.AddListener(()=>
                    {
                        dailyRewards.ClaimPrize();
                        readyToClaim = false;
                        UpdateUI();
                    }
                        );

                    isRewardAvailableNow = true;
                }
                else if (day <= lastReward)
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.CLAIMED;

                }
                else
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_UNAVAILABLE;
                }

                dailyRewardUI.Refresh();
            }

            //buttonClaim.gameObject.SetActive(isRewardAvailableNow);
            buttonClose.gameObject.SetActive(!isRewardAvailableNow);
            if (isRewardAvailableNow)
            {
                SnapToReward();
                textTimeDue.text = "You can claim your reward!";
            }
            readyToClaim = isRewardAvailableNow;
        }

        // Snap to the next reward
        public void SnapToReward()
        {
            Canvas.ForceUpdateCanvases();

            var lastRewardIdx = dailyRewards.lastReward;

            // Scrolls to the last reward element
            if (dailyRewardsUI.Count - 1 < lastRewardIdx)
                lastRewardIdx++;

			if(lastRewardIdx > dailyRewardsUI.Count - 1)
				lastRewardIdx = dailyRewardsUI.Count - 1;

            var target = dailyRewardsUI[lastRewardIdx].GetComponent<RectTransform>();

            var content = scrollRect.content;

            //content.anchoredPosition = (Vector2)scrollRect.transform.InverseTransformPoint(content.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

            float normalizePosition = (float)target.GetSiblingIndex() / (float)content.transform.childCount;
            scrollRect.verticalNormalizedPosition = normalizePosition;
        }

        void Update()
        {
            dailyRewards.TickTime();
            // Updates the time due
            CheckTimeDifference();
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, 0, rectTransform.localPosition.z);
        }

        private void CheckTimeDifference ()
        {
            if (!readyToClaim)
            {
                TimeSpan difference = dailyRewards.GetTimeDifference();

                // If the counter below 0 it means there is a new reward to claim
                if (difference.TotalSeconds <= 0)
                {
                    readyToClaim = true;
                    UpdateUI();
                    SnapToReward();
                    canvas.gameObject.SetActive(false);
                    return;
                }

                string formattedTs = dailyRewards.GetFormattedTime(difference);

                textTimeDue.text = string.Format("Come back in {0} for your next reward", formattedTs);
            }
        }

        // Delegate
        private void OnClaimPrize(int day)
        {
            panelReward.SetActive(true);

            var reward = dailyRewards.GetReward(day);
            var unit = reward.unit;
            var rewardQt = reward.reward;
            imageReward.sprite = reward.sprite;
            if (rewardQt > 0)
            {
                textReward.text = string.Format("You got {0} {1}!", reward.reward, unit);
                RewardPanelText.text = string.Format("Congratulation !  You got {0} {1}!", reward.reward, unit);
            }
            else
            {
                textReward.text = string.Format("Congratulation !  You got {0}!", unit);
                RewardPanelText.text = string.Format("Congratulation !  You got {0}!", unit);

            }

            CallLocalNotification();

        }


        void CallLocalNotification()
        {
            //var notification = new AndroidNotification();
            //notification.Title = "Daily Rewards!!";
            //notification.Text = "Come back to claim your reward now";
            //notification.LargeIcon = "daily_rewards";
            //notification.FireTime = System.DateTime.Now.AddHours(24);

            //AndroidNotificationCenter.SendNotification(notification, "channel_id");
        }


        private void OnInitialize(bool error, string errorMessage)
        {
            if (!error)
            {
                var showWhenNotAvailable = dailyRewards.keepOpen;
                var isRewardAvailable = dailyRewards.availableReward > 0;

                UpdateUI();
                canvas.gameObject.SetActive(false);//showWhenNotAvailable || (!showWhenNotAvailable && isRewardAvailable));

                SnapToReward();
                CheckTimeDifference();
            }
        }
    }
}