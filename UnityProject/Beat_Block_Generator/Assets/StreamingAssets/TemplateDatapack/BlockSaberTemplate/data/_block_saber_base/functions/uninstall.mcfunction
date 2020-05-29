execute as @a run function _block_saber_base:stop
scoreboard objectives remove NoteHealth
scoreboard objectives remove NodeRowID
scoreboard objectives remove TickCount
scoreboard objectives remove Difficulty
scoreboard objectives remove TempVar1
scoreboard objectives remove TempVar2
scoreboard objectives remove Const_3

scoreboard objectives remove HighBlockHeight
scoreboard objectives remove MidBlockHeight
scoreboard objectives remove LowBlockHeight

scoreboard objectives remove SongID
scoreboard objectives remove IsPlayerSneeking

gamerule doMobLoot true
team remove NoCollide
gamerule sendCommandFeedback true