using ADC.API;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ADC
{
    public class UnitStatsUIFiller: MonoBehaviour
    {
        public void SetAll(UnitUIInfo uiInfo)
        {
            SetTitle(uiInfo.Title);
            SetBanner(uiInfo.UnitBanner);
            SetWeaponInfo(uiInfo.Weapon);
            SetShieldInfo(uiInfo.Shield);
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

        [SerializeField] public TextMeshProUGUI weaponTitle;
        [SerializeField] public Image weaponIcon;
        public void SetWeaponInfo(WeaponUIInfo uiInfo)
        {
            weaponTitle.text = uiInfo.Title;
            weaponIcon.sprite = uiInfo.Icon;
        }

        [SerializeField] public TextMeshProUGUI shieldTitle;
        [SerializeField] public Image shieldIcon;
        public void SetShieldInfo(ShieldUIInfo uiInfo)
        {
            shieldTitle.text = uiInfo.Title;
            shieldIcon.sprite = uiInfo.Icon;
        }

        //[SerializeField]
        public void SetSpecialAbilityIcons(Sprite[] icons)
        {

        }
    }
}