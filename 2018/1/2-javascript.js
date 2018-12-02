const readline = require('readline')
const fs = require('fs')

const frequencies = [0]

function readFrequencyChanges() {
  readline.createInterface({
    input: fs.createReadStream('./input.txt')
  })
  .on('line', handleFrequencyChange)
  .on('close', readFrequencyChanges)
}

function handleFrequencyChange(change){
  const frequency = frequencies[frequencies.length - 1] + parseInt(change)

  if(frequencies.includes(frequency)) {
    console.log(`First repeated frequency ${frequency}`)
    process.exit()
  }

  frequencies.push(frequency)
}

readFrequencyChanges()