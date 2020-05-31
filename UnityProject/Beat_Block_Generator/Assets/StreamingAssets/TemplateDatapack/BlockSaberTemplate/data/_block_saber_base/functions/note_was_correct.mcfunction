execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongID=SONGID}] run function _block_saber_base:correct_point
execute at @s run kill @e[type=armor_stand,distance=..0.05]
execute at @s run teleport @s ~ ~-300 ~
kill @s