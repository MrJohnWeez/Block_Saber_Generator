kill @e[tag=blockBeat]
stopsound @s music
scoreboard players set @s SongID 0
scoreboard players set @s TickCount 0
scoreboard players set @s IsPlayerSneeking 0
scoreboard players set @s TempVar1 0
scoreboard players set @s FinishedNotes 0
scoreboard players set @s FinishedObsicles 0
scoreboard players set @s PlayerScore 0
scoreboard players set @s FinishedCount 0
scoreboard players set @s Multiplier 1
scoreboard players set @s PlayingSong 0
experience set @s 1 levels
experience set @s 0 points
effect clear @s
execute at @s as @e[type=item,distance=..6] run kill @s
clear @s
title @s clear
title @s reset