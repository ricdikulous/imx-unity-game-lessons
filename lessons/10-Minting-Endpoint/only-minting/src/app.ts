import express from 'express'
import router from './routes/routes'
import swaggerJsdoc from 'swagger-jsdoc'
import swaggerUi from 'swagger-ui-express'

const app = express()
const port = 3000

// Define Swagger options
const swaggerOptions: swaggerJsdoc.Options = {
  definition: {
    openapi: '3.1.0',
    info: {
      title: 'Minting API',
      version: '0.0.1',
      description: 'API documentation for your Express.js application',
    },
    servers: [
      {
        url: 'http://localhost:3000',
      },
    ],
  },
  apis: ['./src/routes/*.ts'], // Update the path based on your project structure
}
const swaggerSpec = swaggerJsdoc(swaggerOptions)

app.use('/docs', swaggerUi.serve, swaggerUi.setup(swaggerSpec))

app.use(express.json())
app.use('/', router)

app.listen(port, () => {
  console.log(`Server is running at http://localhost:${port}`)
})
