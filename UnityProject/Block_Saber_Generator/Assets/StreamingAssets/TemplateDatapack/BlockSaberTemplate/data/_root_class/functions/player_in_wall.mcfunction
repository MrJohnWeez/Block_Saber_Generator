# Decrease health by 2%
execute if score #BlockSaberGlobal GodModeEnabled matches 0 run scoreboard players remove #BlockSaberGlobal HealthPoints 2
scoreboard players operation #BlockSaberGlobal Multiplier /= #CONST Const_2
execute if score #BlockSaberGlobal Multiplier matches 0 run scoreboard players set #BlockSaberGlobal Multiplier 1
execute if score #BlockSaberGlobal GodModeEnabled matches 0 in minecraft:the_end run playsound minecraft:block.ender_chest.open block @a 0 150.0 500 0.5
scoreboard players set #BlockSaberGlobal Combo 0