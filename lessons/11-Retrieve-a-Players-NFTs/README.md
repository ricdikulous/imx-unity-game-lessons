# Lesson 11: Retrieve a Player's NFTs

## Prerequisites
- Understanding of Ethereum blockchain and Immutable's SDK.
- Familiarity with Node.js and REST API development.
- Completion of previous lessons on setting up contract services and minting NFTs.

## Overview
In Lesson 11, we focus on retrieving a player's NFTs using Immutable's SDK. This lesson involves adding a new function in the contract service and refining the Get NFTs endpoint to return the player's NFTs. Successfully retrieving NFTs is a crucial step in integrating NFT functionality into our game.

## Steps for Retrieving a Player's NFTs

### Step 1: Define List NFTs Function
In the contract service, create an asynchronous `listNFTs` function that accepts a player's account address. This address is crucial for identifying and retrieving the player's NFTs from the blockchain.

### Step 2: Configure Blockchain Data Settings
Specify the environment for blockchain interactions. In development, we'll use Immutable's Sandbox environment.

### Step 3: Create Blockchain Client
Instantiate a blockchain client using the `Blockchain-Data` class with the defined configuration. This client will facilitate interactions with the blockchain to retrieve NFT data.

### Step 4: Implement List NFTs Function
Inside a try block, use the blockchain client to list NFTs based on chain name, contract address, and the player's account address. This will return all NFTs owned by the player, along with their associated metadata.

### Step 5: Integrate List NFTs Function into Routes
Update the routes file to replace the placeholder code with a call to `listNFTs` and return its result.

### Step 6: Test Endpoint with Swagger
Run the application and use Swagger to interact with the Get endpoint. This endpoint will take an account address as a parameter and, upon execution, display all NFTs owned by the account.

## Code Snippets
The implementation includes code snippets for the creation and integration of the `listNFTs` function.

\```javascript
// [Add code snippet for listNFTs function in the contract service]
\```

\```javascript
// [Add code snippet for integrating listNFTs function into the routes file]
\```

## Conclusion
By completing this lesson, you'll have set a solid foundation for both minting NFTs and retrieving a player's NFTs. This is a significant milestone in integrating NFTs into the "Trash Dash" game.

## Next Steps
With these capabilities in place, we are now ready to take the next big step: integrating NFTs directly into our game. Stay tuned for further developments in our NFT integration journey!
