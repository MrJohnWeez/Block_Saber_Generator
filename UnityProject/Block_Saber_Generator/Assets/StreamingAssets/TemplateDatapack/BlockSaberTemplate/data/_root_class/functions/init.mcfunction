# World commands
gamerule commandBlockOutput false
gamerule sendCommandFeedback false
gamerule logAdminCommands false
gamerule doMobSpawning false
gamerule announceAdvancements false
difficulty normal

#Define Teams
team add NoCollide
team modify NoCollide collisionRule never
team modify NoCollide deathMessageVisibility never

#Define Scoreboards
scoreboard objectives add NoteHealth dummy
scoreboard objectives add NodeRowID dummy
scoreboard objectives add TickCount dummy
scoreboard objectives add FinishedCount dummy
scoreboard objectives add Difficulty dummy
scoreboard objectives add PlayerPlaying dummy
scoreboard objectives add Combo dummy

scoreboard objectives add TempVar1 dummy
scoreboard objectives add TempVar2 dummy
scoreboard objectives add TempVar3 dummy
scoreboard objectives add TempVar4 dummy
scoreboard objectives add PlayerScore dummy
scoreboard objectives add Multiplier dummy
scoreboard objectives add PlayingSong dummy
scoreboard objectives add HealthPoints dummy
scoreboard objectives add CopyTemp dummy

# Lights color 0 - 24
scoreboard objectives add BackLasersGroupColor dummy
scoreboard objectives add RingLightsGroupColor dummy
scoreboard objectives add LeftRotatingLasersGroupColor dummy
scoreboard objectives add RightRotatingLasersGroupColor dummy
scoreboard objectives add CenterLightsGroupColor dummy
scoreboard objectives add BoostLightColor dummy
scoreboard objectives add ExtraLeftSideLightsColor dummy
scoreboard objectives add ExtraRightSideLightsColor dummy
scoreboard objectives add LeftSideLasersColor dummy
scoreboard objectives add RightSideLasersColor dummy

# Lights on or off value t/f
scoreboard objectives add BackLasersGroupOnOff dummy
scoreboard objectives add RingLightsGroupOnOff dummy
scoreboard objectives add LeftRotatingLasersGroupOnOff dummy
scoreboard objectives add RightRotatingLasersGroupOnOff dummy
scoreboard objectives add CenterLightsGroup dummy
scoreboard objectives add BoostLightOnOff dummy
scoreboard objectives add ExtraLeftSideLightsOnOff dummy
scoreboard objectives add ExtraRightSideLightsOnOff dummy
scoreboard objectives add LeftSideLasersOnOff dummy
scoreboard objectives add RightSideLasersOnOff dummy

scoreboard objectives add FinishedNotes dummy
scoreboard objectives add FinishedObsicles dummy
scoreboard objectives add FinishedEvents dummy

scoreboard objectives add HighBlockHeight dummy
scoreboard objectives add MidBlockHeight dummy
scoreboard objectives add LowBlockHeight dummy

scoreboard objectives add SongID dummy
scoreboard objectives add IsPlayerSneeking minecraft.custom:minecraft.sneak_time

scoreboard objectives add GodModeEnabled dummy
scoreboard objectives add LightShowEnabled dummy

execute unless score #BlockSaberGlobal GodModeEnabled matches 1 run scoreboard players set #BlockSaberGlobal GodModeEnabled 0
execute unless score #BlockSaberGlobal LightShowEnabled matches 0 run scoreboard players set #BlockSaberGlobal LightShowEnabled 1

# Set Const
scoreboard objectives add Const_3 dummy
scoreboard objectives add Const_2 dummy
scoreboard players set #CONST Const_3 3
scoreboard players set #CONST Const_2 2

scoreboard objectives add EndIsLoaded dummy
scoreboard objectives add LevelMapSpawned dummy


# Init values
scoreboard players set #BlockSaberGlobal EndIsLoaded 0
scoreboard players set #BlockSaberGlobal LevelMapSpawned 0

execute as @a[gamemode=!spectator] run function _root_class:reset_player_position