# Lesson 12: Display the Player's NFTs in "Trash Dash"

## Introduction
In today's lesson, Lesson 12, we're focusing on a critical aspect of our game development for "Trash Dash" — displaying the player's NFTs in the game. This follows our previous work of setting up the minting server.

## Objectives
Our goals for this lesson are:
1. Update Unity code to fetch NFT data from our server.
2. Enhance the code to retrieve NFT images.
3. Display each NFT, complete with its image and metadata, in the game.

## Current State of the Game
We've implemented an NFT inventory feature, displaying NFTs owned by the player with names, token IDs, and descriptions. However, these are currently placeholders with mock data and incorrect image rendering. Our task now is to fetch and display actual NFTs owned by the player.

## Prerequisites
Before proceeding, make sure you are comfortable with:
- Unity development and scripting.
- Working with APIs in Unity.
- Understanding of NFTs and blockchain technology.

## Implementation Steps

### NFT Inventory UI Script
1. **Existing Features**: The script includes loading states, a content panel for NFT displays, and a template for each NFT item.
2. **Data Structures**: It uses a dictionary to store NFT data and a list to track created prefabs.
3. **UI Management**: Open and Close methods manage the inventory UI visibility.
4. **Fetching NFTs**: The Fetch NFTs method retrieves NFT data and calls other functions to process and display this data.
5. **Populating UI**: The Populate method iterates through NFT data, instantiates new panels, and sets properties based on metadata.
6. **Dynamic Address Fetching**: We update the fetch method to use the player's actual address, introducing a new class, `Passport Service`, for dynamic and personalized NFT fetching.

### Get Tokens Function
1. **Initial Approach**: Previously, we used a mock JSON string for sample NFT data.
2. **Dynamic Data Fetching**: We update the function to fetch real-time data from our server using Unity Web Request.
3. **Error Handling**: The function handles connection or protocol errors and deserializes the JSON response into a list of TokenObject.
4. **Implementing Passport Service**: The function now uses dynamic player addresses to fetch NFTs.

### NFT Inventory UI Script (Continued)
1. **Dynamic Address Handling**: We update the API call to use the current player's address.
2. **Passport Service Integration**: This new service manages player-related data, including blockchain addresses.

### Fetch Image Function
1. **Function Creation**: Added in the ApiService class to fetch image textures.
2. **Implementation**: Uses Unity Web Request Texture to fetch and render images from NFT image URLs.

### NftPanel Class
1. **Display Functionality**: Responsible for displaying NFT metadata.
2. **Image Rendering**: Adds a function to fetch and set the image texture for each NFT.

### Unity Demonstration
1. **API Call**: The inventory now makes API calls to fetch the player's actual NFTs.
2. **Image Rendering**: Images are successfully rendered, enhancing the user experience.

## Conclusion
We've successfully updated "Trash Dash" to fetch and display the player's NFTs, making them tangible elements within our game. This enhances the overall user experience by integrating NFTs seamlessly into the gameplay.

## Next Steps
In the next lesson, we will take these NFTs beyond visual elements, making them functional within the game. Stay tuned as we continue to revolutionize the gaming experience with NFT integration.

---
**Stay excited for our next lesson, where we bring functionality to these NFTs within "Trash Dash"!**
