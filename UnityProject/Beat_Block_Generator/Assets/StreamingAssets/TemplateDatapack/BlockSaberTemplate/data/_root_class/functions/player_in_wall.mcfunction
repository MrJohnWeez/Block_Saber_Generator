# Decrease health by 2%
scoreboard players remove #BlockSaberGlobal HealthPoints 2
scoreboard players operation #BlockSaberGlobal XpLevels /= #CONST Const_2
execute if score #BlockSaberGlobal XpLevels matches 0 run scoreboard players set #BlockSaberGlobal XpLevels 1
execute in minecraft:the_end run playsound minecraft:block.ender_chest.open block @a 0 150.0 500 0.5
scoreboard players set #BlockSaberGlobal Combo 0