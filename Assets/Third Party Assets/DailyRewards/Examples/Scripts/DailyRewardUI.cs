/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;

/* 
 * Daily Reward Object UI representation
 */
namespace NiobiumStudios
{
    /** 
     * The UI Representation of a Daily Reward.
     * 
     *  There are 3 states:
     *  
     *  1. Unclaimed and available:
     *  - Shows the Color Claimed
     *  
     *  2. Unclaimed and Unavailable
     *  - Shows the Color Default
     *  
     *  3. Claimed
     *  - Shows the Color Claimed
     *  
     **/
    public class DailyRewardUI : MonoBehaviour
    {
        public bool showRewardName;

        [Header("UI Elements")]
        public Text textDay;                // Text containing the Day text eg. Day 12
        public Text textReward;             // The Text containing the Reward amount
        public Image imageRewardBackground; // The Reward Image Background
        public Image imageReward;           // The Reward Image
        public Color colorClaim;            // The Color of the background when claimed
        public Sprite Outline, Claimed;
        private Color colorUnclaimed;       // The Color of the background when not claimed


        [Header("Internal")]
        public int day;

        [HideInInspector]
        public Reward reward;

        public DailyRewardState state;

        // The States a reward can have
        public enum DailyRewardState
        {
            UNCLAIMED_AVAILABLE,
            UNCLAIMED_UNAVAILABLE,
            CLAIMED
        }

        void Awake()
        {
            colorUnclaimed = imageReward.color;
        }

        public void Initialize()
        {
            textDay.text = string.Format("Day {0}", day.ToString());
            if (reward.reward > 0)
            {
                if (showRewardName)
                {
                    textReward.text = reward.reward + " " + reward.unit;
                }
                else
                {
                    textReward.text = reward.reward.ToString();
                }
            }
            else
            {
                textReward.text = reward.unit.ToString();
            }
            imageReward.sprite = reward.sprite;
        }

        // Refreshes the UI
        public void Refresh()
        {
            switch (state)
            {
                case DailyRewardState.UNCLAIMED_AVAILABLE:
                    //imageRewardBackground.gameObject.SetActive(true);
                    Color col1 = new Color(imageRewardBackground.color.r, imageRewardBackground.color.g, imageRewardBackground.color.b, 1f);
                    imageRewardBackground.color = col1;
                    imageRewardBackground.sprite = Outline;
                    //.color = colorClaim;
                    gameObject.GetComponent<Button>().interactable = true;
                    break;
                case DailyRewardState.UNCLAIMED_UNAVAILABLE:
                    //imageRewardBackground.gameObject.SetActive(false);
                    Color col = new Color(imageRewardBackground.color.r, imageRewardBackground.color.g, imageRewardBackground.color.b, 0.25f);
                    imageRewardBackground.color = col;
                    gameObject.GetComponent<Button>().interactable = false;

                    //                    imageRewardBackground.color = colorUnclaimed;
                    break;
                case DailyRewardState.CLAIMED:
                    imageRewardBackground.gameObject.SetActive(true);
                    imageReward.sprite = Claimed;
                    imageReward.enabled = true;
                    gameObject.GetComponent<Button>().interactable = false;

                    //imageRewardBackground.color = colorClaim;
                    break;
            }
        }
    }
}