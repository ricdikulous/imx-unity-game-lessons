# Lesson 10: Minting an NFT
In Lesson 10, we implement the core functionality of minting NFTs using the Immutable SDK in our "Trash Dash" game.

## Overview
Here's an overview of what we'll cover in this lesson:
1. Add a new Service file
2. Create a Minting function
3. Integrate the Minting function with our API endpoint
4. Test the minting process with the Swagger API

## Prerequisites
- Familiarity with Node.js, Ethereum blockchain, and the Immutable SDK.
- Understanding of REST API endpoints and Swagger documentation.
- Completion of previous lessons covering the setup of the environment file and [granting the minter role](../09-Setting-Up-Smart-Contract%20Permissions:Granting-the-Minter-Role/README.md).

## Steps for Minting an NFT

### Step 1: Set Up Contract Service
Create a `contractService` file to handle interactions with the smart contract. 

```typescript
import { getDefaultProvider, Wallet } from 'ethers' // ethers v5
import { TransactionResponse } from '@ethersproject/providers' // ethers v5
import { ERC721MintByIDClient } from '@imtbl/contracts'
import * as dotenv from 'dotenv'
import { ethers } from 'ethers'

dotenv.config()

const CONTRACT_ADDRESS = process.env.CONTRACT_ADDRESS
const PRIVATE_KEY = process.env.PRIVATE_KEY

if (!CONTRACT_ADDRESS) {
  throw new Error('Missing environment variable: CONTRACT_ADDRESS')
}

if (!PRIVATE_KEY) {
  throw new Error('Missing environment variable: PRIVATE_KEY')
}

type MintRequest = {
  to: string
  tokenId: number
}
```
- This service will encapsulate all the logic for smart contract functions, particularly minting.
- Import necessary modules and use `dotenv` to securely load `CONTRACT_ADDRESS` and `PRIVATE_KEY`.
- Establish a `MintRequest` type, structuring minting requests with a recipient address and token ID.

### Step 2: Create the Mint Function
```typescript
export const mint = async (
  mintRequest: MintRequest,
): Promise<TransactionResponse> => {
  const provider = getDefaultProvider('https://rpc.testnet.immutable.com')

  // Bound contract instance
  const contract = new ERC721MintByIDClient(CONTRACT_ADDRESS)
  // The wallet of the intended signer of the mint request
  const wallet = new Wallet(PRIVATE_KEY, provider)

  // We can use the read function hasRole to check if the intended signer
  // has sufficient permissions to mint before we send the transaction
  const minterRole = await contract.MINTER_ROLE(provider)
  const hasMinterRole = await contract.hasRole(
    provider,
    minterRole,
    wallet.address,
  )

  if (!hasMinterRole) {
    // Handle scenario without permissions...
    console.log('Account doesnt have permissions to mint.')
    return Promise.reject(new Error('Account doesnt have permissions to mint.'))
  }

  // Rather than be executed directly, contract write functions on the SDK client are returned
  // as populated transactions so that users can implement their own transaction signing logic.
  const populatedTransaction = await contract.populateMint(
    mintRequest.to,
    mintRequest.tokenId,
  )

  const result = await wallet.sendTransaction(populatedTransaction)
  console.log(result)
  return result
}
```
Develop a mint function within `contractService`. This function will:
- Take in a `MintRequest` object.
- Connect to Immutable's testnet.
- Initialize a contract client and wallet instance.
- Verify minter role permissions for the wallet.
- Prepare and execute the minting transaction.
- Log transaction details.

### Step 3: Integrate Mint Function with API Endpoint
Integrate the mint function into the POST endpoint of the `routes.ts` file, combining it with the dynamic metadata upload process.
```typescript
const [metadata] = await Promise.all([
    await uploadRandomMetadata(tokenId),
    await mint({ to: recipientAddress, tokenId: tokenId }),
])
```

### Step 4: Run Application and Test Minting
To test it out you can run `npm run dev` and navigate to `http://localhost:3000` where you will see the Swagger UI:
- Execute the `/mint` POST endpoint and set the recipient address for the NFT.
- Verify the image and metadata URLs post-upload.

You can confirm this step was successful by navigating to the [Immutable Hub](https://hub.immutable.com) and clicking on the `Block Explorer` for your NFT collection - you should see a transaction with `Token transfer` in the Type field and `mint` in the Method field.

## Conclusion
With this lesson, you'll have a fully functional endpoint capable of dynamically uploading metadata and minting NFTs, marking a significant step in integrating NFT functionality into "Trash Dash".

## Next Steps
Our next lesson will focus on extending the server to create an endpoint for listing a user's NFTs, further enhancing the NFT experience in the game.

[Lesson 11: Retrieve a Player's NFTs](../11-Retrieve-a-Players-NFTs/README.md)
