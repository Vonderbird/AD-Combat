using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour
{
    private bool isDeleteEnabled = false;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image trashIcon;

    public bool IsDeleteEnabled => isDeleteEnabled;
    public UnityEvent Clicked;

    public void OnDeleteClicked()
    {
        if (!IsDeleteEnabled)
        {
            text.color = Color.red;
            trashIcon.color = Color.red;
            isDeleteEnabled = true;
        }
        else
        {
            text.color = Color.white;
            trashIcon.color = Color.white;
            isDeleteEnabled = false;
        }
        Clicked?.Invoke();
    }
}
