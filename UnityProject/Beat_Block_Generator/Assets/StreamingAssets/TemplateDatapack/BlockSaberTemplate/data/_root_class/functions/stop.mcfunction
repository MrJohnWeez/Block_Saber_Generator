kill @e[tag=blockBeat]
stopsound @a music
effect clear @a
title @a clear
title @a reset
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
scoreboard players set #BlockSaberGlobal XpPoints 50
scoreboard players set #BlockSaberGlobal XpLevels 0

execute positioned 0 150.0 0 as @a[distance=..4] run function _root_class:update_xp_display
execute positioned 0 150.0 0 as @e[type=item,distance=..4] run kill @s
