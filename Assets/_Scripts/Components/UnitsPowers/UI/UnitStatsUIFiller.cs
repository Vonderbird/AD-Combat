using System;
using ADC.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ADC
{
    [Serializable]
    public class EquipmentUIs
    {
        [SerializeField] public TextMeshProUGUI weaponTitle;
        [SerializeField] public Image weaponIcon;
        [SerializeField] public Image strongAgainst1Icon;
        [SerializeField] public TextMeshProUGUI strongAgainst1Name;
        [SerializeField] public Image strongAgainst2Icon;
        [SerializeField] public TextMeshProUGUI strongAgainst2Name;
        [SerializeField] public Image weakAgainst1Icon;
        [SerializeField] public TextMeshProUGUI weakAgainst1Name;
        [SerializeField] public Image weakAgainst2Icon;
        [SerializeField] public TextMeshProUGUI weakAgainst2Name;

        public void SetInfo(IEquipmentUIInfo uiInfo)
        {
            weaponTitle.text = uiInfo.Title;
            weaponIcon.sprite = uiInfo.Icon;
            strongAgainst1Icon.sprite = uiInfo.StrongAgainst1Icon;
            strongAgainst1Name.text = uiInfo.StrongAgainst1Title;
            strongAgainst2Icon.sprite = uiInfo.StrongAgainst2Icon;
            strongAgainst2Name.text = uiInfo.StrongAgainst2Title;
            weakAgainst1Icon.sprite = uiInfo.WeakAgainst1Icon;
            weakAgainst1Name.text = uiInfo.WeakAgainst1Title;
            weakAgainst2Icon.sprite = uiInfo.WeakAgainst2Icon;
            weakAgainst2Name.text = uiInfo.WeakAgainst2Title;
        }
    }

    public class UnitStatsUIFiller: MonoBehaviour
    {
        [SerializeField] private EquipmentUIs weaponUIInfo;
        [SerializeField] private EquipmentUIs shieldUIInfo;
        public void SetAll(UnitUIInfo uiInfo)
        {
            SetTitle(uiInfo.Title);
            SetBanner(uiInfo.UnitBanner);
            weaponUIInfo.SetInfo(uiInfo.Weapon);
            shieldUIInfo.SetInfo(uiInfo.Shield);
            SetSpecialAbilityIcons(uiInfo.SpecialAbilityIcons);
        }

        [SerializeField] private TextMeshProUGUI titleText;
        public void SetTitle(string title)
        {
            titleText.text = title;
        }

        [SerializeField] private Image bannerImage;
        public void SetBanner(Sprite banner)
        {
            bannerImage.sprite = banner;
        }

        //[SerializeField]
        public void SetSpecialAbilityIcons(Sprite[] icons)
        {

        }
    }
}