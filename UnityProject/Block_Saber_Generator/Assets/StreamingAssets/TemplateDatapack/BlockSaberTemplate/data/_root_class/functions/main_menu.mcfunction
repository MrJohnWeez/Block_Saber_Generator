# Clear visuals of all
stopsound @a music
title @a clear
title @a reset
function _root_class:player_effects

# World commands
gamerule commandBlockOutput false
gamerule sendCommandFeedback false
gamerule logAdminCommands false
gamerule doMobSpawning false
gamerule announceAdvancements false
difficulty normal

# Reset vars
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
scoreboard players set #BlockSaberGlobal CenterLightsGroupOnOff 0
scoreboard players set #BlockSaberGlobal BoostLightOnOff 0
scoreboard players set #BlockSaberGlobal ExtraLeftSideLightsOnOff 0
scoreboard players set #BlockSaberGlobal ExtraRightSideLightsOnOff 0
scoreboard players set #BlockSaberGlobal LeftSideLasersOnOff 0
scoreboard players set #BlockSaberGlobal RightSideLasersOnOff 0

# Clear playing
scoreboard players set @a PlayerPlaying 0

function _root_class:verify_resourcepack_all_players

# Show song list
kill @e[type=armor_stand,tag=showTitle,tag=blocksaber]
execute as @a run function _root_class:song_list

scoreboard players set #BlockSaberGlobal LevelMapSpawned 10