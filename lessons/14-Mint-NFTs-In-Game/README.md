# Lesson 14: Mint NFTs In Game

## Introduction
In Lesson 14, we're introducing a transformative feature in "Trash Dash": minting NFTs as rewards for completing missions. This update integrates blockchain technology directly into our game mechanics, enhancing the player experience.

## Prerequisites
In the previous lessons we have setup a server for retrieving the player's NFTs and created an ApiService in our game to call that server. If you haven't already you can complete that in [previous lessons](../13-Equipping-the-NFT-Accessories/README.md)

## Objectives
1. Review the current mission completion and reward system.
2. Update game mechanics to reward players with NFTs instead of in-game currency.
3. Demonstrate the updated game flow where players claim NFTs as mission rewards.

## Transition to NFT Rewards

### Current Reward System
- **Original Mechanism**: Players complete missions and receive in-game currency.
- **Goal**: Shift from traditional rewards to innovative NFT rewards.

### Updating Game Mechanics
- **NFT Popup Introduction**: Replace in-game currency rewards with a visual NFT popup.
- **Placeholder Functionality**: Currently, the NFT popup is hardcoded and non-functional.

### Enabling Real NFT Minting
- **Mint Endpoint Recap**: Review the endpoint created earlier for minting NFTs.
- **API-Service Class Update**: Introduce the NFT-Metadata class and a function to call the mint endpoint.

### Implementing NFT Minting in Game
- **Functionality**: Create an asynchronous function for NFT minting, handling recipient address and server response.
- **Integration in MissionUI**: Update the Claim function to include real NFT minting and data display.

## In-Game Demonstration
1. **Start the server**: Your server must be running on `localhost:3000` for this to work.
2. **Run the game**: Run the game and open the Loadout. The go to a completed mission and click claim.

## Conclusion
We've successfully integrated NFT minting into "Trash Dash," allowing players to earn unique NFTs by completing missions. This feature not only enhances the reward mechanism but also enriches the overall gaming experience with the integration of blockchain technology.

## Looking Ahead
In upcoming sessions, we will explore further advancements, like enabling players to craft new NFTs by combining multiple ones.
