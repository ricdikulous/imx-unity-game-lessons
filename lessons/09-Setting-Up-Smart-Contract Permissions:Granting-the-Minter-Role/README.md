# Lesson 09: Setting Up Smart Contract Permissions
In this lesson, we focus on a pivotal aspect of NFT integration: setting up smart contract permissions for minting NFTs.

## Overview
Here's an overview of what we'll cover in this lesson:
1. We'll update our environment file and create a script to grant minting permissions to a user's wallet.

## Prerequisites
- Familiarity with Ethereum smart contracts and blockchain transactions.
- Understanding of the 'ethers' library and environment variable management in Node.js.
- Access to Immutable Hub and your NFT contract.

## Environment File Update and Script Creation

### Step 1: Update Environment File
You can find a copy of our [permission project folder here](../09-Setting-Up-Smart-Contract%20Permissions:Granting-the-Minter-Role/permission)
Include the `PRIVATE_KEY` of your minting wallet and the `CONTRACT_ADDRESS` of your NFT contract in your `.env` file.

 - Remember to securely handle the `PRIVATE_KEY`
- You can find your `CONTRACT_ADDRESS` in your project on the [Immutable Hub](https://hub.immutable.com)

```sh
PRIVATE_KEY=
CONTRACT_ADDRESS=
```

### Step 2: Script for Granting Minter Role
We'll develop a script to grant the Minter Role. Below is the complete code snippet for this script:

```typescript
import { getDefaultProvider, Wallet } from 'ethers' // ethers v5
import { Provider, TransactionResponse } from '@ethersproject/providers' // ethers v5
import { ERC721Client } from '@imtbl/contracts'
import * as dotenv from 'dotenv'
dotenv.config()

const CONTRACT_ADDRESS = process.env.CONTRACT_ADDRESS
const PRIVATE_KEY = process.env.PRIVATE_KEY

if (!CONTRACT_ADDRESS) {
  throw new Error('Missing environment variable: CONTRACT_ADDRESS')
}

if (!PRIVATE_KEY) {
  throw new Error('Missing environment variable: PRIVATE_KEY')
}

const provider = getDefaultProvider('https://rpc.testnet.immutable.com')

const grantMinterRole = async (
  provider: Provider,
): Promise<TransactionResponse> => {
  // Bound contract instance
  const contract = new ERC721Client(CONTRACT_ADDRESS)
  // The wallet of the intended signer of the mint request
  const wallet = new Wallet(PRIVATE_KEY, provider)

  // Give the wallet minter role access
  const populatedTransaction = await contract.populateGrantMinterRole(
    wallet.address,
  )
  const result = await wallet.sendTransaction(populatedTransaction)
  return result
}

grantMinterRole(provider)
```

### Code Breakdown
1. **Define Contract Information**: We define `CONTRACT_ADDRESS` and `PRIVATE_KEY` pulled from environment variables, essential for contract interactions and authenticating transactions.
2. **Provider Setup**: Establishing a connection to Immutable's testnet to interact with the blockchain.
3. **Grant Minter Role Function**: 
    1. Smart Contract Instantiation: It initializes a new `ERC721Client` instance, binding the smart contract at `CONTRACT_ADDRESS` to enable interactions.

    2. Wallet Setup: Creates a new Wallet instance using `PRIVATE_KEY` and provider, enabling Ethereum transactions and address management.

    3. Transaction Preparation: Invokes `populateGrantMinterRole` on the contract with wallet.address as the address that will be granted the minter role. Preparing the transaction.

    4. Executing Transaction: Sends the prepared transaction to the blockchain using `wallet.sendTransaction`, which signs it with the wallet's private key.

    5. Transaction Response: Returns the outcome of the transaction as a `TransactionResponse`, concluding the function's operation.

### Step 3: Update Package.json
Add a new script command `grant-minter-role` in `package.json` for easy execution of the script.

```json
"grant-minter-role": "ts-node ./src/grantMinterRole.ts"
```

### Step 4: Run and Verify the Script
Execute the script by executing `npm run grant-minter-role` in the terminal to grant the minter role to the designated wallet. Once complete, verify the transaction in the [Immutable Hub](https://hub.immutable.com) and on the block explorer.

## Conclusion
We've now successfully set up the permissions for our wallet to mint NFTs on our smart contract. This foundational setup is crucial for the upcoming minting process.

## Next Steps
In the next lesson, we'll delve into the actual minting process, using the Immutable SDK to mint and manage NFTs in "Trash Dash". Stay tuned for more!

[Lesson 10: Minting Endpoint](../10-Minting-Endpoint/README.md)
