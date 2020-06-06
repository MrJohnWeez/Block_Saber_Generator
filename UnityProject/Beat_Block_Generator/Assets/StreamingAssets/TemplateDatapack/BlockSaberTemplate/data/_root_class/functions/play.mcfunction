function _root_class:stop
execute as @s run function _root_class:configure_base
scoreboard players set @s PlayerPlaying 1

# Give 3sec delay until song starts
scoreboard players set #BlockSaberGlobal TickCount -60
scoreboard players set #BlockSaberGlobal NodeRowID 0
scoreboard players set #BlockSaberGlobal PlayingSong 1

effect give @p[scores={PlayerPlaying=1}] saturation 1000000 20 true
effect give @p[scores={PlayerPlaying=1}] resistance 1000000 20 true

gamemode adventure @p[scores={PlayerPlaying=1}]
gamerule doMobLoot false
gamerule sendCommandFeedback false

#Spawn main points
summon armor_stand 0 150.0 0 {Tags:[playerOrgin,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
summon armor_stand 0 150.0 0 {Tags:[fakePlayerEyes,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
summon armor_stand 0 150.0 0 {Tags:[nodeCursor,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}

#Set up eye detection heights
summon armor_stand ~ ~151.8 ~ {Tags:[HighBlockHeight,temp,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
summon armor_stand ~ ~151.5 ~ {Tags:[MidBlockHeight,temp,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
summon armor_stand ~ ~151.2 ~ {Tags:[LowBlockHeight,temp,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute store result score @p[scores={PlayerPlaying=1}] HighBlockHeight run data get entity @e[type=armor_stand,tag=HighBlockHeight,limit=1] Pos[1] 1000000
execute store result score @p[scores={PlayerPlaying=1}] MidBlockHeight run data get entity @e[type=armor_stand,tag=MidBlockHeight,limit=1] Pos[1] 1000000
execute store result score @p[scores={PlayerPlaying=1}] LowBlockHeight run data get entity @e[type=armor_stand,tag=LowBlockHeight,limit=1] Pos[1] 1000000
kill @e[type=armor_stand,tag=temp]