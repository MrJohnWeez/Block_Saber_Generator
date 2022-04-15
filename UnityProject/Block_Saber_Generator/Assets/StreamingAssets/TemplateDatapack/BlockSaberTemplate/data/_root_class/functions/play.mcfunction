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

scoreboard players set #BlockSaberGlobal BackLasersGroupColor 0
scoreboard players set #BlockSaberGlobal RingLightsGroupColor 0
scoreboard players set #BlockSaberGlobal LeftRotatingLasersGroupColor 0
scoreboard players set #BlockSaberGlobal RightRotatingLasersGroupColor 0
scoreboard players set #BlockSaberGlobal CenterLightsGroupColor 0
scoreboard players set #BlockSaberGlobal BoostLightColor 0
scoreboard players set #BlockSaberGlobal ExtraLeftSideLightsColor 0
scoreboard players set #BlockSaberGlobal ExtraRightSideLightsColor 0
scoreboard players set #BlockSaberGlobal LeftSideLasersColor 0
scoreboard players set #BlockSaberGlobal RightSideLasersColor 0

scoreboard players set #BlockSaberGlobal BackLasersGroupOnOff 0
scoreboard players set #BlockSaberGlobal RingLightsGroupOnOff 0
scoreboard players set #BlockSaberGlobal LeftRotatingLasersGroupOnOff 0
scoreboard players set #BlockSaberGlobal RightRotatingLasersGroupOnOff 0
scoreboard players set #BlockSaberGlobal CenterLightsGroup 0
scoreboard players set #BlockSaberGlobal BoostLightOnOff 0
scoreboard players set #BlockSaberGlobal ExtraLeftSideLightsOnOff 0
scoreboard players set #BlockSaberGlobal ExtraRightSideLightsOnOff 0
scoreboard players set #BlockSaberGlobal LeftSideLasersOnOff 0
scoreboard players set #BlockSaberGlobal RightSideLasersOnOff 0

scoreboard players set @s PlayerPlaying 1

# Kill all song related pointers
execute run kill @e[type=armor_stand,tag=title]
execute run kill @e[type=marker,tag=playerOrgin]
execute run kill @e[type=marker,tag=fakePlayerEyes]
execute run kill @e[type=marker,tag=nodeCursor]
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
execute at @e[type=marker,tag=origin,limit=1] run summon marker ~ ~ ~ {Tags:[playerOrgin,blocksaber]}
execute at @e[type=marker,tag=origin,limit=1] run summon marker ~ ~ ~ {Tags:[fakePlayerEyes,blocksaber]}
execute at @e[type=marker,tag=origin,limit=1] run summon marker ~ ~ ~ {Tags:[nodeCursor,blocksaber]}
