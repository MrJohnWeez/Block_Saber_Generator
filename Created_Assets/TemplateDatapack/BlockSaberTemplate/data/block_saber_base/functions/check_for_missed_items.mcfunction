execute at @s positioned ~-1 ~1 ~2 as @e[dx=3,dy=4,dz=2,tag=noteBlue] run say -1 Missed Blue Note
execute at @s positioned ~-1 ~1 ~2 as @e[dx=3,dy=4,dz=2,tag=noteBlue] run kill @s

execute at @s positioned ~-1 ~1 ~2 as @e[dx=3,dy=4,dz=2,tag=noteRed] run say -1 Missed Red Note
execute at @s positioned ~-1 ~1 ~2 as @e[dx=3,dy=4,dz=2,tag=noteRed] run kill @s

execute at @s positioned ~-1 ~1 ~2 as @e[dx=3,dy=4,dz=2,tag=node] run kill @s