# Verify Resourcepack
execute in minecraft:the_end positioned 0 150.0 500 run summon item ~ ~ ~ {Item:{id:"minecraft:dead_fire_coral_block",Count:1b,display:{Name:"[{\"text\":\"Red Saber\",\"italic\":false}]"},Unbreakable:1b,HideFlags:63,CanDestroy:["minecraft:air"],CanPlaceOn:["minecraft:air"],Enchantments:[{id:"minecraft:unbreaking",lvl:3}]}}
scoreboard players set #BlockSaberGlobal TempVar1 0
execute in minecraft:the_end positioned 0 150.0 500 as @e[type=item,name="Red Saber",distance=..2] run scoreboard players set #BlockSaberGlobal TempVar1 1
execute if score #BlockSaberGlobal TempVar1 matches 0 run function _root_class:resourcepack_error

kill @e[tag=blockBeat]
stopsound @a music
title @a clear
title @a reset
execute as @a[scores={PlayerPlaying=1}] run clear
scoreboard players set @a PlayerPlaying 0

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
scoreboard players set #BlockSaberGlobal XpLevels 1
scoreboard players set #BlockSaberGlobal NodeRowID 0
scoreboard players set #BlockSaberGlobal Combo 0

execute in minecraft:the_end positioned 0 150.0 500 as @e[type=item,distance=..6] run kill @s

function _root_class:spawn_titles
