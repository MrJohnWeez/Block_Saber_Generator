execute as @s run function _block_saber_base:stop
execute as @s run function _block_saber_base:configure_base

# Give 3sec delay until song starts
scoreboard players set @s TickCount -60
scoreboard players set @s NodeRowID 0

scoreboard players set @s PlayingSong 1

effect give @s saturation 1000000 20 true
effect give @s resistance 1000000 20 true

# Reset health to 50%
experience set @s 28 levels
experience set @s 50 points
experience set @s 1 levels

gamemode adventure @s
gamerule doMobLoot false
gamerule sendCommandFeedback false

#Spawn main points
execute at @s run summon armor_stand ~ ~ ~ {Tags:[playerOrgin,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute at @s run summon armor_stand ~ ~ ~ {Tags:[fakePlayerEyes,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}

execute at @e[type=armor_stand,tag=playerOrgin] run summon armor_stand ~ ~ ~ {Tags:[nodeCursor,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}

#Set up eye detection heights
execute at @s run summon armor_stand ~ ~1.8 ~ {Tags:[HighBlockHeight,temp,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute at @s run summon armor_stand ~ ~1.5 ~ {Tags:[MidBlockHeight,temp,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute at @s run summon armor_stand ~ ~1.2 ~ {Tags:[LowBlockHeight,temp,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute store result score @s HighBlockHeight run data get entity @e[type=armor_stand,tag=HighBlockHeight,limit=1] Pos[1] 1000000
execute store result score @s MidBlockHeight run data get entity @e[type=armor_stand,tag=MidBlockHeight,limit=1] Pos[1] 1000000
execute store result score @s LowBlockHeight run data get entity @e[type=armor_stand,tag=LowBlockHeight,limit=1] Pos[1] 1000000
kill @e[type=armor_stand,tag=temp]