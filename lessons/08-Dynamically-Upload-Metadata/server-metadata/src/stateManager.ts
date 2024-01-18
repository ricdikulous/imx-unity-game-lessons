import fs from 'fs'

const stateFilePath = './src/state.json'

interface State {
  latestTokenID: number
}

// Read the state from the file
export function readState(): State {
  const data = fs.readFileSync(stateFilePath, 'utf-8')
  return JSON.parse(data)
}

// Write the state to the file
export function writeState(state: State): void {
  const data = JSON.stringify(state, null, 2)
  fs.writeFileSync(stateFilePath, data, 'utf-8')
}
