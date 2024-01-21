# Overview of the Minting Architecture for "Trash Dash"

## Prerequisites
Before diving into this lesson, ensure you are familiar with our existing integration of Immutable Passport in our game. If you are not, you can check out this lesson [before](../04-retrieve-player-data-and-logout/README.md)


## Introduction
Welcome to Lesson 5 of our course, where we delve into the architecture necessary for adding NFT minting to "Trash Dash". This lesson will not involve coding; instead, we focus on understanding the big picture of how NFT minting will be integrated into our game.

## Lesson Objective
Our goal is to provide you with a clear understanding of the overall architecture for minting NFTs within "Trash Dash". We’ll explore the expansion of our current integration with Immutable Passport, enabling not just player login and detail fetching, but also the minting of rewards as NFTs and their utilization in the game.

## Current State of the Game
Currently, "Trash Dash" has basic integration with Immutable Passport, allowing players to log in and fetch basic details. We aim to expand this by introducing NFTs. Specifically, we'll focus on converting accessories into NFTs. Players will be able to receive these accessories as NFTs for completing missions, view them in an inventory screen, and equip them via the loadout page.

## Proposed Minting Architecture
- **Existing Framework**: The game currently communicates directly with Immutable for player logins and data retrieval.
- **Minting Server**: We will introduce a dedicated server for minting NFTs, controlled and secure, to avoid direct player access to minting permissions.
- **Storage Solution**: An Amazon S3 bucket will be used for storing NFT metadata.
- **Security Note**: For tutorial simplicity, we will run the minting server locally without additional security layers, though this is not recommended for operational games.

## Minting Process
1. **Login**: The game logs the player in with the Immtuable Passport.
2. **Player Data**: The player's account details are retrieved using the Immutable Passport.
3. **POST /mint**: The game sends a request to the minting server with the player's account details.
4. **Upload Metadata**: Concurrently with the minting process, the NFT's metadata is uploaded.
5. **Minting Transaction**: The server uses the Immutable SDK to initiate the minting process.
6. **NFT Metdata**: The server then sends this NFT and it's metadata back to the game.
7. **NFT Image**: A call is made to the S3 bucket to render the NFT's image.

![In-game NFT Minting Diagram](./‎MintingArchitecture.‎002.png)

## Displaying NFTs in Player Inventory
To display NFTs in the player's inventory, we follow a process similar to the minting workflow, involving several key steps:
1. **Login**: The game logs the player in with the Immtuable Passport.
2. **Player Data**: The player's account details are retrieved using the Immutable Passport.
3. **GET /nfts**: The game initiates a request to our minting server, passing the player's account details.
4. **Retrieve Player's NFTs**: The server, in turn, communicates with Immutable to request the player's NFT data.
5. **Player's NFTs**: The server then sends this NFT data back to the game.
6. **NFT Image**: A call is made to the S3 bucket to render the NFT's image.

![Display Player's Inventory Diagram](./‎MintingArchitecture.‎003.png)

## Conclusion
We've outlined a comprehensive architectural plan for integrating NFT minting and displaying them in player inventory in "Trash Dash". This sets the stage for enhancing the game with NFT integration.

## Next Steps
In the upcoming lessons, we will establish the S3 bucket for NFT metadata and integrate a dedicated minting server with the Immutable SDK. These steps are crucial for bringing a new dimension of NFT minting to our game.

[Lesson 06: Creating an S3 Bucket for NFT Metadata](../06-Creating-an-S3-Bucket-for-NFT-Metadata/README.md)
