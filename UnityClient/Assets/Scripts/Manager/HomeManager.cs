using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField = default;
    [SerializeField] private TextMeshProUGUI output = default;
    
    public void OnClickSearchByCamera()
    {
        PopupUIManager.Instance.GetPopup<PopupCameraCapture>().ShowPopup();
    }

    public void OnClickSendInput()
    {
        Debug.Log(inputField.text);
        output.text = inputField.text;
        ChatbotClient.Instance.SendMessageToChatbot(inputField.text);
    }
}
