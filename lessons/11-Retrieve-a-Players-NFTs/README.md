# Lesson 11: Retrieve a Player's NFTs

## Prerequisites
This lesson assumes that you already have a smart contract in place and are able to mint NFTs from that contract. If you haven't done that already please go back and complete the [previous lessons](../10-Minting-Endpoint/README.md)

## Overview
In Lesson 11, we focus on retrieving a player's NFTs using Immutable's SDK. This lesson involves adding a new function in the contract service and refining the Get NFTs endpoint to return the player's NFTs. Successfully retrieving NFTs is a crucial step in integrating NFT functionality into our game.

## Steps for Retrieving a Player's NFTs

### Step 1: Implement The Function to retrieve the NFTs

Implement listNFTs Function in contractService.ts

```typescript
export const listNFTs = async (accountAddress: string) => {
  const config: blockchainData.BlockchainDataModuleConfiguration = {
    baseConfig: {
      environment: immutableConfig.Environment.SANDBOX,
    },
  }

  const client = new blockchainData.BlockchainData(config)

  try {
    const response = await client.listNFTsByAccountAddress({
      chainName: 'imtbl-zkevm-testnet',
      contractAddress: CONTRACT_ADDRESS,
      accountAddress: accountAddress,
    })
    return response
  } catch (error) {
    console.error(error)
  }
}
```
- listNFTs is an exported asynchronous function that takes the address of the account you want to query.
- Initializes a blockchain data configuration with the Sandbox environment.
- Creates a BlockchainData client with the specified configuration.
- Uses the client to asynchronously fetch NFTs associated with the provided accountAddress, specifying the chain name and contract address.
- Returns the response containing the NFT data.

### Step 2: Integrate List NFTs Function into Routes
Update the `routes.ts` to call `listNFTs` and return its result to the caller.

```typescript
const nfts = await listNFTs(accountAddress)
res.status(200).json(nfts?.result)
```

### Step 3: Run Application and Test the Endpoint
To test it out you can run `npm run dev` and navigate to `http://localhost:3000` where you will see the swagger docs
- Execute the `/nfts` GET endpoint.
- Verify the returned NFTs are as you expect.

## Conclusion
By completing this lesson, you'll have set a solid foundation for both minting NFTs and retrieving a player's NFTs. This is a significant milestone in integrating NFTs into the "Trash Dash" game.

## Next Steps
With these capabilities in place, we are now ready to take the next big step: integrating NFTs directly into our game. Stay tuned for further developments in our NFT integration journey!

[Lesson 12: Display the Player's NFTs In Game](../12-Display-the-Players-NFTs/README.md)