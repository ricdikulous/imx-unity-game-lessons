using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class NftMetadata
{
    public int id;
    public string image;
    public string token_id;
    public string name;
    public string description;
}

[Serializable]
public class TokenObject
{
    public string animation_url;
    public string balance;
    public Chain chain;
    public string contract_address;
    public string contract_type;
    public string description;
    public string external_link;
    public string image;
    public string indexed_at;
    public string metadata_id;
    public string metadata_synced_at;
    public string name;
    public string token_id;
    public string updated_at;
    public string youtube_url;
}

[Serializable]
public class Chain
{
    public string id;
    public string name;
}

public class ApiService : MonoBehaviour
{
    private static string mintEndpoint = "https://aac3-116-255-5-226.ngrok-free.app";    
    public static async Task<List<TokenObject>> GetTokens(string accountAddress)
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{mintEndpoint}/nfts/{accountAddress}"))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
                throw new Exception("Error: " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;
                List<TokenObject> tokens = JsonConvert.DeserializeObject<List<TokenObject>>(jsonResponse);
                return tokens;
            }
        }
    }

    public static async Task<Texture2D> FetchImage(string imageUrl)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);

        Debug.Log("Fetching immage with url: " + imageUrl);

        var imageRequest = www.SendWebRequest();

        while (!imageRequest.isDone)
        {
            await Task.Yield();
        }

        if (www.result == UnityWebRequest.Result.Success)
        {
            // If successful, get the texture from the web request
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            return texture;
        }
        else
        {
            // If the web request failed, throw an exception
            throw new Exception("Failed to fetch image: " + www.error);
        }
    }

    public static async Task<NftMetadata> MakeMintRequest(string recipientAddress)
    {
        using (UnityWebRequest www = UnityWebRequest.Post($"{mintEndpoint}/mint", "{\"recipientAddress\":\"" + recipientAddress + "\"}", "application/json"))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            var operation = www.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
                throw new Exception("Error: " + www.error);
            }
            else
            {
                string jsonResponse = www.downloadHandler.text;

                // Deserialize the JSON response into NftMetadata
                NftMetadata result = JsonUtility.FromJson<NftMetadata>(jsonResponse);
                return result;
            }
        }
    }

}
