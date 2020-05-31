execute at @s positioned ~-1 ~1 ~2 as @e[dx=3,dy=4,dz=2,tag=noteBlue] at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongID=SONGID}] run function _block_saber_base:wrong_point
execute at @s positioned ~-1 ~1 ~2 as @e[dx=3,dy=4,dz=2,tag=noteBlue] run kill @s

execute at @s positioned ~-1 ~1 ~2 as @e[dx=3,dy=4,dz=2,tag=noteRed] at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongID=SONGID}] run function _block_saber_base:wrong_point
execute at @s positioned ~-1 ~1 ~2 as @e[dx=3,dy=4,dz=2,tag=noteRed] run kill @s

execute at @s positioned ~-1 ~1 ~2 as @e[dx=3,dy=4,dz=2,tag=node] run kill @s