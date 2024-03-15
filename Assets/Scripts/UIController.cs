using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private GameObject[] uiPanels;

    [Header("Order and Product Code")] [SerializeField]
    private TMP_Text orderCodeText;

    [SerializeField] private TMP_Text productCodeText;
    [SerializeField] private float orderCode;
    [SerializeField] private float productCode;

    [Header("Order Count")] [SerializeField]
    private float orderFillAmount;

    [SerializeField] private Image orderProgressBar;
    [SerializeField] private float orderTotalProgressBarValue;
    [SerializeField] private float maxOrderProgressBarValue;
    [SerializeField] private TMP_Text orderCountText;

    [Header("OEE")] [SerializeField] private float oeeFillAmount;
    [SerializeField] private Image oeeProgressBar;
    [SerializeField] private float maxOeeProgressBarValue;
    [SerializeField] private TMP_Text oeeValueText;
    [SerializeField] private float repeatRate;

    [Header("Stove")] [SerializeField] private TMP_Text stoveTemperatureText;
    [SerializeField] private TMP_Text stoveHeightText;
    [SerializeField] private TMP_Text upperMoldTemperatureText;
    [SerializeField] private TMP_Text lowerMoldTemperatureText;
    [SerializeField] private TMP_Text accelerationText;
    [SerializeField] private TMP_Text pressureText;
    [SerializeField] private float stoveTemperatureValue;
    [SerializeField] private float stoveHeightValue;
    [SerializeField] private float upperMoldTemperatureValue;
    [SerializeField] private float lowerMoldTemperatureValue;
    [SerializeField] private float accelerationValue;
    [SerializeField] private float pressureValue;
    [SerializeField] private float stoveTemperatureBaseValue;
    [SerializeField] private float stoveHeightBaseValue;
    [SerializeField] private float upperMoldTemperatureBaseValue;
    [SerializeField] private float lowerMoldTemperatureBaseValue;
    [SerializeField] private float accelerationBaseValue;
    [SerializeField] private float pressureBaseValue;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ActionManager.Instance.OnIncreaseOrderAndProductValue += IncreaseOrderAndProductCode;
        ActionManager.Instance.OnIncreaseOrderCount += IncreaseOrderCount;

        InvokeRepeating("SetRandomOeeValue", 0, repeatRate);
        InvokeRepeating("SetRandomStoveValue",0,1);
    }

    public void ActivateMainPanel()
    {
        StartCoroutine(ActivateMainPanelCo());
    }

    IEnumerator ActivateMainPanelCo()
    {
        yield return new WaitForSeconds(1f);

        foreach (var panel in uiPanels)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

    private void IncreaseOrderAndProductCode()
    {
        orderCode += 5;
        productCode += 5;

        orderCodeText.text = "O-20519" + orderCode;
        productCodeText.text = "E-57" + productCode;
    }

    private void IncreaseOrderCount()
    {
        orderTotalProgressBarValue += orderFillAmount;

        if ((int)orderTotalProgressBarValue > (int)maxOrderProgressBarValue)
        {
            SetFillImage(orderProgressBar, 0);
            orderTotalProgressBarValue = 0;
        }
        else
        {
            SetFillImage(orderProgressBar, orderTotalProgressBarValue / maxOrderProgressBarValue);
        }

        orderCountText.text = orderTotalProgressBarValue + "/800";
    }

    private void SetFillImage(Image progressBar, float value, Ease ease = Ease.OutBack)
    {
        DOTween.Kill(progressBar);
        progressBar.DOFillAmount(value, .3f).SetEase(ease).SetId(progressBar);
    }

    private void SetRandomOeeValue()
    {
        oeeFillAmount = Random.Range(75, 85);
        SetFillImage(oeeProgressBar, oeeFillAmount / maxOeeProgressBarValue);
        oeeValueText.text = "%" + oeeFillAmount;
    }

    private void SetRandomStoveValue()
    {
        stoveTemperatureText.text = GetRandomPercentValue(stoveTemperatureValue, stoveTemperatureBaseValue, 1.1f, .9f) +
                                    "\u00b0C";
        stoveHeightText.text = GetRandomPercentValue(stoveHeightValue, stoveHeightBaseValue, 1.1f, .9f) + "kg";
        upperMoldTemperatureText.text =
            GetRandomPercentValue(upperMoldTemperatureValue, upperMoldTemperatureBaseValue, 1.1f, .9f) + "\u00b0C";
        lowerMoldTemperatureText.text =
            GetRandomPercentValue(lowerMoldTemperatureValue, lowerMoldTemperatureBaseValue, 1.1f, .9f) + "\u00b0C";
        accelerationText.text =
            GetRandomPercentValue(accelerationValue, accelerationBaseValue, 1.1f, .9f) + "mm/s\u00b2";
        pressureText.text = GetRandomPercentValue(pressureValue, pressureBaseValue, 1.1f, .9f) + "bar";
    }

    private float GetRandomPercentValue(float targetValue, float baseValue, float positiveValue, float negativeValue)
    {
        targetValue = Random.Range(baseValue * positiveValue, baseValue * negativeValue);
        return targetValue;
    }
    
    public void OpenTargetURL(string url)
    {
        Application.OpenURL(url);
    }
}