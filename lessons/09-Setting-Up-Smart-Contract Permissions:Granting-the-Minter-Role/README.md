# Lesson 09: Setting Up Smart Contract Permissions

## Prerequisites
- Familiarity with Ethereum smart contracts and blockchain transactions.
- Understanding of the 'ethers' library and environment variable management in Node.js.
- Access to Immutable Hub and your NFT contract.

## Overview
In this lesson, we focus on a pivotal aspect of NFT integration: setting up smart contract permissions for minting NFTs. We'll update our environment file and create a script to grant minting permissions to a user's wallet. This step is essential before proceeding with NFT minting.

## Environment File Update and Script Creation

### Step 1: Update Environment File
Include the `PRIVATE_KEY` of your minting wallet and the `CONTRACT_ADDRESS` of your NFT contract in your `.env` file. Remember to securely handle the `PRIVATE_KEY`.

### Step 2: Script for Granting Minter Role
We'll develop a script to grant the Minter Role. Below is the complete code snippet for this script:

\```javascript
// Import necessary modules
// [Import statements from 'ethers' library and 'dotenv']

// Define CONTRACT_ADDRESS and PRIVATE_KEY
// [Define and check CONTRACT_ADDRESS and PRIVATE_KEY from .env]

// Set up provider
// [Provider setup code for Immutable's testnet]

// Grant Minter Role Function
// [Complete grantMinterRole function]
\```

### Code Breakdown
1. **Import Modules**: We're importing tools from the 'ethers' library for blockchain interactions, and 'dotenv' for managing environment variables.
2. **Define Contract Information**: We define `CONTRACT_ADDRESS` and `PRIVATE_KEY` pulled from environment variables, essential for contract interactions and authenticating transactions.
3. **Provider Setup**: Establishing a connection to Immutable's testnet to interact with the blockchain.
4. **Grant Minter Role Function**: An asynchronous function that prepares and sends a transaction to grant the minter role, using the Immutable ERC721 Client and a wallet instance.

### Step 3: Update Package.json
Add a new script command `grant-minter-role` in `package.json` for easy execution of the script.

### Step 4: Run and Verify the Script
Execute the script to grant the minter role to the designated wallet. Once complete, verify the transaction in the Immutable Hub and on the block explorer.

## Conclusion
We've now successfully set up the permissions for our wallet to mint NFTs on our smart contract. This foundational setup is crucial for the upcoming minting process.

## Next Steps
In the next lesson, we'll delve into the actual minting process, using the Immutable SDK to mint and manage NFTs in "Trash Dash". Stay tuned for more!
