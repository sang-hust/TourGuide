using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ImageRecognitionClient : MonoBehaviour
{
    private string serverUrl = "http://yourserver.com/api/uploadImage";

    public IEnumerator SendImage(Texture2D texture)
    {
        byte[] imageData = texture.EncodeToPNG();

        UnityWebRequest www = UnityWebRequest.PostWwwForm(serverUrl, "");
        www.uploadHandler = new UploadHandlerRaw(imageData);
        www.SetRequestHeader("Content-Type", "application/octet-stream");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Received recognition result: " + www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error: " + www.error);
        }
    }
}