# Lesson 12: Display the Player's NFTs In Game
In lesson 12, we're focusing on a critical aspect of our game development, displaying the player's NFTs inside the game.

## Overview
Here's an overview of what we'll cover in this lesson:
1. Update Unity code to fetch NFT data from our server.
2. Enhance the code to retrieve NFT images.
3. Display each NFT, complete with its image and metadata, in the game.

## Prerequisites
For this to work it requires that you have a server to make requests to and return the player's NFTs. If you haven't done this already you can complete the [previous lessons](../11-Retrieve-a-Players-NFTs/README.md).

## Current State of the Game
We've implemented an NFT inventory feature, displaying NFTs owned by the player with names, token IDs, and descriptions. However, these are currently placeholders with mock data and incorrect image rendering. Our task now is to fetch and display actual NFTs owned by the player.

## Implementation Steps

### Step 1: Dynamically Fetch the Player's Account
Seeing as this is something that we will be doing a number of times in our game let's create a new `PassportService.cs` so we can encapsulate the logic for fetching a Player's account.

```csharp
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Immutable.Passport;

public class PassportService
{
    public static async Task<List<string>> FetchPlayerAccounts()
    {
        Passport passport = Passport.Instance;
        await passport.ConnectEvm();
        List<string> accounts = await passport.ZkEvmRequestAccounts();
        Debug.Log("Players accounts: " + String.Join(", ", accounts)); 
        return accounts;
    }
}
```

Update the `FetchNFTs` function in the `NftInventoryUI.cs` class to use this new service and function. We're currently returning the first account returned on the list but you can give the player a choice.

```csharp
List<string> accounts = await PassportService.FetchPlayerAccounts();
List<TokenObject> tokenObjects = await ApiService.GetTokens(accounts[0]);
```

### Step 2: ApiService and Get Tokens Function

Create an `ApiService.cs` file to encapulate the business logic for communicating with out server.

```csharp
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
    private static string mintEndpoint = "http://localhost:3000";    
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
}
```
1. **TokenObject**: The represents the data that is returned from our GET /nfts endpoint.
2. **mintEndpoint**: Is directed at our server running on our localhost.
3. **GetTokens**: The function takes the player's account, makes a request to `http://localhost:3000/nfts/{accountAddress}`, handles connection or protocol errors and then deserializes the JSON response into a list of TokenObject.

### Step 3: Fetch Image Function

We need a function to fetch the image for the NFTs separately as the metadata only returns the image URL of the NFT. To do so, we need to update our `ApiService.cs` file.

```csharp
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
```
Update the `Populate` function in the `NftPanel.cs` class to call `FetchImage` with the image URL.

```csharp
private async void FetchImage(string imageUrl)
{
    Texture2D texture = await ApiService.FetchImage(imageUrl);
    image.texture = texture;
}
```

### Test and run
1. **Start the server**: Your server must be running on `localhost:3000` for this to work.
2. **Run the game**: Run the game and open the NFT Inventory and you should see the player's NFTs.

## Conclusion
We've successfully updated "Trash Dash" to fetch and display the player's NFTs, making them tangible elements within our game. This enhances the overall user experience by integrating NFTs seamlessly into the gameplay.

## Next Steps
In the next lesson, we will take these NFTs beyond visual elements, making them functional within the game. Stay tuned as we continue to revolutionize the gaming experience with NFT integration.

[Lesson 13: Update the Game to Use NFTs In Game](../13-Equipping-the-NFT-Accessories/README.md)
