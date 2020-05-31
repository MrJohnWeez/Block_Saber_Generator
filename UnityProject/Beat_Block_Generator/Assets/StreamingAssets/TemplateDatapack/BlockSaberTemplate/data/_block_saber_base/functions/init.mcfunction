say beatsaber reloaded!

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
scoreboard objectives add Const_3 dummy

scoreboard objectives add TempVar1 dummy
scoreboard objectives add TempVar2 dummy
scoreboard objectives add PlayerScore dummy
scoreboard objectives add Multiplier dummy
scoreboard objectives add PlayingSong dummy

scoreboard objectives add FinishedNotes dummy
scoreboard objectives add FinishedObsicles dummy

scoreboard objectives add HighBlockHeight dummy
scoreboard objectives add MidBlockHeight dummy
scoreboard objectives add LowBlockHeight dummy

scoreboard objectives add SongID dummy
scoreboard objectives add IsPlayerSneeking minecraft.custom:minecraft.sneak_time

#Set constants
scoreboard players set #CONST3 Const_3 3