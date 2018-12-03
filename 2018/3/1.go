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
		processClaim(scanner.Text(), grid)
	}

	overlap := 0

	for _, count := range grid {
		if count > 1 {
			overlap += 1
		}
	}

	fmt.Printf("first challenge: %d\n", overlap)
}

func processClaim(claim string, grid map[string]int) {
	split := strings.Split(claim, " ")

	left, _ := strconv.Atoi(strings.Split(split[2], ",")[0])
	top, _ := strconv.Atoi(strings.TrimRight(strings.Split(split[2], ",")[1], ":"))
	width, _ := strconv.Atoi(strings.Split(split[3], "x")[0])
	height, _ := strconv.Atoi(strings.Split(split[3], "x")[1])

	// fmt.Printf("id=%s, left=%d, top=%d, width=%d, height=%d\n", id, left, top, width, height)

	for i := 0; i < width; i++ {
		for j := 0; j < height; j++ {
			grid[fmt.Sprintf("%dx%d", left+i, top+j)] += 1
		}
	}
}
