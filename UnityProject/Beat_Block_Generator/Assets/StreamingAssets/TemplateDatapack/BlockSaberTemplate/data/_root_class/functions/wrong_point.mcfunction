# Decrease health by 10%
scoreboard players remove #BlockSaberGlobal XpPoints 10
scoreboard players operation #BlockSaberGlobal XpLevels /= #CONST Const_2
execute if score #BlockSaberGlobal XpLevels matches 0 run scoreboard players set #BlockSaberGlobal XpLevels 1
execute in minecraft:the_end run playsound minecraft:entity.player.hurt block @a 0 150.0 500 1
execute as @p[scores={PlayerPlaying=1}] run function _root_class:update_xp_display