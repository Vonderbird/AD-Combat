using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillTimerUI : MonoBehaviour
{
    [SerializeField] private CellUnitSpawner unitSpawner;
    [SerializeField] private Image fillImage;
    private void Awake()
    {
        if (!unitSpawner)
            unitSpawner = FindAnyObjectByType<CellUnitSpawner>();
        if (!fillImage)
            fillImage = GetComponent<Image>();

        StartCoroutine(UpdateFiller()); 
    }

    IEnumerator UpdateFiller()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            fillImage.fillAmount = unitSpawner.UnitsTimer.CurrValue / unitSpawner.UnitsTimer.DefaultValue;
            yield return null;
        }
    }
}
