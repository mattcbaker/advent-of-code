const readline = require('readline')
const fs = require('fs')

let frequency = 0

readline.createInterface({
  input: fs.createReadStream('./input.txt')
})
.on('line', change => frequency += parseInt(change))
.on('close', () => console.log(frequency))