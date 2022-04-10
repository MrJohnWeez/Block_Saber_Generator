# Clear visuals of all
stopsound @a music
title @a clear
title @a reset
function _root_class:player_effects

# Reset song vars
scoreboard players set #BlockSaberGlobal IsPlayerSneeking 0
scoreboard players set #BlockSaberGlobal FinishedNotes 0
scoreboard players set #BlockSaberGlobal FinishedObsicles 0
scoreboard players set #BlockSaberGlobal PlayerScore 0
scoreboard players set #BlockSaberGlobal FinishedCount 0
scoreboard players set #BlockSaberGlobal Multiplier 1
scoreboard players set #BlockSaberGlobal HealthPoints 50
scoreboard players set #BlockSaberGlobal NodeRowID 0
scoreboard players set #BlockSaberGlobal Combo 0

scoreboard players set @s PlayerPlaying 1

# Kill all song related pointers
execute run kill @e[type=armor_stand,tag=title]
execute run kill @e[type=armor_stand,tag=playerOrgin]
execute run kill @e[type=armor_stand,tag=fakePlayerEyes]
execute run kill @e[type=armor_stand,tag=nodeCursor]
execute run kill @e[tag=node]

# Give 3 seconds delay until song starts
scoreboard players set #BlockSaberGlobal TickCount -60

# Set playtime vars
scoreboard players set #BlockSaberGlobal PlayingSong 1
scoreboard players set #BlockSaberGlobal HighBlockHeight 1528
scoreboard players set #BlockSaberGlobal MidBlockHeight 1525
scoreboard players set #BlockSaberGlobal LowBlockHeight 1522

experience set @p[scores={PlayerPlaying=1}] 0 levels
experience set @p[scores={PlayerPlaying=1}] 0 points

gamemode adventure @p[scores={PlayerPlaying=1}]

#Spawn main points
execute at @e[type=marker,tag=origin,limit=1] run summon armor_stand ~ ~ ~ {Tags:[playerOrgin,blocksaber],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute at @e[type=marker,tag=origin,limit=1] run summon armor_stand ~ ~ ~ {Tags:[fakePlayerEyes,blocksaber],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute at @e[type=marker,tag=origin,limit=1] run summon armor_stand ~ ~ ~ {Tags:[nodeCursor,blocksaber],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
