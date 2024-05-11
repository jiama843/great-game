using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaController : MonoBehaviour
{
    [SerializeField] ManaScriptableObject manaSO;

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI manaText;


    // Start is called before the first frame update
    void Awake()
    {
        slider.value = manaSO.manaAmount;
        SetManaText();
    }

    public int GetCurrentMana()
    {
        return manaSO.manaAmount;
    }

    public void DecreaseMana(int amount)
    {
        manaSO.manaAmount -= amount;
        slider.value = manaSO.manaAmount;
        SetManaText();
    }

    void SetManaText()
    {
        manaText.text = "MP: " + manaSO.manaAmount.ToString() + "/" + manaSO.maxMana.ToString();
    }

}
