

using System.Collections;
using RTSEngine;
using RTSEngine.Game;
using TMPro;
using UnityEngine;

public class CurrencyUIText : CurrencyVisualizer
{
    [SerializeField] private TextMeshProUGUI textUI;

    private Coroutine updater;

    private void OnEnable()
    {
        if (textUI == null)
            textUI = GetComponent<TextMeshProUGUI>();
        if (textUI == null)
        {
            Debug.LogError($"[CurrencyUIText] {transform.parent.name} > {name}: textUI is not assigned!");
            //return;
        }
        //updater = StartCoroutine(UpdateUI());
    }

    //private void OnDisable()
    //{
    //    if (updater != null)
    //        StopCoroutine(updater);
    //}

    //IEnumerator UpdateUI()
    //{
    //    while (true)
    //    {
    //        textUI.text = $"{0:F1}";
    //        yield return null;
    //    }
    //}


    public override void Refresh<T>(CurrencyChangeEventArgs<T> args)
    {
        if (!RTSHelper.IsLocalPlayerFaction(args.FactionId)) return;
        textUI.text = $"{args.NewValue.Value}";
    }
}

