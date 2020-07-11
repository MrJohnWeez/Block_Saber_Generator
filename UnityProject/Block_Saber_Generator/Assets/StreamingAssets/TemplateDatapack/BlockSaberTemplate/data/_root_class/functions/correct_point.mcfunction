# Increase health by 1% x Multiplier
scoreboard players operation #BlockSaberGlobal HealthPoints += #BlockSaberGlobal Multiplier
scoreboard players operation #BlockSaberGlobal PlayerScore += #BlockSaberGlobal Multiplier
execute in minecraft:the_end run playsound minecraft:entity.player.attack.nodamage block @a 0 150.0 500 1 0.5
scoreboard players add #BlockSaberGlobal Combo 1