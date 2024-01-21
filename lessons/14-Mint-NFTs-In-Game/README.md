# Lesson 14: Mint NFTs in Game
In Lesson 14, we're introducing a transformative feature in "Trash Dash": minting NFTs as rewards for completing missions. This update integrates blockchain technology directly into our game mechanics, enhancing the player experience.

## Overview
Here's an overview of what we'll cover in this lesson:
1. Review the current mission completion and reward system.
2. Update game mechanics to reward players with NFTs instead of in-game currency.
3. Demonstrate the updated game flow where players claim NFTs as mission rewards.

## Prerequisites
In the previous lessons we have setup a server for retrieving the player's NFTs and created an ApiService in our game to call that server. If you haven't already you can complete that in [previous lessons](../13-Equipping-the-NFT-Accessories/README.md).

## Transition to NFT Rewards

### Current Reward System
- **Original Mechanism**: Players complete missions and receive in-game currency.
- **Goal**: Shift from traditional rewards to innovative NFT rewards.

### Updating Game Mechanics
- **NFT Popup Introduction**: Replace in-game currency rewards with a visual NFT popup.
- **Placeholder Functionality**: Currently, the NFT popup is hardcoded and non-functional.

### Add the NFT Metadata Class

Add the NftMetadata class to the `ApiService.cs` file

```csharp
[Serializable]
public class NftMetadata
{
    public int id;
    public string image;
    public string token_id;
    public string name;
    public string description;
}
```

### Enabling NFT Minting

Add a function to the `ApiService.cs` class to call our mint endpoint

```csharp
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
```
- Creates a POST request to the mint endpoint with the recipient's address in JSON format.

- Initiates the web request and waits for completion using an asynchronous loop with `Task.Yield()`.

- Converts the JSON response into an `NftMetadata` object and returns it.

### Integrate into Missions
Update the `Claim` function in the `MissionUI.cs` class to call the new mint function

```csharp
public async void Claim(MissionBase m)
{
    PlayerData.instance.ClaimMission(m);

    List<string> accounts = await PassportService.FetchPlayerAccounts();
    NftMetadata nftMetadata = await ApiService.MakeMintRequest(accounts[0]);

    Texture2D texture = await ApiService.FetchImage(nftMetadata.image);
    nftName.text = nftMetadata.name;
    nftDescription.text = nftMetadata.description;
    nftId.text = "Token ID: " + nftMetadata.token_id;
    nftImage.texture = texture;
    mintedNFT.SetActive(true);
}
```
- Retrieves the player's account.
- Calls our new mint function.
- Retrieves the new NFTs image.
- Updates the UI to display the new NFT.

### Test and Run
1. **Start the server**: Your server must be running on `localhost:3000` for this to work.
2. **Run the game**: Run the game and open the Loadout. The go to a completed mission and click claim.

## Conclusion
We've successfully integrated NFT minting into "Trash Dash," allowing players to earn unique NFTs by completing missions. This feature not only enhances the reward mechanism but also enriches the overall gaming experience with the integration of blockchain technology.
