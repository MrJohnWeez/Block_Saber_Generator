#> Center the player in block then spawn platform

# Make sure player if facing the correct direction when game begins
execute as @s run function _block_saber_base:reset_player_position
execute at @s run fill ~-1 ~ ~-1 ~1 ~2 ~1 minecraft:air
execute at @s run fill ~-1 ~-1 ~-1 ~1 ~-1 ~1 minecraft:black_concrete replace
execute at @s run fill ~-1 ~ ~-1 ~1 ~ ~1 minecraft:brick_wall replace
execute at @s run setblock ~ ~ ~ minecraft:air replace