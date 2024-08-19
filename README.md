# Shooter-Game-Project
# Overview
 It is a personally developed idea of a console game that initializes a 2-dimensional array as a terrain with possible enemies to shoot. It has been created via OOP principles that allow easy manageability and control over game objects and entities. Additionally, it is connected to a database storage system for user data preservation for future app developments (Leaderboards, Statistics, etc...). To achieve better decoupling of enemies' properties and abilities, Shooter Game has implemented repository and factory patterns.
# How to play
On startup, you will be required to enter your username. Hence you follow the guideline:
1.  You will be warped in a randomly generated map. It could be DefaultMap(5:5) size or CustomMap(x,y) size:
2.  You will be listed several commands available in our game: **Shoot/StatsUpdate/Report/Hint**
3.  You must start with the Shoot command and write the x and y coordinates on a new row to navigate your crosshair where you want to shoot
>[!NOTE]
>coordinates begin from {0:0} and should not exceed map size
4.  You can either continue shooting or update your stats with the UpdateStats command
5.  When you are finished you call the command Report which will end your game, save your user data, and depict your stats on the console screen.
