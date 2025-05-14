using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SubjectNerd.Utilities;

[CreateAssetMenu]
public class WeaponsData : ScriptableObject
{
    [System.Serializable]
    public class Weapons
    {
        public string weaponName;
        public string displayName;
        public string weaponSku;
        public int priceCoins;
        public bool isBought;
        public float damageValue;
        public float accuracyValue;
        public float aimValue;
        public float scopeValue;
        public float rangeValue;
    }
    [Reorderable]
    public Weapons[] weaponsList;
}