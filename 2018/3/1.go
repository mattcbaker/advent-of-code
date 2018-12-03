// wish I had time to clean this up, but sadly the next advent day is coming up so soon!!

package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	grid := make(map[string]int)

	file, _ := os.Open("./input.txt")
	defer file.Close()

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		readInClaim(scanner.Text(), grid)
	}

	overlap := 0

	for _, count := range grid {
		if count > 1 {
			overlap += 1
		}
	}

	fmt.Printf("first challenge: %d\n", overlap)
}

func readInClaim(claim string, grid map[string]int) {
	split := strings.Split(claim, " ")

	left, _ := strconv.Atoi(strings.Split(split[2], ",")[0])
	top, _ := strconv.Atoi(strings.TrimRight(strings.Split(split[2], ",")[1], ":"))
	width, _ := strconv.Atoi(strings.Split(split[3], "x")[0])
	height, _ := strconv.Atoi(strings.Split(split[3], "x")[1])

	for i := 0; i < width; i++ {
		for j := 0; j < height; j++ {
			grid[fmt.Sprintf("%dx%d", left+i, top+j)] += 1
		}
	}
}
