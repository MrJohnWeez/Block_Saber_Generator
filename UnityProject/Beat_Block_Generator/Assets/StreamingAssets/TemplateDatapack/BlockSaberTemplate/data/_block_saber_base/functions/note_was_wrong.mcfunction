execute at @e[type=armor_stand,tag=playerOrgin] as @p[scores={SongID=SONGID}] run function _block_saber_base:wrong_point
execute at @s run kill @e[type=armor_stand,distance=..0.05]
kill @s