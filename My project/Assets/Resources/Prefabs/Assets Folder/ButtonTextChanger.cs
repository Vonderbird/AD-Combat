using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonTextChanger : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    public TextMeshProUGUI homeworldText;
    public TextMeshProUGUI battlegroundText;
    public AudioSource buttonAudioSource;
    public AudioClip buttonSoundEffect;

    private bool isHomeworldTextActive = true;

    public void OnButtonClick()
    {
        isHomeworldTextActive = !isHomeworldTextActive;

        homeworldText.gameObject.SetActive(isHomeworldTextActive);
        battlegroundText.gameObject.SetActive(!isHomeworldTextActive);

        buttonAudioSource.PlayOneShot(buttonSoundEffect);
    }
}