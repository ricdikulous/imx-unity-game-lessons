# Lesson 10: Minting Endpoint

## Prerequisites
- Familiarity with Node.js, Ethereum blockchain, and the Immutable SDK.
- Understanding of REST API endpoints and Swagger documentation.
- Completion of previous lessons covering the setup of the environment file and granting the minter role.

## Overview
In Lesson 10, we implement the core functionality of minting NFTs using the Immutable SDK in our "Trash Dash" game. This lesson guides you through updating the server to mint NFTs, including the creation of a new service for smart contract interactions and the integration of this functionality into our API.

## Steps for Minting an NFT

### Step 1: Set Up Contract Service
Create a `contractService` file to handle interactions with the smart contract. This service will encapsulate all the logic for smart contract functions, particularly minting.

### Step 2: Import Dependencies and Load Environment Variables
Import necessary modules and use `dotenv` to securely load `CONTRACT_ADDRESS` and `PRIVATE_KEY`.

### Step 3: Define MintRequest Type
Establish a `MintRequest` type, structuring minting requests with a recipient address and token ID.

### Step 4: Create the Mint Function
Develop a mint function within `contractService`. This function will:
- Take in a `MintRequest` object.
- Connect to Immutable's testnet.
- Initialize a contract client and wallet instance.
- Verify minter role permissions for the wallet.
- Prepare and execute the minting transaction.
- Log transaction details.

### Step 5: Integrate Mint Function with API Endpoint
Integrate the mint function into the POST endpoint of our API, combining it with the dynamic metadata upload process.

### Step 6: Run Application and Test Endpoint
Launch the updated application and utilize Swagger to test the minting functionality.

### Step 7: Verify Minting on Immutable Hub
Post-request, confirm the minting process on the Immutable Hub's block explorer.

## Code Snippets
The implementation involves a series of code snippets, each contributing to the setup and execution of the minting process.

\```javascript
// [Snippet for setting up contractService, importing dependencies, and defining MintRequest]
\```

\```javascript
// [Snippet for creating the mint function and integrating it with the API endpoint]
\```

## Conclusion
With this lesson, you'll have a fully functional endpoint capable of dynamically uploading metadata and minting NFTs, marking a significant step in integrating NFT functionality into "Trash Dash".

## Next Steps
Our next lesson will focus on extending the server to create an endpoint for listing a user's NFTs, further enhancing the NFT experience in the game.
