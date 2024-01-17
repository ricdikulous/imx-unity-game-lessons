# Lesson 13: Update the Game to Use NFTs in "Trash Dash"

## Introduction
In Lesson 13, we're enhancing "Trash Dash" by integrating NFTs into the game's mechanics, shifting the player experience from traditional in-game purchases to NFT-based accessory ownership.

## Prerequisites
- Familiarity with Unity game development and scripting.
- Understanding of NFTs in gaming.
- Experience with APIs and asynchronous programming in Unity.

## Objectives
1. Examine the pre-NFT method of accessory management.
2. Update the game to link accessory ownership with NFTs.
3. Demonstrate the integration in-game.

## Transitioning to NFT-Based Accessory Management

### Before NFT Integration
- **Original Method**: Accessories were bought using in-game currency.
- **Equipping Mechanism**: Players equipped accessories on the loadout page.

### Integrating NFTs
- **NFT-Based Ownership**: Shift to NFTs representing player accessories.
- **Challenge**: Players can view but not equip NFT accessories.

### Updating Game Mechanics
- **Adapting Player Data**: Change from in-game purchase updates to NFT-based updates.
- **New Functionality**:
  - `Fetch Accessory NFTs`: An asynchronous function to fetch NFT data.
  - **Player Data Update**: Reflect accessory ownership based on NFTs.
  - **Loadout Script Adjustments**: Reset accessory list and fetch the latest NFT data on each loadout screen access.
  - **Accessory Identification**: Match NFT tokens with specific accessories using switch statements in the loop.

## In-Game Demonstration
- **Loading Indicator**: Signifies real-time fetching of NFT data.
- **Real-Time Updates**: Accessories align with the player's NFT collection, available for equipping.
- **Inventory Synchronization**: Confirms in-game accessories match NFT holdings.

## Conclusion
We've successfully integrated NFTs into "Trash Dash," updating the game mechanics to dynamically reflect real-time NFT ownership, enhancing the player's experience by linking digital assets with in-game functionality.

## Next Steps
Prepare for the next lesson, where we introduce in-game NFT minting, expanding the interactive possibilities in "Trash Dash."

---
**Stay tuned for the exciting addition of in-game NFT minting in the upcoming lesson!**
