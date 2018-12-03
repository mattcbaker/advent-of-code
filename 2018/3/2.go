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
	claims := []string{}

	file, _ := os.Open("./input.txt")
	defer file.Close()

	scanner := bufio.NewScanner(file)
	for scanner.Scan() {
		claims = append(claims, scanner.Text())
	}

	grid := make(map[string]int)

	for _, claim := range claims {
		readInClaim(claim, grid)
	}

	for _, claim := range claims {
		overlap, id := checkClaimForOverlap(claim, grid)

		if !overlap {
			fmt.Printf("found it %s\n", id)
			return
		}
	}
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

func checkClaimForOverlap(claim string, grid map[string]int) (bool, string) {
	split := strings.Split(claim, " ")

	id := strings.TrimLeft(split[0], "#")
	left, _ := strconv.Atoi(strings.Split(split[2], ",")[0])
	top, _ := strconv.Atoi(strings.TrimRight(strings.Split(split[2], ",")[1], ":"))
	width, _ := strconv.Atoi(strings.Split(split[3], "x")[0])
	height, _ := strconv.Atoi(strings.Split(split[3], "x")[1])

	overlap := false
	for i := 0; i < width; i++ {
		for j := 0; j < height; j++ {
			if grid[fmt.Sprintf("%dx%d", left+i, top+j)] > 1 {
				overlap = true
			}
		}
	}

	if !overlap {
		return false, id
	} else {
		return true, id
	}
}