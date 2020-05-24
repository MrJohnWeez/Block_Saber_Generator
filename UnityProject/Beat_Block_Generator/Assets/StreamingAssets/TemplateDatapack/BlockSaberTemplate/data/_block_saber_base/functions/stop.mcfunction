kill @e[tag=blockBeat]
stopsound @s music
scoreboard players set @s SongID 0
scoreboard players set @s TickCount 0
effect clear @s
execute at @s as @e[type=item,distance=..6] run kill @s
clear @s