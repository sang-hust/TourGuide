using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CameraCapture : MonoBehaviour
{
    [SerializeField] private RawImage rawImage = default;
    private WebCamTexture webCamTexture;

    private string serverUrl = "http://localhost:8088/api/uploadImage";  // Đường dẫn API trên server

    
    private void Start()
    {
        webCamTexture = new WebCamTexture();
        if(!webCamTexture.isPlaying) webCamTexture.Play();
        rawImage.texture = webCamTexture;
    }
    
    public IEnumerator SendImage(Texture2D texture)
    {
        byte[] imageData = texture.EncodeToPNG();  // Chuyển hình ảnh thành PNG

        UnityWebRequest request = new UnityWebRequest(serverUrl, UnityWebRequest.kHttpVerbPOST);
        request.uploadHandler = new UploadHandlerRaw(imageData);
        request.uploadHandler.contentType = "application/octet-stream";
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Image sent successfully! Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error sending image: " + request.error);
        }
    }

    public void CaptureAndSend()
    {
        var snap = new Texture2D(webCamTexture.width, webCamTexture.height);
        snap.SetPixels(webCamTexture.GetPixels());
        snap.Apply();

        StartCoroutine(SendImage(snap));
        // StartCoroutine(SendImageToServer());
    }

    private IEnumerator SendImageToServer()
    {
        yield return null;
        
        // Chụp ảnh từ camera
        var snap = new Texture2D(webCamTexture.width, webCamTexture.height);
        snap.SetPixels(webCamTexture.GetPixels());
        snap.Apply();

        // Chuyển đổi ảnh thành byte array
        var imageBytes = snap.EncodeToPNG();

        // Tạo request
        // var www = UnityWebRequest.Put(apiURL, imageBytes);
        // www.SetRequestHeader("Content-Type", "image/png");
        //
        // // Gửi request và nhận phản hồi
        // yield return www.SendWebRequest();
        //
        // if (www.result == UnityWebRequest.Result.Success)
        // {
        //     Debug.Log("API Response: " + www.downloadHandler.text);
        // }
        // else
        // {
        //     Debug.Log("API Error: " + www.error);
        // }
    }
}
