package main

import (
	"fmt"
	"os"
	"strings"
	"time"
)

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		fmt.Println("File reading error", err)
		return
	}

}
