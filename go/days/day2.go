func day2() {
	redLength := len("red")
	blueLength := len("blue")
	greenLength := len("green")

	deel1 := 0
	deel2 := 0

	lines := strings.Split(string(data), "\n")
	start := time.Now()
	for _, line := range lines {
		//Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
		cursor := 5

		gamenumber := getNumber(line, &cursor)

		cursor += 2
		redMaxValue := 1
		blueMaxValue := 1
		greenMaxValue := 1

		for cursor < len(line) {
			colorNumber := getNumber(line, &cursor)
			cursor++

			switch line[cursor] {
			case 'r':
				if redMaxValue < colorNumber {
					redMaxValue = colorNumber
				}
				cursor += redLength
				break

			case 'b':
				if blueMaxValue < colorNumber {
					blueMaxValue = colorNumber
				}
				cursor += blueLength
				break

			case 'g':
				if greenMaxValue < colorNumber {
					greenMaxValue = colorNumber
				}
				cursor += greenLength
				break
			}

			cursor += 2
		}

		if redMaxValue <= 12 && greenMaxValue <= 13 && blueMaxValue <= 14 {
			deel1 += gamenumber
		}

		deel2 += redMaxValue * blueMaxValue * greenMaxValue
	}
	elapsed := time.Since(start)
	fmt.Println("Deel 1:", deel1)
	fmt.Println("Deel 2:", deel2)
	fmt.Println("Hoelang:", elapsed)
}

func getNumber(line string, cursor *int) int {
	number := 0

	for line[*cursor] >= '0' && line[*cursor] <= '9' {
		number = number*10 + int(line[*cursor]-'0')
		*cursor++
	}

	return number
}