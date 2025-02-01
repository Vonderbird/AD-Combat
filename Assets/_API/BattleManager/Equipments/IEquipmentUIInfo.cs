using System;
using ADC.API;
using UnityEngine;

namespace ADC.API
{
    public interface IEquipmentUIInfo
    {
        public string Title { get; set; }
        public Sprite Icon { get; set; }

        public string StrongAgainst1Title { get; }

        public string StrongAgainst2Title { get; }

        public string WeakAgainst1Title { get; }

        public string WeakAgainst2Title { get; }

        public Sprite StrongAgainst1Icon { get; }

        public Sprite StrongAgainst2Icon { get; }

        public Sprite WeakAgainst1Icon { get; }

        public Sprite WeakAgainst2Icon { get; }
    }

    [Serializable]
    public struct WeaponUIInfo : IEquipmentUIInfo
    {
        [SerializeField] private string title;
        [SerializeField] private Sprite icon;

        [SerializeField] private Shield strongAgainst1;
        [SerializeField] private Shield strongAgainst2;
        [SerializeField] private Shield weakAgainst1;
        [SerializeField] private Shield weakAgainst2;

        public string Title { get => title; set => title = value; }
        public Sprite Icon { get => icon; set => icon = value; }

        public string StrongAgainst1Title => strongAgainst1.UIInfo.Title;

        public string StrongAgainst2Title => strongAgainst2.UIInfo.Title;

        public string WeakAgainst1Title => weakAgainst1.UIInfo.Title;

        public string WeakAgainst2Title => weakAgainst2.UIInfo.Title;

        public Sprite StrongAgainst1Icon => strongAgainst1.UIInfo.Icon;

        public Sprite StrongAgainst2Icon => strongAgainst2.UIInfo.Icon;

        public Sprite WeakAgainst1Icon => weakAgainst1.UIInfo.Icon;

        public Sprite WeakAgainst2Icon => weakAgainst2.UIInfo.Icon;
    }


    [Serializable]
    public struct ShieldUIInfo : IEquipmentUIInfo
    {
        [SerializeField] private string title;
        [SerializeField] private Sprite icon;

        [SerializeField] private Weapon strongAgainst1;
        [SerializeField] private Weapon strongAgainst2;
        [SerializeField] private Weapon weakAgainst1;
        [SerializeField] private Weapon weakAgainst2;

        public string Title { get => title; set => title = value; }
        public Sprite Icon { get => icon; set => icon = value; }

        public string StrongAgainst1Title => strongAgainst1.UIInfo.Title;

        public string StrongAgainst2Title => strongAgainst2.UIInfo.Title;

        public string WeakAgainst1Title => weakAgainst1.UIInfo.Title;

        public string WeakAgainst2Title => weakAgainst2.UIInfo.Title;

        public Sprite StrongAgainst1Icon => strongAgainst1.UIInfo.Icon;

        public Sprite StrongAgainst2Icon => strongAgainst2.UIInfo.Icon;

        public Sprite WeakAgainst1Icon => weakAgainst1.UIInfo.Icon;

        public Sprite WeakAgainst2Icon => weakAgainst2.UIInfo.Icon;
    }
}