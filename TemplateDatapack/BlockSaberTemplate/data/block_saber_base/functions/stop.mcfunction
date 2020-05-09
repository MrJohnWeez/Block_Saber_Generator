kill @e[tag=blockBeat]
stopsound @s music
scoreboard players set @s SongUUID 0
effect clear @s
execute at @s as @e[type=item,distance=..6] run kill @s
clear @s