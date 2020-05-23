execute as @s run function block_saber_base:stop
execute as @s run function block_saber_base:configure_base

#Set up vars
scoreboard players set @s SongUUID SONGUUID
scoreboard players set @s IsPlayerSneeking 0
scoreboard players set @s TempVar1 0
scoreboard players set @s AirTime 0
scoreboard players set @s TickCount -21
scoreboard players set @s NodeRowID 0
scoreboard players set @s Difficulty 1
scoreboard players set #TEST NoteHealth 0

effect give @s saturation 1000000 20 true
effect give @s resistance 1000000 20 true

#Spawn main points
execute at @s run summon armor_stand ~ ~ ~ {Tags:[playerOrgin,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute at @s run summon armor_stand ~ ~ ~ {Tags:[fakePlayerEyes,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}

execute at @e[type=armor_stand,tag=playerOrgin] run summon armor_stand ~ ~ ~ {Tags:[nodeCursor,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute at @e[type=armor_stand,tag=playerOrgin] run summon armor_stand ~-0.45 ~1.2 ~OFFSET {Tags:[nodeSpawnOrgin,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}

#Set up detection heights
execute at @s run summon armor_stand ~ ~1.8 ~ {Tags:[HighBlockHeight,temp,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute at @s run summon armor_stand ~ ~1.5 ~ {Tags:[MidBlockHeight,temp,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute at @s run summon armor_stand ~ ~1.2 ~ {Tags:[LowBlockHeight,temp,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute store result score @s HighBlockHeight run data get entity @e[type=armor_stand,tag=HighBlockHeight,limit=1] Pos[1] 1000000
execute store result score @s MidBlockHeight run data get entity @e[type=armor_stand,tag=MidBlockHeight,limit=1] Pos[1] 1000000
execute store result score @s LowBlockHeight run data get entity @e[type=armor_stand,tag=LowBlockHeight,limit=1] Pos[1] 1000000
kill @e[type=armor_stand,tag=temp]

execute at @s run playsound minecraft:SONG music @s ~ ~ ~ 1

