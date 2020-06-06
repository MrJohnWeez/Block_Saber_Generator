# Increase health by 1% x Multiplier
scoreboard players operation #BlockSaberGlobal XpPoints += #BlockSaberGlobal XpLevels
scoreboard players operation #BlockSaberGlobal PlayerScore += #BlockSaberGlobal XpLevels
playsound minecraft:entity.player.attack.nodamage block @a 0 150.0 0 1 0.5
execute as @p[scores={PlayerPlaying=1}] run function _root_class:update_xp_display