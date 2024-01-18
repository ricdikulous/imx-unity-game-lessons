# Lesson 13: Update the Game to Use NFTs In Game

## Introduction
In Lesson 13, we're enhancing "Trash Dash" by integrating NFTs into the game's mechanics, shifting the player experience from traditional in-game purchases to NFT-based accessory ownership.

## Prerequisites
In the previous lessons we have setup a server for retrieving the player's NFTs and created an ApiService in our game to call that server. If you haven't already you can complete that in [previous lessons](../12-Display-the-Players-NFTs/README.md)

## Objectives
1. Examine the pre-NFT method of accessory management.
2. Update the game to link accessory ownership with NFTs.
3. Demonstrate the integration in-game.

## How equipping NFTs Worked

### Before NFT Integration
![Before NFT Integration Diagram](./EquipNFTsDiagramOld.png)
- Accessories were bought using in-game currency
- The Store updates the PlayerData when the accessory is purchased
- The Loadout Page reads from the PlayerData to determine which accessories the player can equip
- Player equips the accessory.

### After Integrating NFTs
![After NFT Integration Diagram](./EquipNFTsDiagramNew.png)
- Can no longer rely on the Store to update the PlayerData
- The Loadout Page makes a request to the Minting API to determine the Player's NFTs
- Uses the NFT data to update the PlayerData
- The Loadout Page reads from the PlayerData to determine which accessories the player can equip
- Player equips the accessory.

## Updating Game Mechanics

Update the `LoadoutState.cs` class with a function to fetch the player's NFTs and update the PlayerData

```csharp
public async void FetchAccessoryNFTs() {
        loadingAccessories.SetActive(true);
  List<string> accounts = await PassportService.FetchPlayerAccounts();
      List<TokenObject> tokenObjects = await ApiService.GetTokens(accounts[0]);
      
      foreach (var token in tokenObjects)
      {
          switch (token.name)
          {
              case "Cat Party Hat":
                  PlayerData.instance.AddAccessory("Trash Cat:Party Hat");
                  break;
              case "Cat Construction Hat":
                  PlayerData.instance.AddAccessory("Trash Cat:Safety");
                  break;
              case "Cat Top Hat":
                  PlayerData.instance.AddAccessory("Trash Cat:Smart");
                  break;
              case "Racoon Party Hat":
                  PlayerData.instance.AddAccessory("Rubbish Raccoon:Party Hat");
                  break;
              case "Racoon Construction Hat":
                  PlayerData.instance.AddAccessory("Rubbish Raccoon:Safety");
                  break;
              default:
                  // Handle the case when token name doesn't match any known value
                  break;
          }
      }
      loadingAccessories.SetActive(false);
      Refresh();
  }
```

Update the `Enter` function in the loadoutstate to clear the player's accessories and call our new function

```csharp
PlayerData.instance.ClearAccessories();
FetchAccessoryNFTs();
```
The accessories must be cleared in case there a transaction of NFTs has occurred outside of the game.

### Test and run
1. **Start the server**: Your server must be running on `localhost:3000` for this to work.
2. **Run the game**: Run the game and open the Loadout. If done correctly you will see a little loading indicator and then you will be able to equip the accessories to the character.

## Conclusion
We've successfully integrated NFTs into "Trash Dash," updating the game mechanics to dynamically reflect real-time NFT ownership, enhancing the player's experience by linking digital assets with in-game functionality.

## Next Steps
Prepare for the next lesson, where we introduce in-game NFT minting, expanding the interactive possibilities in "Trash Dash."

[Lesson 14: Mint NFTs In Game](../14-Mint-NFTs-In-Game/README.md)