say beatsaber reloaded!

function block_saber_base:uninstall

team add NoCollide
team modify NoCollide collisionRule never
team modify NoCollide deathMessageVisibility never

scoreboard objectives add NoteHealth dummy
scoreboard objectives add PlayerUUID dummy
scoreboard objectives add Const_3 dummy

scoreboard objectives add TempVar1 dummy

scoreboard objectives add HighBlockHeight dummy
scoreboard objectives add MidBlockHeight dummy
scoreboard objectives add LowBlockHeight dummy

scoreboard objectives add SongUUID dummy
scoreboard objectives add IsPlayerSneeking minecraft.custom:minecraft.sneak_time
scoreboard objectives add AirTime dummy

#Set constants
scoreboard players set #CONST3 Const_3 3


execute as MrJohnWeez run function block_saber_base:play

#TODO:
#-Texture spectual_arrow to stop





# bpm = beats per minute
# bps = beats per second
# mpb = meters per beat
# tps = ticks per second
# mps = meters per second
# mpt = meters per tick

# The max mpt minecraft can handle without hitbox errors is 2mps ish?

# 24 (notes per beat) * 0.21 (distance of a single note block) = 5.04 (mpb)
# 5.04 (mpb) / 20 (tps) = 0.252 (mps)
# 2 (max mps) / 0.252 (mps) = 7.93650793651 (bps)
# 7.93650793651 (bps) * 60 (bpm)  = 476.19 (bpm)


# Questions:
# -Does a bomb disapear if face hits it
# -Does bomb disapear if sword hits it
# -Does the red wall slowly kill you bases on that white bar?
