# Decrease health by 10%
execute if score #BlockSaberGlobal GodModeEnabled matches 1 run scoreboard players set #BlockSaberGlobal HealthPoints 50
execute if score #BlockSaberGlobal GodModeEnabled matches 0 run scoreboard players remove #BlockSaberGlobal HealthPoints 10
scoreboard players operation #BlockSaberGlobal Multiplier /= #CONST Const_2
execute if score #BlockSaberGlobal Multiplier matches 0 run scoreboard players set #BlockSaberGlobal Multiplier 1
execute if score #BlockSaberGlobal GodModeEnabled matches 0 in minecraft:the_end run playsound minecraft:entity.player.hurt block @a 0 150.0 500 1
scoreboard players set #BlockSaberGlobal Combo 0