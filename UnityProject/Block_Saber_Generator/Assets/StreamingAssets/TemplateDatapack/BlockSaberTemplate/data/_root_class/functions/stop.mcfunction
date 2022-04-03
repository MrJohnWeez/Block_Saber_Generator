# Verify Resourcepack
execute at @a run summon item ~ ~ ~ {Age:-32768,PickupDelay:32767,Item:{id:"minecraft:structure_void",Count:1b,tag:{HideFlags:127}}}
scoreboard players set @a TempVar1 0
execute as @a positioned as @s at @e[type=item,name="BeatSaber Void",distance=..2] run scoreboard players set @s TempVar1 1
execute as @a if score @s TempVar1 matches 0 run function _root_class:resourcepack_error
scoreboard players set @a TempVar1 0

stopsound @a music
title @a clear
title @a reset
scoreboard players set @a PlayerPlaying 0

effect give @a saturation 1000000 20 true
effect give @a resistance 1000000 20 true
effect give @a night_vision 1000000 1 true

scoreboard players set #BlockSaberGlobal SongID 0
scoreboard players set #BlockSaberGlobal TickCount 0
scoreboard players set #BlockSaberGlobal IsPlayerSneeking 0
scoreboard players set #BlockSaberGlobal TempVar1 0
scoreboard players set #BlockSaberGlobal FinishedNotes 0
scoreboard players set #BlockSaberGlobal FinishedObsicles 0
scoreboard players set #BlockSaberGlobal PlayerScore 0
scoreboard players set #BlockSaberGlobal FinishedCount 0
scoreboard players set #BlockSaberGlobal Multiplier 1
scoreboard players set #BlockSaberGlobal PlayingSong 0
scoreboard players set #BlockSaberGlobal HealthPoints 50
scoreboard players set #BlockSaberGlobal NodeRowID 0
scoreboard players set #BlockSaberGlobal Combo 0

execute in minecraft:the_end positioned ~ ~ ~ as @e[type=item,distance=..30] run kill @s
function _root_class:spawn_titles
function blocksaber:song_list
