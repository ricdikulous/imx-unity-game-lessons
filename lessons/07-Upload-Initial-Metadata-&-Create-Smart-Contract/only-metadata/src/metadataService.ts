import * as fs from 'fs'
import * as path from 'path'
import { S3Client, PutObjectCommand } from '@aws-sdk/client-s3'
import * as dotenv from 'dotenv'
dotenv.config()

const awsAccessKeyId = process.env.AWS_ACCESS_KEY_ID
const awsSecretAccessKey = process.env.AWS_SECRET_ACCESS_KEY
const bucketName = process.env.S3_BUCKET_NAME
const region = process.env.S3_REGION

if (!awsAccessKeyId) {
  throw new Error('Missing environment variable: AWS_ACCESS_KEY_ID')
}
if (!awsSecretAccessKey) {
  throw new Error('Missing environment variable: AWS_SECRET_ACCESS_KEY')
}
if (!bucketName) {
  throw new Error('Missing environment variable: S3_BUCKET_NAME')
}
if (!region) {
  throw new Error('Missing environment variable: S3_REGION')
}

const contractMetadataDirectory = './metadata/contract-metadata'
const nftImageExtension = 'png'

const s3NftMetadataPath = 'nft-metadata/'

const s3 = new S3Client({
  region: region,
  credentials: {
    accessKeyId: awsAccessKeyId,
    secretAccessKey: awsSecretAccessKey,
  },
})

async function uploadToS3(
  localPath: string,
  key: string,
  body: Buffer | string,
  contentType: string,
): Promise<void> {
  const params: PutObjectCommand = new PutObjectCommand({
    Bucket: bucketName,
    Key: key,
    Body: body,
    ContentType: contentType,
  })

  try {
    await s3.send(params)
    console.log(`Uploaded ${localPath} to ${bucketName}/${key}`)
  } catch (error) {
    console.error(
      `Error uploading ${localPath} to ${bucketName}/${key}:`,
      error,
    )
  }
}

export async function init() {
  //Upload the contract logo
  const contractLogoPath = path.join(
    contractMetadataDirectory,
    `logo.${nftImageExtension}`,
  )
  const logoS3Key = 'logo.png'
  await uploadToS3(
    contractLogoPath,
    logoS3Key,
    fs.readFileSync(contractLogoPath),
    `image/${nftImageExtension}`,
  )

  //Upload the contract metadata
  const contractMetadataPath = path.join(
    contractMetadataDirectory,
    'contract.json',
  )
  const s3Key = 'contract.json'

  const parsedContractMetadata = JSON.parse(
    fs.readFileSync(contractMetadataPath, 'utf-8'),
  )
  parsedContractMetadata['image'] =
    `https://${bucketName}.s3.amazonaws.com/${logoS3Key}`

  await uploadToS3(
    contractMetadataPath,
    s3Key,
    JSON.stringify(parsedContractMetadata),
    'application/json; charset=utf-8',
  )

  console.log(`
******************************************************************************

Base URI:                https://${bucketName}.s3.amazonaws.com/${s3NftMetadataPath}
Collection Metadata URI: https://${bucketName}.s3.amazonaws.com/contract.json

******************************************************************************
`)
}
