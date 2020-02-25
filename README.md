# PokerHandShowdown
A short library for a poker hand showdown mini console game. Developed on .NET Core 3.1

## Get Started
Compile and run the solution. Once running, enter a player by adding a minus (-) sign followed by the desired cards in hand. For example
`-Arzey 5D 1D TD 6D 7D`. You can add as many players as you want.

You can run a simulation by pressing enter after the console launches.

## Notes
- Card are all unique and should only have one copy per round.
- A minimum of four players will always be created by the system.
- A maximum of 5 card codes can only be added to the hand. Any exceeding codes will be trimmed down automatically.
- If less than 5 cards is specified during the input, the system will automatically fill the remaining cards slots.
- If an invalid value has been specified to the card input, the system will treat it as invalid and will move on to the next code.
- Shortcodes are not case-sensitive

## Valid Shortcodes
### Spades
| Name | Code |
|:----:|------|
| Ace of Spades | AC |
| Two of Spades | 2S |
| Three of Spades | 3S |
| Four of Spades | 4S |
| Five of Spades  | 5S |
| Six of Spades | 6S |
| Seven of Spades | 7S |
| Eight of Spades | 8S |
| Nine of Spades | 9S |
| Ten of Spades | TS |
| Jack of Spades | JS |
| Queen of Spades | QS |
| King of Spades | KS |


### Hearts
| Name | Code |
|:----:|------|
| Ace of Hearts | AH |
| Two of Hearts | 2H |
| Three of Hearts | 3H |
| Four of Hearts | 4H |
| Five of Hearts  | 5H |
| Six of Hearts | 6H |
| Seven of Hearts | 7H |
| Eight of Hearts | 8H |
| Nine of Hearts | 9H |
| Ten of Hearts | TH |
| Jack of Hearts | JH |
| Queen of Hearts | QH |
| King of Hearts | KH |

### Clubs
| Name | Code |
|:----:|------|
| Ace of Clubs | AC |
| Two of Clubs | 2C |
| Three of Clubs | 3C |
| Four of Clubs | 4C |
| Five of Clubs  | 5C |
| Six of Clubs | 6C |
| Seven of Clubs | 7C |
| Eight of Clubs | 8C |
| Nine of Clubs | 9C |
| Ten of Clubs | TC |
| Jack of Clubs | JC |
| Queen of Clubs | QC |
| King of Clubs | KC |


### Diamonds
| Name | Code |
|:----:|------|
| Ace of Diamonds | AD |
| Two of Diamonds | 2D |
| Three of Diamonds | 3D |
| Four of Diamonds | 4D |
| Five of Diamonds  | 5D |
| Six of Diamonds | 6D |
| Seven of Diamonds | 7D |
| Eight of Diamonds | 8D |
| Nine of Diamonds | 9D |
| Ten of Diamonds | TD |
| Jack of Diamonds | JD |
| Queen of Diamonds | QD |
| King of Diamonds | KD |

