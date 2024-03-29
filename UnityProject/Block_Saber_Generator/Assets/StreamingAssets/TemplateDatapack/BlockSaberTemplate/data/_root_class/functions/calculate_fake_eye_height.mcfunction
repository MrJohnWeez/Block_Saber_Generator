execute at @s run teleport @e[type=marker,tag=fakePlayerEyes] ~ ~ ~

# Determine where fake eye height is dependent on sneeking and is in air
execute if entity @s[scores={IsPlayerSneeking=..0},nbt={OnGround:1b}] store result entity @e[type=marker,tag=fakePlayerEyes,limit=1] Pos[1] double 0.1 run scoreboard players get #BlockSaberGlobal MidBlockHeight
execute if entity @s[scores={IsPlayerSneeking=1..},nbt={OnGround:1b}] store result entity @e[type=marker,tag=fakePlayerEyes,limit=1] Pos[1] double 0.1 run scoreboard players get #BlockSaberGlobal LowBlockHeight
execute if entity @s[nbt={OnGround:0b}] store result entity @e[type=marker,tag=fakePlayerEyes,limit=1] Pos[1] double 0.1 run scoreboard players get #BlockSaberGlobal HighBlockHeight