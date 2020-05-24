#Run base class
execute as @s run function _block_saber_base:play


scoreboard players set @s SongID SONGID

execute at @s run playsound minecraft:SONG music @s ~ ~ ~ 1
