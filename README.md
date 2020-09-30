Battleships 
============

Simple version of the game Battleships.
Allows a single human player to play a one-sided game of Battleships placed by the computer.

## Get Started

Before running the game prepare your environment. Ensure that you have installed:
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## Run and test application

In order to play the game you can use console:
* in directory containing Battleships.csproj file: ```dotnet run``` and follow the instructions 

Random gameplay:

![Image](https://raw.githubusercontent.com/orzech123123/BattleshipsBdd/master/gameplay-example.PNG?raw=true)

In order to run tests of the application you can use console:
* run tests for the solution in app root drectory: ```dotnet test```

Example tests results:

![Image](https://raw.githubusercontent.com/orzech123123/BattleshipsBdd/master/tests-run-example.PNG?raw=true)


---------


## TODO 
As promised we are sending code review:
* A bit of magic numbers https://github.com/orzech123123/BattleshipsBdd/blob/master/Battleships/Program.cs#L10

* Commented out debug code

* + Used ToString for clean write to console

* Quite misleading name, this is not IterateThrough, but DoubleIterate. And could replaced with following code: areShipsColliding = shipsToPlace.Any(ship => shipsToPlace.Any(otherShip => ship.IsColliding(other)) note, that above is shorter, uses standard linq and is more efficient as it finishes iteration as soon as any collision is found.

* All logic in this method below belongs to ship (https://github.com/orzech123123/BattleshipsBdd/blob/master/Battleships/Grid.cs#L96), that would mean method Damage can be changed into e.g. Shoot and result in Ship returning valid state as it’s this state that defines response.

* Using GridCellState?[,] is giving this codebase a bit of pain. If simple List<T> was used (with T having row, col and state) it would render a lot of code simple iteration instead of double iteration. Only place where it helps is here (https://github.com/orzech123123/BattleshipsBdd/blob/master/Battleships/Ship.cs#L16), but it complicates code in all other places.

* + Nice that it uses BDD for testing, renders test’s cleaner
