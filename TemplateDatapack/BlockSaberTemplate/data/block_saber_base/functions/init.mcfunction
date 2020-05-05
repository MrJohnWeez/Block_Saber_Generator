say beatsaber reloaded!

function block_saber_base:uninstall

team add NoCollide
team modify NoCollide collisionRule never
team modify NoCollide deathMessageVisibility never

scoreboard objectives add NoteHealth dummy
scoreboard objectives add PlayerUUID dummy
scoreboard objectives add Const_10 dummy

scoreboard objectives add TempVar1 dummy

scoreboard objectives add HighBlockHeight dummy
scoreboard objectives add MidBlockHeight dummy
scoreboard objectives add LowBlockHeight dummy

scoreboard objectives add SongUUID dummy
scoreboard objectives add IsPlayerSneeking minecraft.custom:minecraft.sneak_time
scoreboard objectives add AirTime dummy

#Set constants
scoreboard players set #CONST10 Const_10 10


execute as MrJohnWeez run function block_saber_base:play

#Dropping the red saber restarts the song

#Use motion instead of teleporting
#Must also then face





#bpm = beats per minute
#bps = beats per second
#mpb = meters per beat
#tps = ticks per second
#mps = meters per second
#mpt = meters per tick

#The max mpt minecraft can handle without hitbox errors is 
#There are 16.8 meters per beat in minecraft
#20tps in one second

#60bpm / 60s = 1bps
#16.8mpb * 1bps = 16.8mps
#16.8mps / 20tps = 0.84mpt










#Play around with the smaller note idea






#Questions:
#Does a bomb disapear if face hits it
#Does bomb disapear if sword hits it
#Does the red wall slowly kill you bases on that white bar?
