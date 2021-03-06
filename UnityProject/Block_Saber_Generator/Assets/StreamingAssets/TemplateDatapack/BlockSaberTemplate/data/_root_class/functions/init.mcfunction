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
scoreboard objectives add PlayerScore dummy
scoreboard objectives add Multiplier dummy
scoreboard objectives add PlayingSong dummy
scoreboard objectives add HealthPoints dummy
scoreboard objectives add CopyTemp dummy

scoreboard objectives add FinishedNotes dummy
scoreboard objectives add FinishedObsicles dummy

scoreboard objectives add HighBlockHeight dummy
scoreboard objectives add MidBlockHeight dummy
scoreboard objectives add LowBlockHeight dummy

scoreboard objectives add SongID dummy
scoreboard objectives add IsPlayerSneeking minecraft.custom:minecraft.sneak_time


#Set Const
scoreboard objectives add Const_3 dummy
scoreboard objectives add Const_2 dummy
scoreboard players set #CONST Const_3 3
scoreboard players set #CONST Const_2 2

# Hide commands
gamerule sendCommandFeedback false

# Auto Display Songs
execute as @r at @s run function _root_class:stop_main_menu
