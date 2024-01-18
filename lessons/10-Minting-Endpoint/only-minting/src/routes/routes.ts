import express from 'express'
import { ethers } from 'ethers'
import { readState, writeState } from '../stateManager'
import { uploadRandomMetadata } from '../metadataService'
import { mint } from '../contractService'

/**
 * @swagger
 * tags:
 *   name: Mints
 *   description: API operations related to minting NFTs
 */

const router = express.Router()

router.get('/', (req, res) => {
  res.redirect(302, '/docs')
})

/**
 * @swagger
 * /mint:
 *   post:
 *     summary: Mint a new token
 *     tags: [Mints]
 *     requestBody:
 *       required: true
 *       content:
 *         application/json:
 *           schema:
 *             example:
 *               recipientAddress: '0x18bAD9BF2553dc16DA15d24D2679f00fB7D7E23A'
 *             required:
 *               - recipientAddress
 *     responses:
 *       '200':
 *         description: Transaction processed successfully.
 *         content:
 *           application/json:
 *             example:
 *               message: Transaction processed successfully.
 *               recipientAddress: '0xRecipientAddress'
 *       '400':
 *         description: Bad request
 *         content:
 *           application/json:
 *             example:
 *               error: 'Invalid "recipientAddress".'
 *       '500':
 *         description: Internal server error
 *         content:
 *           application/json:
 *             example:
 *               error: 'Internal server error.'
 */
router.post('/mint', async (req, res) => {
  const { recipientAddress } = req.body

  if (!recipientAddress) {
    return res.status(400).json({
      error: 'missing "recipientAddress" in the request body.',
    })
  }

  if (
    typeof recipientAddress !== 'string' ||
    !ethers.utils.isAddress(recipientAddress)
  ) {
    return res.status(400).json({
      error: `${recipientAddress} is not a valid "recipientAddress".`,
    })
  }

  try {
    const initialState = readState()
    const tokenId = initialState.latestTokenID

    const [metadata] = await Promise.all([
      await uploadRandomMetadata(tokenId),
      await mint({ to: recipientAddress, tokenId: tokenId }),
    ])

    const updatedState = { ...initialState, latestTokenID: tokenId + 1 }
    writeState(updatedState)

    res.status(200).json(metadata)
  } catch (error) {
    console.error(error)
    res.status(500).json({ error: 'Internal server error.' })
  }
})

/**
 * @swagger
 * /nfts/{accountAddress}:
 *   get:
 *     summary: Get a list of mints for a specific Ethereum account
 *     description: Retrieve a list of mints for the provided Ethereum account address
 *     parameters:
 *       - in: path
 *         name: accountAddress
 *         required: true
 *         schema:
 *           type: string
 *           example:
 *             '0x18bAD9BF2553dc16DA15d24D2679f00fB7D7E23A'
 *         description: Ethereum account address
 *     responses:
 *       200:
 *         description: Successful response
 *         content:
 *           application/json:
 *             example:
 *               message: Mint by quantity request processed successfully
 *       400:
 *         description: Bad request - Invalid Ethereum account address
 *         content:
 *           application/json:
 *             example:
 *               error: Invalid Ethereum account address
 */
router.get('/nfts/:accountAddress', async (req, res) => {
  const { accountAddress } = req.params

  if (!ethers.utils.isAddress(accountAddress)) {
    return res.status(400).json({ error: 'Invalid Ethereum account address' })
  }

  //Add in code to get the account nfts

  res.status(200).json([])
})

export default router
