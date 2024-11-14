using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ChatbotClient : Singleton<ChatbotClient>
{
    private const string URL = "http://127.0.0.1:5001/chat";

    public void SendMessageToChatbot(string message)
    {
        StartCoroutine(SendMessageToChatBot(message));
    }
    
    IEnumerator SendMessageToChatBot(string message)
    {
        // Tạo dữ liệu JSON
        string json = "{\"message\": \"" + message + "\"}";

        // Tạo yêu cầu HTTP POST
        using (UnityWebRequest request = new UnityWebRequest(URL, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Gửi yêu cầu và chờ phản hồi
            yield return request.SendWebRequest();

            // Kiểm tra kết quả
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + request.downloadHandler.text);
            }
            else
            {
                Debug.Log("Error: " + request.error);
            }
        }
    }
}