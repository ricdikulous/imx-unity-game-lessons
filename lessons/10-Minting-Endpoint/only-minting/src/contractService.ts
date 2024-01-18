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
